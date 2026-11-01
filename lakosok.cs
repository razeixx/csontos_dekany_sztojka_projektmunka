using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakosNyilvantarto
{
    public enum Nem { Ferfi, No }

    public class SzemelyAzonosito
    {
        public string Ertek { get; }

        public SzemelyAzonosito(string ertek)
        {
            if (ertek == null || ertek.Length != 11)
                throw new ArgumentException("Érvénytelen azonosító.");
            Ertek = ertek;
        }

        public override bool Equals(object obj) =>
            obj is SzemelyAzonosito mas && mas.Ertek == Ertek;

        public override int GetHashCode() => Ertek.GetHashCode();
    }

    public class Szemely
    {
        public SzemelyAzonosito Azonosito { get; set; }
        public string VezetekNev { get; set; }
        public string KeresztNev { get; set; }
        public string SzuletesiNev { get; set; }
        public string AnyjaNeve { get; set; }
        public string SzuletesiHely { get; set; }
        public DateTime SzuletesiDatum { get; set; }
        public Nem Nem { get; set; }
        public string Telefonszam { get; set; }      
        public DateTime? HalalDatuma { get; set; }   
        public bool Torolt { get; set; }            

        public bool El
        {
            get { return HalalDatuma == null; }
        }

        public int Eletkor()
        {
            DateTime veg = HalalDatuma ?? DateTime.Today;
            int kor = veg.Year - SzuletesiDatum.Year;
            if (SzuletesiDatum.Date > veg.AddYears(-kor).Date)
                kor--;
            return kor;
        }
    }
}
