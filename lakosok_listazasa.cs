using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakossagNyilvantartas
{
    public class Lakos
    {
        public List<lakosság> Lakosok { get; set; }

        public Lakos()
        {
            Lakosok = new List<lakosság>();
        }

        // Lakosok listázása
        public void Listazas()
        {
            foreach (lakosság lakos in Lakosok)
            {
                Console.WriteLine(lakos.Nev);
            }
        }
    }
}
