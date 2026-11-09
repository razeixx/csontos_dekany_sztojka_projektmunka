using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakossagNyilvantartas
{
    public class LakosListazas
    {
        public List<Lakossag> Lakosok { get; set; }

        // Konstruktor
        public LakosListazas(List<Lakossag> lakosok)
        {
            Lakosok = lakosok;
        }

        // Lakosok adatainak listázása
        public void Listazas()
        {
            foreach (Lakossag lakos in Lakosok)
            {
                Console.WriteLine("--------------------");
                Console.WriteLine($"Azonosító: {lakos.Azonosito}");
                Console.WriteLine($"Név: {lakos.Nev}");
                Console.WriteLine($"Születési dátum: {lakos.SzuletesiDatum}");
                Console.WriteLine($"Nem: {lakos.Nem}");
                Console.WriteLine($"Település: {lakos.Telepules}");
                Console.WriteLine($"Irányítószám: {lakos.Iranyitoszam}");
                Console.WriteLine($"Lakcím: {lakos.Lakcim}");
            }
        }
    }
}
