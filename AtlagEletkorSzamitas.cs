
using System;
using System.Collections.Generic;
using System.Linq;

namespace LakossagNyilvantartas
{
    public class AtlagEletkorSzamitas
    {
        public List<Szemely> Lakosok { get; set; }

        public AtlagEletkorSzamitas(List<Szemely> lakosok)
        {
            Lakosok = lakosok;
        }

        public void Szamitas()
        {
            List<Szemely> aktivLakosok = Lakosok
                .Where(x => !x.Torolt)
                .ToList();

            if (aktivLakosok.Count == 0)
            {
                Console.WriteLine("Nincs beolvasott lakos!");
                return;
            }

            double atlag = aktivLakosok.Average(x =>
            {
                int kor = DateTime.Now.Year -
                          x.SzuletesiDatum.Year;

                if (DateTime.Now.Date <
                    x.SzuletesiDatum.AddYears(kor).Date)
                {
                    kor--;
                }

                return kor;
            });

            Console.WriteLine(
                $"Lakosok átlagéletkora: {atlag:F1} év");
        }
    }
}
