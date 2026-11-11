using System;
using System.Collections.Generic;
using System.Linq;

namespace LakossagNyilvantartas
{
    public class KeresesAzonositoSzerint
    {
        public List<Szemely> Lakosok { get; set; }

        public KeresesAzonositoSzerint(List<Szemely> lakosok)
        {
            Lakosok = lakosok;
        }

        public Szemely Kereses(string azonosito)
        {
            Szemely talalat = Lakosok.FirstOrDefault(
                x => x.Azonosito.Ertek == azonosito && !x.Torolt
            );

            if (talalat != null)
            {
                Console.WriteLine($"Lakos megtalálva: {talalat.VezetekNev} {talalat.KeresztNev} ({talalat.SzuletesiHely}, {talalat.SzuletesiDatum:yyyy.MM.dd.})");
            }
            else
            {
                Console.WriteLine("Nem található lakos ezzel az azonosítóval!");
            }

            return talalat;
        }
    }
}
