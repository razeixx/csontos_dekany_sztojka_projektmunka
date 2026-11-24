using System;
using System.Collections.Generic;
using System.Linq;

namespace LakossagNyilvantartas
{
    public enum RendezesIrany
    {
        Novekvo,
        Csokkeno
    }

    public class RendezesLakosok
    {
        public List<Szemely> Lakosok { get; set; }

        public RendezesLakosok(List<Szemely> lakosok)
        {
            Lakosok = lakosok ?? new List<Szemely>();
        }

        public List<Szemely> RendezesNevSzerint(RendezesIrany irany = RendezesIrany.Novekvo)
        {
            IEnumerable<Szemely> aktiv = AktivLakosok();

            List<Szemely> rendezett = (irany == RendezesIrany.Novekvo)
                ? aktiv.OrderBy(x => x.VezetekNev ?? string.Empty)
                       .ThenBy(x => x.KeresztNev ?? string.Empty)
                       .ToList()
                : aktiv.OrderByDescending(x => x.VezetekNev ?? string.Empty)
                       .ThenByDescending(x => x.KeresztNev ?? string.Empty)
                       .ToList();

            KiirLista(rendezett, "Név szerint (" + IranySzoveg(irany) + ")", lakos =>
                NevSzoveg(lakos));
            return rendezett;
        }

        public List<Szemely> RendezesEletkorSzerint(RendezesIrany irany = RendezesIrany.Novekvo)
        {
            IEnumerable<Szemely> aktiv = AktivLakosok();

            List<Szemely> rendezett = (irany == RendezesIrany.Novekvo)
                ? aktiv.OrderBy(x => EletkorSzamitas(x.SzuletesiDatum))
                       .ThenBy(x => x.VezetekNev ?? string.Empty)
                       .ToList()
                : aktiv.OrderByDescending(x => EletkorSzamitas(x.SzuletesiDatum))
                       .ThenBy(x => x.VezetekNev ?? string.Empty)
                       .ToList();

            KiirLista(rendezett, "Életkor szerint (" + IranySzoveg(irany) + ")", lakos =>
                NevSzoveg(lakos) + " - " + EletkorSzamitas(lakos.SzuletesiDatum) + " év");
            return rendezett;
        }

        public List<Szemely> RendezesTelepulesSzerint(RendezesIrany irany = RendezesIrany.Novekvo)
        {
            IEnumerable<Szemely> aktiv = AktivLakosok();

            List<Szemely> rendezett = (irany == RendezesIrany.Novekvo)
                ? aktiv.OrderBy(x => TelepulesNev(x))
                       .ThenBy(x => x.VezetekNev ?? string.Empty)
                       .ToList()
                : aktiv.OrderByDescending(x => TelepulesNev(x))
                       .ThenBy(x => x.VezetekNev ?? string.Empty)
                       .ToList();

            KiirLista(rendezett, "Település szerint (" + IranySzoveg(irany) + ")", lakos =>
                NevSzoveg(lakos) + " | " + TelepulesNev(lakos));
            return rendezett;
        }

        public List<Szemely> RendezesMegyeSzerint(
            Dictionary<string, string> telepulesToMegye,
            RendezesIrany irany = RendezesIrany.Novekvo)
        {
            Dictionary<string, string> lookup = NormalizaltLookup(telepulesToMegye);
            IEnumerable<Szemely> aktiv = AktivLakosok();

            List<Szemely> rendezett = (irany == RendezesIrany.Novekvo)
                ? aktiv.OrderBy(x => MegyeNev(x, lookup))
                       .ThenBy(x => x.VezetekNev ?? string.Empty)
                       .ToList()
                : aktiv.OrderByDescending(x => MegyeNev(x, lookup))
                       .ThenBy(x => x.VezetekNev ?? string.Empty)
                       .ToList();

            KiirLista(rendezett, "Megye szerint (" + IranySzoveg(irany) + ")", lakos =>
                NevSzoveg(lakos) + " | " + MegyeNev(lakos, lookup) + " (" + TelepulesNev(lakos) + ")");
            return rendezett;
        }

        private IEnumerable<Szemely> AktivLakosok()
        {
            return Lakosok.Where(x => x != null && !x.Torolt);
        }

        private static string NevSzoveg(Szemely lakos)
        {
            return ((lakos.VezetekNev ?? string.Empty) + " " + (lakos.KeresztNev ?? string.Empty)).Trim();
        }

        private static string TelepulesNev(Szemely lakos)
        {
            return (lakos.Lakhely != null && !string.IsNullOrWhiteSpace(lakos.Lakhely.Nev))
                ? lakos.Lakhely.Nev
                : "Ismeretlen település";
        }

        private static Dictionary<string, string> NormalizaltLookup(Dictionary<string, string> forras)
        {
            var eredmeny = new Dictionary<string, string>();
            if (forras == null)
                return eredmeny;

            foreach (var pair in forras)
            {
                if (!string.IsNullOrWhiteSpace(pair.Key))
                {
                    eredmeny[pair.Key.Trim().ToLower()] = pair.Value ?? "Ismeretlen megye";
                }
            }

            return eredmeny;
        }

        private static string MegyeNev(Szemely lakos, Dictionary<string, string> lookup)
        {
            if (lakos.Lakhely == null || string.IsNullOrWhiteSpace(lakos.Lakhely.Nev))
                return "Ismeretlen megye";

            string kulcs = lakos.Lakhely.Nev.Trim().ToLower();
            string megye;
            return lookup.TryGetValue(kulcs, out megye) ? megye : "Ismeretlen megye";
        }

        private static int EletkorSzamitas(DateTime szuletesiDatum)
        {
            DateTime ma = DateTime.Today;
            int eletkor = ma.Year - szuletesiDatum.Year;

            if (szuletesiDatum.Date > ma.AddYears(-eletkor))
            {
                eletkor--;
            }

            return Math.Max(0, eletkor);
        }

        private static string IranySzoveg(RendezesIrany irany)
        {
            return irany == RendezesIrany.Novekvo ? "növekvő" : "csökkenő";
        }

        private static void KiirLista(List<Szemely> lista, string cim, Func<Szemely, string> formazo)
        {
            Console.WriteLine("=== Rendezés: " + cim + " (" + lista.Count + " fő) ===");
            if (lista.Count == 0)
            {
                Console.WriteLine("Nincs megjeleníthető lakos.");
                return;
            }

            foreach (var lakos in lista)
            {
                string azonosito = lakos.Azonosito != null ? lakos.Azonosito.Ertek.ToString() : "N/A";
                Console.WriteLine("- " + formazo(lakos) + " (Azonosító: " + azonosito + ")");
            }
        }
    }
}
