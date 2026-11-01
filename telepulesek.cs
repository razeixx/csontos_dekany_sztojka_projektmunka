using System.Collections.Generic;

namespace LakosNyilvantarto.Modellek
{
    public enum TelepulesTipus { Fovaros, Varos, Kozseg }

    public class Telepules
    {
        public string Nev { get; set; }
        public string Iranyitoszam { get; set; }
        public TelepulesTipus Tipus { get; set; }
    }
}
