using System;
using System.Collections.Generic;
using System.Linq;

namespace LakossagNyilvantartas
{
    public class SzuresMegyeSzerint
    {
        public List<Szemely> Lakosok { get; set; }

        public SzuresMegyeSzerint(List<Szemely> lakosok)
        {
            Lakosok = lakosok;
        }

        public List<Szemely> Szures(string megyeNev)
        {
            List<Szemely> talalatok = Lakosok.Where(
                x => !x.Torolt &&
                     x.Lakhely != null &&
                     x.Lakhely.Megye.Nev.ToLower() == megyeNev.ToLower()
            ).ToList();

            if (talalatok.Count > 0)
            {
                Console.WriteLine($"{megyeNev} lakosai ({talalatok.Count} fő):");
                foreach (var lakos in talalatok)
                {
                    Console.WriteLine($"- {lakos.VezetekNev} {lakos.KeresztNev} (Azonosító: {lakos.Azonosito.Ertek})");
                }
            }
            else
            {
                Console.WriteLine("Ebben a megyében nem található lakos!");
            }

            return talalatok;
        }
    }
}
