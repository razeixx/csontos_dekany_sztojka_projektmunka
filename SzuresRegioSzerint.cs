using System;
using System.Collections.Generic;
using System.Linq;

namespace LakossagNyilvantartas
{
    public class SzuresRegioSzerint
    {
        public List<Szemely> Lakosok { get; set; }

        public SzuresRegioSzerint(List<Szemely> lakosok)
        {
            Lakosok = lakosok;
        }

        public List<Szemely> Szures(string regioNev)
        {
            List<Szemely> talalatok = Lakosok.Where(
                x => !x.Torolt &&
                     x.Lakhely != null &&
                     x.Lakhely.Regio.Nev.ToLower() == regioNev.ToLower()
            ).ToList();

            if (talalatok.Count > 0)
            {
                Console.WriteLine($"{regioNev} lakosai ({talalatok.Count} fő):");
                foreach (var lakos in talalatok)
                {
                    Console.WriteLine($"- {lakos.VezetekNev} {lakos.KeresztNev} (Azonosító: {lakos.Azonosito.Ertek})");
                }
            }
            else
            {
                Console.WriteLine("Ebben a régióban nem található lakos!");
            }

            return talalatok;
        }
    }
}
