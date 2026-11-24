using System;
using System.Collections.Generic;
using System.Linq;

namespace LakossagNyilvantartas
{
    public class SzuresEletkorSzerint
    {
        public List<Szemely> Lakosok { get; set; }

        public SzuresEletkorSzerint(List<Szemely> lakosok)
        {
            Lakosok = lakosok ?? new List<Szemely>();
        }

        public List<Szemely> Szures(int? minKor, int? maxKor, DateTime? viszonyitasiDatum = null)
        {
            if ((minKor.HasValue && minKor.Value < 0) || (maxKor.HasValue && maxKor.Value < 0))
            {
                Console.WriteLine("Hiba: az életkor nem lehet negatív!");
                return new List<Szemely>();
            }

            if (minKor.HasValue && maxKor.HasValue && minKor.Value > maxKor.Value)
            {
                Console.WriteLine("Hiba: a minimum életkor nem lehet nagyobb a maximum életkornál!");
                return new List<Szemely>();
            }

            if (!minKor.HasValue && !maxKor.HasValue)
            {
                Console.WriteLine("Hiba: legalább minimum vagy maximum életkort meg kell adni!");
                return new List<Szemely>();
            }

            DateTime alapDatum = (viszonyitasiDatum ?? DateTime.Today).Date;

            if (Lakosok.Any(x => !x.Torolt && x.SzuletesiDatum.Date > alapDatum))
            {
                Console.WriteLine("Figyelmeztetés: található olyan lakos, akinek a születési dátuma a viszonyítási dátum után van.");
            }

            List<Szemely> talalatok = Lakosok
                .Where(x =>
                {
                    if (x.Torolt || x.SzuletesiDatum.Date > alapDatum)
                        return false;

                    int eletkor = EletkorSzamitas(x.SzuletesiDatum, alapDatum);
                    bool megfelelMin = !minKor.HasValue || eletkor >= minKor.Value;
                    bool megfelelMax = !maxKor.HasValue || eletkor <= maxKor.Value;
                    return megfelelMin && megfelelMax;
                })
                .ToList();

            KiirTalalatok(talalatok, minKor, maxKor, alapDatum);
            return talalatok;
        }

        public List<Szemely> SzuresPontosKor(int eletkor, DateTime? viszonyitasiDatum = null)
        {
            return Szures(eletkor, eletkor, viszonyitasiDatum);
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

        private static string TartomanySzovegKeszites(int? minKor, int? maxKor)
        {
            if (minKor.HasValue && maxKor.HasValue && minKor.Value == maxKor.Value)
                return minKor.Value + " éves";

            if (minKor.HasValue && maxKor.HasValue)
                return minKor.Value + "-" + maxKor.Value + " év közötti";

            if (minKor.HasValue)
                return "legalább " + minKor.Value + " éves";

            if (maxKor.HasValue)
                return "legfeljebb " + maxKor.Value + " éves";

            return "megadott életkorú";
        }

        private static void KiirTalalatok(List<Szemely> talalatok, int? minKor, int? maxKor, DateTime alapDatum)
        {
            string tartomanySzoveg = TartomanySzovegKeszites(minKor, maxKor);

            Console.WriteLine("Életkor: " + tartomanySzoveg);
            Console.WriteLine("Találatok száma: " + talalatok.Count);

            if (talalatok.Count == 0)
            {
                Console.WriteLine("Ebben az életkor-tartományban nem található lakos!");
                return;
            }

            foreach (var lakos in talalatok)
            {
                int kor = EletkorSzamitas(lakos.SzuletesiDatum, alapDatum);
                string telepulesNev = lakos.Lakhely != null ? lakos.Lakhely.Nev : "Ismeretlen település";
                string azonosito = lakos.Azonosito != null ? lakos.Azonosito.Ertek.ToString() : "N/A";
                Console.WriteLine("- " + lakos.VezetekNev + " " + lakos.KeresztNev + " (" + kor + " év, Azonosító: " + azonosito + ") | " + telepulesNev);
            }
        }
    }
}
