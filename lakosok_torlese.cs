using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakossagNyilvantartas
{
    public class LakosTorles
    {
        public List<Lakossag> Lakosok { get; set; }

        // Konstruktor
        public LakosTorles(List<Lakossag> lakosok)
        {
            Lakosok = lakosok;
        }

        // Lakos törlése azonosító alapján
        public void Torles(string azonosito)
        {
            Lakossag torlendo = Lakosok.Find(x => x.Azonosito == azonosito);

            if (torlendo != null)
            {
                Lakosok.Remove(torlendo);
                Console.WriteLine("A lakos sikeresen törölve!");
            }
            else
            {
                Console.WriteLine("Nem található ilyen lakos!");
            }
        }
    }
}
