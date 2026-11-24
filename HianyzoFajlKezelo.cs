using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LakossagNyilvantartas
{
    public class HianyzoFajlKezelo
    {
        public bool AlapFajlLetrehozasaHaHianyzik { get; set; }

        public HianyzoFajlKezelo(bool alapFajlLetrehozasaHaHianyzik = false)
        {
            AlapFajlLetrehozasaHaHianyzik = alapFajlLetrehozasaHaHianyzik;
        }

        public bool FajlLetezikE(string fajlUtvonal)
        {
            if (string.IsNullOrWhiteSpace(fajlUtvonal))
            {
                Console.WriteLine("Hiba: üres vagy érvénytelen fájlútvonal!");
                return false;
            }

            if (!File.Exists(fajlUtvonal))
            {
                Console.WriteLine("Figyelmeztetés: a fájl nem található: " + fajlUtvonal);

                if (AlapFajlLetrehozasaHaHianyzik)
                {
                    return AlapFajlLetrehozasa(fajlUtvonal);
                }

                return false;
            }

            return true;
        }

        public List<Szemely> HianyzoFajlKezeleseLakosokhoz(string fajlUtvonal)
        {
            FajlLetezikE(fajlUtvonal);
            return new List<Szemely>();
        }

        private static bool AlapFajlLetrehozasa(string fajlUtvonal)
        {
            try
            {
                string konyvtar = Path.GetDirectoryName(fajlUtvonal);
                if (!string.IsNullOrWhiteSpace(konyvtar) && !Directory.Exists(konyvtar))
                {
                    Directory.CreateDirectory(konyvtar);
                }

                File.WriteAllText(fajlUtvonal, string.Empty, Encoding.UTF8);
                Console.WriteLine("Új adatfájl létrehozva: " + fajlUtvonal);
                return true;
            }
            catch (IOException ex)
            {
                Console.WriteLine("Hiba az új fájl létrehozásakor: " + ex.Message);
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Hiba: nincs jogosultság a fájl létrehozásához (" + ex.Message + ").");
                return false;
            }
        }
    }
}
