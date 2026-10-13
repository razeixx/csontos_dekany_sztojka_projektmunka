using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakossagNyilvantartas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine(" MAGYARORSZÁGI LAKOSNYILVÁNTARTÁS");
            Console.WriteLine("========================================");
            Console.WriteLine("");
            Console.WriteLine("1. Lakosok kezelése");
            Console.WriteLine("2. Települések kezelése");
            Console.WriteLine("3. Lakos keresése");
            Console.WriteLine("4. Lakosok szűrése");
            Console.WriteLine("5. Lakosok rendezése");
            Console.WriteLine("6. Települési statisztikák");
            Console.WriteLine("7. megyei statisztikák");
            Console.WriteLine("8. Régiós statisztikák");
            Console.WriteLine("9. Országos statisztikák");
            Console.WriteLine("10. Adatok mentése");
            Console.WriteLine("11. Adatok betöltése");
            Console.WriteLine("0. Kilépés");

            Console.Write("Választás: ");
            Console.ReadKey();
        }
    }
}
