using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakosNyilvantarto
{
    public Jaras SzuloJaras { get; set; }
    public enum TelepulesTipus { Fovaros, Varos, Kozseg }

    public class Telepules
    {
        public string Nev { get; set; }
        public string Iranyitoszam { get; set; }
        public TelepulesTipus Tipus { get; set; }
    }
}
