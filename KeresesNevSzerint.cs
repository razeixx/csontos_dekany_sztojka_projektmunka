using System;
using System.Collections.Generic;
using System.Linq;

namespace LakossagNyilvantartas
{
    public class KeresesNevSzerint
    {
        public List<Szemely> Lakosok { get; set; }

        public KeresesNevSzerint(List<Szemely> lakosok)
        {
            Lakosok = lakosok;
        }

        public List<Szemely> Kereses(string keresettNev)
        {
            List<Szemely> talalatok = Lakosok.Where(
                x => !x.Torolt &&
                     ($"{x.VezetekNev} {x.KeresztNev}".ToLower().Contains(keresettNev.ToLower()) ||
                      x.VezetekNev.ToLower() == keresettNev.ToLower() ||
                      x.KeresztNev.ToLower() == keresettNev.ToLower())
            ).ToList();

            if (talalatok.Count > 0)
            {
                Console.WriteLine($"Találatok száma ({keresettNev}): {talalatok.Count} fő");
                foreach (var lakos in talalatok)
                {
                    Console.WriteLine($"- {lakos.VezetekNev} {lakos.KeresztNev} (Azonosító: {lakos.Azonosito.Ertek})");
                }
            }
            else
            {
                Console.WriteLine("Nem található ilyen nevű lakos!");
            }

            return talalatok;
        }
    }
}
