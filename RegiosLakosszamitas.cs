
using System;
using System.Collections.Generic;
using System.Linq;

namespace LakossagNyilvantartas
{
    public class RegiosLakosszamitas
    {
        public List<Szemely> Lakosok { get; set; }

        public RegiosLakosszamitas(List<Szemely> lakosok)
        {
            Lakosok = lakosok;
        }

        public void Szamitas()
        {
            var eredmeny = Lakosok
                .Where(x => !x.Torolt &&
                            x.Lakhely != null &&
                            x.Lakhely.Megye != null &&
                            x.Lakhely.Megye.Regio != null)
                .GroupBy(x => x.Lakhely.Megye.Regio.Nev);

            Console.WriteLine("Régiónkénti lakosszám:");

            foreach (var regio in eredmeny)
            {
                Console.WriteLine(
                    $"{regio.Key}: {regio.Count()} fő");
            }
        }
    }
}
