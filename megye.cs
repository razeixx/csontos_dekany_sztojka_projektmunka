using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LakossagNyilvantartas.jaras;

namespace LakossagNyilvantartas
{
    internal class megye
    {
        public Regio SzuloRegio { get; set; }
        public class Megye { public int 
        Id { get; set; } 
        public string Nev { get; set; } 
        public string Regio { get; set; } 
        public List<Jaras> Jarasok { get; set; } = new List<Jaras>(); }
    }
}
