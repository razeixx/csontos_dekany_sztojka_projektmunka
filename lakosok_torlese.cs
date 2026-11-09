using System;
using System.Collections.Generic;
using System.Linq;

namespace LakossagNyilvantartas
{
    public class LakosTorles
    {
        public List<Szemely> Lakosok { get; set; }

    
        public LakosTorles(List<Szemely> lakosok)
        {
            Lakosok = lakosok;
        }


        public void Torles(string azonosito)
        {
            Szemely torlendo = Lakosok.FirstOrDefault(
                x => x.Azonosito.Ertek == azonosito && !x.Torolt
            );

            if (torlendo != null)
            {
                torlendo.Torolt = true;
                Console.WriteLine("A lakos sikeresen törölve!");
            }
            else
            {
                Console.WriteLine("Nem található ilyen lakos!");
            }
        }
    }
}
