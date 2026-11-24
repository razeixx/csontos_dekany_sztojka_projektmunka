using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace LakossagNyilvantartas
{
    public class FajlKezelo
    {
        public char Elvalaszto { get; set; }

        public FajlKezelo(char elvalaszto = ';')
        {
            Elvalaszto = elvalaszto;
        }

        public List<Szemely> BetoltesLakosok(string fajlUtvonal, Func<string, Telepules> telepulesKereso = null)
        {
            var eredmeny = new List<Szemely>();

            if (string.IsNullOrWhiteSpace(fajlUtvonal))
            {
                Console.WriteLine("Hiba: érvénytelen fájlútvonal!");
                return eredmeny;
            }

            if (!File.Exists(fajlUtvonal))
            {
                Console.WriteLine("Figyelmeztetés: a megadott fájl nem található (" + fajlUtvonal + "). Üres lista jött létre.");
                return eredmeny;
            }

            string[] sorok;
            try
            {
                sorok = File.ReadAllLines(fajlUtvonal, Encoding.UTF8);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Hiba a fájl olvasása közben: " + ex.Message);
                return eredmeny;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Hiba: nincs jogosultság a fájl olvasásához (" + ex.Message + ").");
                return eredmeny;
            }

            if (sorok.Length == 0 || sorok.All(s => string.IsNullOrWhiteSpace(s)))
            {
                Console.WriteLine("Figyelmeztetés: a fájl üres (" + fajlUtvonal + ").");
                return eredmeny;
            }

            var latottAzonositok = new HashSet<string>();
            int hibasSorok = 0;

            for (int i = 0; i < sorok.Length; i++)
            {
                int sorszam = i + 1;
                string sor = sorok[i];

                if (string.IsNullOrWhiteSpace(sor))
                    continue;

                string[] mezok = sor.Split(Elvalaszto);
                if (mezok.Length < 4)
                {
                    Console.WriteLine("Hibás formátum a(z) " + sorszam + ". sorban (kevés mező): " + sor);
                    hibasSorok++;
                    continue;
                }

                string azonositoSzoveg = mezok[0].Trim();
                string vezetekNev = mezok[1].Trim();
                string keresztNev = mezok[2].Trim();
                string datumSzoveg = mezok[3].Trim();
                string nemSzoveg = mezok.Length > 4 ? mezok[4].Trim() : string.Empty;
                string telepulesNev = mezok.Length > 5 ? mezok[5].Trim() : string.Empty;
                string toroltSzoveg = mezok.Length > 6 ? mezok[6].Trim() : "false";

                if (string.IsNullOrWhiteSpace(azonositoSzoveg))
                {
                    Console.WriteLine("Hibás adat a(z) " + sorszam + ". sorban: hiányzó azonosító.");
                    hibasSorok++;
                    continue;
                }

                if (!latottAzonositok.Add(azonositoSzoveg.ToLower()))
                {
                    Console.WriteLine("Hibás adat a(z) " + sorszam + ". sorban: duplikált azonosító (" + azonositoSzoveg + ").");
                    hibasSorok++;
                    continue;
                }

                if (string.IsNullOrWhiteSpace(vezetekNev) || string.IsNullOrWhiteSpace(keresztNev))
                {
                    Console.WriteLine("Hibás adat a(z) " + sorszam + ". sorban: hiányzó név.");
                    hibasSorok++;
                    continue;
                }

                DateTime szuletesiDatum;
                if (!DateTime.TryParseExact(
                        datumSzoveg,
                        new[] { "yyyy-MM-dd", "yyyy.MM.dd", "yyyy/MM/dd" },
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out szuletesiDatum) ||
                    szuletesiDatum.Date > DateTime.Today)
                {
                    Console.WriteLine("Hibás születési dátum a(z) " + sorszam + ". sorban: '" + datumSzoveg + "'.");
                    hibasSorok++;
                    continue;
                }

                bool torolt;
                if (!bool.TryParse(toroltSzoveg, out torolt))
                {
                    if (toroltSzoveg == "1") torolt = true;
                    else if (toroltSzoveg == "0") torolt = false;
                    else
                    {
                        Console.WriteLine("Hibás logikai érték a(z) " + sorszam + ". sorban: '" + toroltSzoveg + "'.");
                        hibasSorok++;
                        continue;
                    }
                }

                Nem nemErtek;
                if (!Enum.TryParse(nemSzoveg, true, out nemErtek))
                {
                    nemErtek = default(Nem);
                }

                Telepules telepules = null;
                if (!string.IsNullOrWhiteSpace(telepulesNev) && telepulesKereso != null)
                {
                    telepules = telepulesKereso(telepulesNev);
                }

                var szemely = new Szemely
                {
                    VezetekNev = vezetekNev,
                    KeresztNev = keresztNev,
                    SzuletesiDatum = szuletesiDatum,
                    Nem = nemErtek,
                    Lakhely = telepules,
                    Torolt = torolt
                };

                BeallitAzonosito(szemely, azonositoSzoveg);
                eredmeny.Add(szemely);
            }

            Console.WriteLine("Betöltés kész: " + eredmeny.Count + " érvényes lakos, " + hibasSorok + " hibás sor kihagyva.");
            return eredmeny;
        }

        public bool MentesLakosok(string fajlUtvonal, IEnumerable<Szemely> lakosok)
        {
            if (string.IsNullOrWhiteSpace(fajlUtvonal))
            {
                Console.WriteLine("Hiba: érvénytelen mentési fájlútvonal!");
                return false;
            }

            try
            {
                string konyvtar = Path.GetDirectoryName(fajlUtvonal);
                if (!string.IsNullOrWhiteSpace(konyvtar) && !Directory.Exists(konyvtar))
                {
                    Directory.CreateDirectory(konyvtar);
                }

                var sorok = new List<string>();
                foreach (var lakos in (lakosok ?? Enumerable.Empty<Szemely>()).Where(x => x != null))
                {
                    string azonosito = lakos.Azonosito != null ? lakos.Azonosito.Ertek.ToString() : string.Empty;
                    string vezetekNev = (lakos.VezetekNev ?? string.Empty).Replace(Elvalaszto, ' ');
                    string keresztNev = (lakos.KeresztNev ?? string.Empty).Replace(Elvalaszto, ' ');
                    string datum = lakos.SzuletesiDatum.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    string nem = lakos.Nem.ToString();
                    string telepules = (lakos.Lakhely != null ? (lakos.Lakhely.Nev ?? string.Empty) : string.Empty).Replace(Elvalaszto, ' ');
                    string torolt = lakos.Torolt ? "true" : "false";

                    sorok.Add(string.Join(Elvalaszto.ToString(), azonosito, vezetekNev, keresztNev, datum, nem, telepules, torolt));
                }

                File.WriteAllLines(fajlUtvonal, sorok, Encoding.UTF8);
                Console.WriteLine("Sikeres mentés: " + sorok.Count + " lakos kiírva (" + fajlUtvonal + ").");
                return true;
            }
            catch (IOException ex)
            {
                Console.WriteLine("Hiba a fájl mentése közben: " + ex.Message);
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Hiba: nincs jogosultság a fájl írásához (" + ex.Message + ").");
                return false;
            }
        }

        private static void BeallitAzonosito(Szemely szemely, string azonositoSzoveg)
        {
            if (szemely.Azonosito == null)
                return;

            var ertekProp = szemely.Azonosito.GetType().GetProperty("Ertek");
            if (ertekProp != null && ertekProp.CanWrite)
            {
                if (ertekProp.PropertyType == typeof(int) && int.TryParse(azonositoSzoveg, out int intVal))
                {
                    ertekProp.SetValue(szemely.Azonosito, intVal, null);
                }
                else if (ertekProp.PropertyType == typeof(string))
                {
                    ertekProp.SetValue(szemely.Azonosito, azonositoSzoveg, null);
                }
            }
        }
    }
}
