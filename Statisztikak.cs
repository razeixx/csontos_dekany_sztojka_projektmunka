using System;
using System.Collections.Generic;
using System.Linq;

namespace LakossagNyilvantartas
{
    public class Statisztikak
    {
        public List<Szemely> Lakosok { get; set; }

        public Statisztikak(List<Szemely> lakosok)
        {
            Lakosok = lakosok ?? new List<Szemely>();
        }

        public void OsszesStatisztika(
            Dictionary<string, string> telepulesToMegye = null,
            Dictionary<string, string> megyeToRegio = null,
            DateTime? viszonyitasiDatum = null)
        {
            Dictionary<string, string> megyeLookup = NormalizaltLookup(telepulesToMegye);
            Dictionary<string, string> regioLookup = NormalizaltLookup(megyeToRegio);
            List<Szemely> aktiv = AktivLakosok();
            DateTime alapDatum = (viszonyitasiDatum ?? DateTime.Today).Date;

            Console.WriteLine("========== STATISZTIKÁK (10) ==========");

            Console.WriteLine("1. Nyilvántartott lakosok száma: " + aktiv.Count + " fő");

            Console.WriteLine("2. Lakosok száma településenként:");
            var telepulesCsoportok = aktiv
                .GroupBy(x => TelepulesNev(x))
                .OrderByDescending(g => g.Count())
                .ThenBy(g => g.Key)
                .ToList();
            foreach (var g in telepulesCsoportok)
            {
                Console.WriteLine("   - " + g.Key + ": " + g.Count() + " fő");
            }

            Console.WriteLine("3. Lakosok száma megyénként:");
            var megyeCsoportok = aktiv
                .GroupBy(x => MegyeNev(x, megyeLookup))
                .OrderByDescending(g => g.Count())
                .ThenBy(g => g.Key)
                .ToList();
            foreach (var g in megyeCsoportok)
            {
                Console.WriteLine("   - " + g.Key + ": " + g.Count() + " fő");
            }

            Console.WriteLine("4. Lakosok száma régiónként:");
            var regioCsoportok = aktiv
                .GroupBy(x => RegioNev(x, megyeLookup, regioLookup))
                .OrderByDescending(g => g.Count())
                .ThenBy(g => g.Key)
                .ToList();
            foreach (var g in regioCsoportok)
            {
                Console.WriteLine("   - " + g.Key + ": " + g.Count() + " fő");
            }

            double atlagKor = aktiv.Count > 0
                ? aktiv.Average(x => EletkorSzamitas(x.SzuletesiDatum, alapDatum))
                : 0.0;
            Console.WriteLine("5. Átlagéletkor: " + atlagKor.ToString("0.0") + " év");

            if (aktiv.Count > 0)
            {
                int minKor = aktiv.Min(x => EletkorSzamitas(x.SzuletesiDatum, alapDatum));
                var legfiatalabbak = aktiv
                    .Where(x => EletkorSzamitas(x.SzuletesiDatum, alapDatum) == minKor)
                    .Select(x => NevSzoveg(x));
                Console.WriteLine("6. Legfiatalabb lakos (" + minKor + " év): " + string.Join(", ", legfiatalabbak));

                int maxKor = aktiv.Max(x => EletkorSzamitas(x.SzuletesiDatum, alapDatum));
                var legidosebbek = aktiv
                    .Where(x => EletkorSzamitas(x.SzuletesiDatum, alapDatum) == maxKor)
                    .Select(x => NevSzoveg(x));
                Console.WriteLine("7. Legidősebb lakos (" + maxKor + " év): " + string.Join(", ", legidosebbek));
            }
            else
            {
                Console.WriteLine("6. Legfiatalabb lakos: nincs adat");
                Console.WriteLine("7. Legidősebb lakos: nincs adat");
            }

            int ferfiak = aktiv.Count(x => NemSzoveg(x) == "Férfi");
            int nok = aktiv.Count(x => NemSzoveg(x) == "Nő");
            double ferfiArany = aktiv.Count > 0 ? (ferfiak * 100.0 / aktiv.Count) : 0.0;
            double noArany = aktiv.Count > 0 ? (nok * 100.0 / aktiv.Count) : 0.0;
            Console.WriteLine("8. Férfiak és nők aránya: " + ferfiak + " fő (" + ferfiArany.ToString("0.0") + "%) / " +
                              nok + " fő (" + noArany.ToString("0.0") + "%)");

            var legnepesebbTelepules = telepulesCsoportok.FirstOrDefault();
            Console.WriteLine("9. Legnépesebb település: " +
                (legnepesebbTelepules != null ? legnepesebbTelepules.Key + " (" + legnepesebbTelepules.Count() + " fő)" : "nincs adat"));

            var legnepesebbMegye = megyeCsoportok.FirstOrDefault();
            Console.WriteLine("10. Legtöbb lakossal rendelkező megye: " +
                (legnepesebbMegye != null ? legnepesebbMegye.Key + " (" + legnepesebbMegye.Count() + " fő)" : "nincs adat"));

            Console.WriteLine("========================================");
        }

        private List<Szemely> AktivLakosok()
        {
            return Lakosok.Where(x => x != null && !x.Torolt).ToList();
        }

        private static string NevSzoveg(Szemely lakos)
        {
            return ((lakos.VezetekNev ?? string.Empty) + " " + (lakos.KeresztNev ?? string.Empty)).Trim();
        }

        private static string TelepulesNev(Szemely lakos)
        {
            return (lakos.Lakhely != null && !string.IsNullOrWhiteSpace(lakos.Lakhely.Nev))
                ? lakos.Lakhely.Nev.Trim()
                : "Ismeretlen település";
        }

        private static string NemSzoveg(Szemely lakos)
        {
            string nem = lakos.Nem.ToString().Trim().ToLower();
            if (nem == "f" || nem == "férfi" || nem == "ferfi" || nem == "male")
                return "Férfi";
            if (nem == "n" || nem == "nő" || nem == "no" || nem == "female")
                return "Nő";
            return "Egyéb";
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
                    eredmeny[pair.Key.Trim().ToLower()] = (pair.Value ?? string.Empty).Trim();
                }
            }

            return eredmeny;
        }

        private static string MegyeNev(Szemely lakos, Dictionary<string, string> megyeLookup)
        {
            if (lakos.Lakhely == null || string.IsNullOrWhiteSpace(lakos.Lakhely.Nev))
                return "Ismeretlen megye";

            string kulcs = lakos.Lakhely.Nev.Trim().ToLower();
            string megye;
            return (megyeLookup.TryGetValue(kulcs, out megye) && !string.IsNullOrWhiteSpace(megye))
                ? megye
                : "Ismeretlen megye";
        }

        private static string RegioNev(
            Szemely lakos,
            Dictionary<string, string> megyeLookup,
            Dictionary<string, string> regioLookup)
        {
            string megye = MegyeNev(lakos, megyeLookup);
            if (megye == "Ismeretlen megye")
                return "Ismeretlen régió";

            string kulcs = megye.Trim().ToLower();
            string regio;
            return (regioLookup.TryGetValue(kulcs, out regio) && !string.IsNullOrWhiteSpace(regio))
                ? regio
                : "Ismeretlen régió";
        }

        private static int EletkorSzamitas(DateTime szuletesiDatum, DateTime alapDatum)
        {
            int eletkor = alapDatum.Year - szuletesiDatum.Year;

            if (szuletesiDatum.Date > alapDatum.AddYears(-eletkor))
            {
                eletkor--;
            }

            return Math.Max(0, eletkor);
        }
    }
}
