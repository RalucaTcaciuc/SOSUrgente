using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using LibrarieModele;

namespace NivelStocareDate
{
    public class Administrare_angajati_FisierText
    {
        private const string FisierAngajati = "angajati.txt";
        private const int NR_MAX_ANGAJATI = 50;
        private readonly string numeFisier;
        //private Angajat=FromString()
        public Administrare_angajati_FisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;
            try
            {
                if (!File.Exists(numeFisier))
                {
                    File.Create(numeFisier).Close();
                    Console.WriteLine($"Fișier creat: {numeFisier}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la crearea fisierului: {ex.Message}");
                throw;
            }
        }

        public bool AdaugaAngajat(Angajat angajat)
        {
            try
            {
                File.AppendAllText(numeFisier, angajat.Info() + Environment.NewLine);
                Console.WriteLine($"Angajat adăugat: {angajat.Info()}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la adăugare angajat: {ex.Message}");
                return false;
            }
        }

        public List<Angajat> GetAngajati(out int nrAngajati)
        {
            List<Angajat> angajati = new List<Angajat>();
            nrAngajati = 0;

            try
            {
                if (!File.Exists(numeFisier))
                {
                    Console.WriteLine($"Fișierul {numeFisier} nu există!");
                    return angajati;
                }

                Console.WriteLine($"Încep citirea din {numeFisier}");
                string[] linii = File.ReadAllLines(numeFisier);

                foreach (string linie in linii)
                {
                    Console.WriteLine($"Procesez linia: {linie}");
                    Angajat angajat = Angajat.FromString(linie);

                    if (angajat != null)
                    {
                        angajati.Add(angajat);
                        nrAngajati++;
                        Console.WriteLine($"Adăugat angajat: {angajat.Nume}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EROARE GRAVĂ: {ex.Message}");
            }

            Console.WriteLine($"Total angajați găsiți: {nrAngajati}");
            return angajati;
        }
        public static List<Angajat> CitesteAngajatiDinFisier()
        {
            List<Angajat> angajati = new List<Angajat>();

            if (File.Exists(FisierAngajati))
            {
                foreach (var linie in File.ReadAllLines(FisierAngajati))
                {
                    Angajat a = Angajat.FromString(linie);
                    if (a != null)
                        angajati.Add(a);
                }
            }

            return angajati;
        }
        public static bool ScrieAngajatiInFisier(List<Angajat> angajati)
        {
            try
            {
                if (angajati.Count > 0)
                {
                    using (StreamWriter writer = new StreamWriter(FisierAngajati, false))
                    {
                        foreach (var angajat in angajati)
                        {
                            writer.WriteLine(angajat.Info());
                        }
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la scrierea în fisier: {ex.Message}");
                return false;
            }
        }

        public int GetUltimulIdAngajat()
        {
            try
            {
                return File.Exists(numeFisier)
                    ? File.ReadLines(numeFisier)
                          .Select(linie => linie.Split(','))
                          .Where(date => date.Length > 0 && int.TryParse(date[0], out _))
                          .Select(date => int.Parse(date[0]))
                          .DefaultIfEmpty(0)
                          .Max()
                    : 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la citire ID: {ex.Message}");
                return 0;
            }
        }

        public static Angajat CreeazaAngajat(string nume, string dataNasteriiStr, string profesie,
                                           int vechime, string email, string statut)
        {
            try
            {
                if (!DateTime.TryParseExact(dataNasteriiStr, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                                          DateTimeStyles.None, out DateTime dataNasterii))
                {
                    Console.WriteLine("Format dată invalid. Folosind data curentă.");
                    dataNasterii = DateTime.Today;
                }

                if (!Enum.TryParse(statut, true, out StatutAngajat statutAngajat))
                {
                    Console.WriteLine("Statut invalid. Folosind statutul default (Subaltern).");
                    statutAngajat = StatutAngajat.Ofiter;
                }

                return new Angajat(nume, profesie,  vechime, dataNasterii,  email, statutAngajat);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la creare angajat: {ex.Message}");
                return null;
            }
        }
    }
}