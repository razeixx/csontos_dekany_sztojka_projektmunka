using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LakossagNyilvantartas.jaras;

namespace LakossagNyilvantartas
{
    internal class regio
    {    
    // A régió neve (pl. "Dél-Alföld", "Közép-Dunántúl")
    public string Nev { get; set; }

    // Ebbe a listába fogjuk gyűjteni azokat a Megyéket, amik ehhez a régióhoz tartoznak
    public List<Megye> Megyek { get; set; }

    // Konstruktor: amikor létrehozunk egy új régiót, kötelező megadni a nevét
    public Regio(string nev)
    {
        Nev = nev;
        Megyek = new List<Megye>(); // Létrehozzuk az üres listát, hogy később hozzáadhassunk megyéket
    }

}
