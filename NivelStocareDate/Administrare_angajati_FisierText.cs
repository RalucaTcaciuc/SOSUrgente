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
        private string numeFisier;

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
            }
        }


        public bool AdaugaAngajat(Angajat angajat)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(numeFisier, true))
                {
                    writer.WriteLine(angajat.Info());
                }
                Console.WriteLine($"Angajat adăugat: {angajat.Info()}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la scriere: {ex.Message}");
                return false;
            }
        }


        public Angajat[] GetAngajati(out int nrAngajati)
        {
            Angajat[] angajati = new Angajat[NR_MAX_ANGAJATI];
            nrAngajati = 0;

            try
            {
                using (StreamReader reader = new StreamReader(numeFisier))
                {
                    string linieFisier;
                    while ((linieFisier = reader.ReadLine()) != null)
                    {
                        Angajat angajat = Angajat.FromString(linieFisier);
                        if (angajat != null)
                        {
                            angajati[nrAngajati] = angajat;
                            nrAngajati++;
                        }
                    }
                }
            }
            catch
            {
               
            }

            return angajati;
        }

        public int GetUltimulIdAngajat()
        {
            int idMax = 0;

            try
            {
                if (File.Exists(numeFisier))
                {
                    string[] linii = File.ReadAllLines(numeFisier);
                    foreach (var linie in linii)
                    {
                        var dateFisier = linie.Split(',');
                        if (dateFisier.Length > 0 && int.TryParse(dateFisier[0], out int idAngajat))
                        {
                            if (idAngajat > idMax)
                            {
                                idMax = idAngajat;
                            }
                        }
                    }
                }
            }
            catch
            {
               
            }

            return idMax;
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
                Console.WriteLine($"Eroare la scrierea în fișier: {ex.Message}");
                return false;
            }
        }



        public static Angajat CreeazaAngajat(string nume, string dataNasteriiStr, string profesie, int vechime, string email, string StatutAngajat)
        {
            if (DateTime.TryParseExact(dataNasteriiStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataNasterii))
            {
                return new Angajat(nume, dataNasterii, profesie, vechime, email, StatutAngajat);
            }
            return null;
        }
    }
}