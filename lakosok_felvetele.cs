using System;
using System.Collections.Generic;
using System.Linq;

namespace LakossagNyilvantartas
{
    public class LakosFelvetel
    {
        public List<Szemely> Lakosok { get; set; }

        public LakosFelvetel(List<Szemely> lakosok)
        {
            Lakosok = lakosok;
        }

        public void Felvetel(Szemely ujLakos)
        {
            Szemely letezo = Lakosok.FirstOrDefault(
                x => x.Azonosito.Ertek == ujLakos.Azonosito.Ertek && !x.Torolt
            );

            if (letezo == null)
            {
                Lakosok.Add(ujLakos);
                Console.WriteLine("Az új lakos sikeresen felvéve!");
            }
            else
            {
                Console.WriteLine("Már létezik lakos ezzel az azonosítóval!");
            }
        }
    }
}
