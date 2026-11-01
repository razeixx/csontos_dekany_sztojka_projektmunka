using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakossagNyilvantartas
{
    internal class jaras
    {
        public class Jaras
        {
            public int Id { get; set; }
            public string Nev { get; set; }
            public Megye Megye { get; set; }
            public List<Telepules> Telepulesek { get; set; } = new List<Telepules>();
        }
    }
}
