using System;
using System.Collections.Generic;

namespace LakossagNyilvantartas
{
    public class LakosListazas
    {
        public List<Szemely> Lakosok { get; set; }

 
        public LakosListazas(List<Szemely> lakosok)
        {
            Lakosok = lakosok;
        }

   
        public void Listazas()
        {
            foreach (Szemely lakos in Lakosok)
            {
                if (!lakos.Torolt)
                {
                    Console.WriteLine("--------------------------");
                    Console.WriteLine($"Azonosító: {lakos.Azonosito.Ertek}");
                    Console.WriteLine($"Név: {lakos.VezetekNev} {lakos.KeresztNev}");
                    Console.WriteLine($"Születési név: {lakos.SzuletesiNev}");
                    Console.WriteLine($"Anyja neve: {lakos.AnyjaNeve}");
                    Console.WriteLine($"Születési hely: {lakos.SzuletesiHely}");
                    Console.WriteLine($"Születési dátum: {lakos.SzuletesiDatum.ToShortDateString()}");
                    Console.WriteLine($"Nem: {lakos.Nem}");
                    Console.WriteLine($"Település: {lakos.Lakhely?.Nev}");
                    Console.WriteLine($"Telefonszám: {lakos.Telefonszam}");
                    Console.WriteLine($"Életkor: {lakos.Eletkor()}");
                }
            }
        }
    }
}
