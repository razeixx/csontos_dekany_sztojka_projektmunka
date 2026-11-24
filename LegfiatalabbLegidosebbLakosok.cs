using System;
using System.Collections.Generic;
using System.Linq;

namespace LakossagNyilvantartas
{
    public class LegfiatalabbLegidosebbLakosok
    {
        public List<Szemely> Lakosok { get; set; }

        public LegfiatalabbLegidosebbLakosok(List<Szemely> lakosok)
        {
            Lakosok = lakosok ?? new List<Szemely>();
        }

        public List<Szemely> Legfiatalabbak(DateTime? viszonyitasiDatum = null)
        {
            List<Szemely> aktiv = AktivLakosok();
            if (aktiv.Count == 0)
            {
                Console.WriteLine("Nincs aktív lakos a nyilvántartásban.");
                return new List<Szemely>();
            }

            DateTime alapDatum = (viszonyitasiDatum ?? DateTime.Today).Date;
            int minKor = aktiv.Min(x => EletkorSzamitas(x.SzuletesiDatum, alapDatum));

            List<Szemely> legfiatalabbak = aktiv
                .Where(x => EletkorSzamitas(x.SzuletesiDatum, alapDatum) == minKor)
                .ToList();

            KiirTalalatok("Legfiatalabb lakos(ok)", minKor, legfiatalabbak, alapDatum);
            return legfiatalabbak;
        }

        public List<Szemely> Legidosebbek(DateTime? viszonyitasiDatum = null)
        {
            List<Szemely> aktiv = AktivLakosok();
            if (aktiv.Count == 0)
            {
                Console.WriteLine("Nincs aktív lakos a nyilvántartásban.");
                return new List<Szemely>();
            }

            DateTime alapDatum = (viszonyitasiDatum ?? DateTime.Today).Date;
            int maxKor = aktiv.Max(x => EletkorSzamitas(x.SzuletesiDatum, alapDatum));

            List<Szemely> legidosebbek = aktiv
                .Where(x => EletkorSzamitas(x.SzuletesiDatum, alapDatum) == maxKor)
                .ToList();

            KiirTalalatok("Legidősebb lakos(ok)", maxKor, legidosebbek, alapDatum);
            return legidosebbek;
        }

        public void MeghatarozMindkettot(DateTime? viszonyitasiDatum = null)
        {
            Legfiatalabbak(viszonyitasiDatum);
            Console.WriteLine();
            Legidosebbek(viszonyitasiDatum);
        }

        private List<Szemely> AktivLakosok()
        {
            return Lakosok.Where(x => x != null && !x.Torolt).ToList();
        }

        private static void KiirTalalatok(string cim, int kor, List<Szemely> talalatok, DateTime alapDatum)
        {
            Console.WriteLine(cim + " (" + kor + " év, " + talalatok.Count + " fő):");
            foreach (var lakos in talalatok)
            {
                string nev = ((lakos.VezetekNev ?? string.Empty) + " " + (lakos.KeresztNev ?? string.Empty)).Trim();
                string azonosito = lakos.Azonosito != null ? lakos.Azonosito.Ertek.ToString() : "N/A";
                string telepules = (lakos.Lakhely != null && !string.IsNullOrWhiteSpace(lakos.Lakhely.Nev))
                    ? lakos.Lakhely.Nev.Trim()
                    : "Ismeretlen település";
                int pontosKor = EletkorSzamitas(lakos.SzuletesiDatum, alapDatum);

                Console.WriteLine("- " + nev + " (" + pontosKor + " év, Azonosító: " + azonosito + ") | Település: " + telepules);
            }
        }

        public static int EletkorSzamitas(DateTime szuletesiDatum, DateTime? viszonyitasiDatum = null)
        {
            DateTime ma = (viszonyitasiDatum ?? DateTime.Today).Date;
            int eletkor = ma.Year - szuletesiDatum.Year;

            if (szuletesiDatum.Date > ma.AddYears(-eletkor))
            {
                eletkor--;
            }

            return Math.Max(0, eletkor);
        }
    }
}
