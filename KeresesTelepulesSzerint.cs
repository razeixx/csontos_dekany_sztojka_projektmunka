using System;
using System.Collections.Generic;
using System.Linq;

namespace LakossagNyilvantartas
{
    public class KeresesTelepulesSzerint
    {
        public List<Szemely> Lakosok { get; set; }

        public KeresesTelepulesSzerint(List<Szemely> lakosok)
        {
            Lakosok = lakosok;
        }

        public List<Szemely> Kereses(string telepulesNev)
        {
            List<Szemely> talalatok = Lakosok.Where(
                x => !x.Torolt &&
                     x.Lakhely != null &&
                     x.Lakhely.Nev.ToLower() == telepulesNev.ToLower()
            ).ToList();

            if (talalatok.Count > 0)
            {
                Console.WriteLine($"{telepulesNev} lakosai ({talalatok.Count} fő):");
                foreach (var lakos in talalatok)
                {
                    Console.WriteLine($"- {lakos.VezetekNev} {lakos.KeresztNev} (Azonosító: {lakos.Azonosito.Ertek})");
                }
            }
            else
            {
                Console.WriteLine("Ezen a településen nem található lakos!");
            }

            return talalatok;
        }
    }
}
