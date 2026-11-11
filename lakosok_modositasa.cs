using System;
using System.Collections.Generic;
using System.Linq;

namespace LakossagNyilvantartas
{
    public class LakosModositas
    {
        public List<Szemely> Lakosok { get; set; }

        public LakosModositas(List<Szemely> lakosok)
        {
            Lakosok = lakosok;
        }

        public void Modositas(string azonosito, Szemely modositottAdatok)
        {
            Szemely modositando = Lakosok.FirstOrDefault(
                x => x.Azonosito.Ertek == azonosito && !x.Torolt
            );

            if (modositando != null)
            {
                Console.WriteLine("A lakos adatai sikeresen módosítva!");
            }
            else
            {
                Console.WriteLine("Nem található ilyen lakos!");
            }
        }
    }
}
