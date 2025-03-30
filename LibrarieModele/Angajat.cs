using System;
using System.Globalization;

namespace LibrarieModele
{
    [Flags]
    public enum StatutAngajat
    {
        Subofiter = 0,
        Ofiter = 1,
        Pensionar = 2,
        PersonalAdministrativ = 4
    }

    // Presupunem că Persoana este o clasă de bază cu o metodă Info()
    //public class Persoana
    //{
    //    //public virtual string Info()
    //    //{
    //    //    return "Informații persoană";
    //    //}
    //}

    public class Angajat //: Persoana
    {
        private StatutAngajat statutAngajat;

        public string Nume { get; set; }
        public string Profesie { get; set; }
        public int Vechime { get; set; }
        public DateTime DataNasterii { get; set; }
        public string Email { get; set; }
        public StatutAngajat Statut { get; set; }

        // Constructor pentru crearea unui Angajat
        public Angajat(string nume, string profesie, int vechime, DateTime dataNasterii, string email, StatutAngajat statut)
        {
            Nume = nume;
            Profesie = profesie;
            Vechime = vechime;
            DataNasterii = dataNasterii;
            Email = email;
            Statut = statut;
        }

        public Angajat(string nume, DateTime dataNasterii, string profesie, int vechime, string email, StatutAngajat statutAngajat)
        {
            Nume = nume;
            DataNasterii = dataNasterii;
            Profesie = profesie;
            Vechime = vechime;
            Email = email;
            this.statutAngajat = statutAngajat;
        }

        // Metoda Info pentru a returna informațiile angajatului
        public string Info()
        {
            return $"{Nume}, {Profesie}, {Vechime}, {DataNasterii.ToString("dd/MM/yyyy")}, {Email}, {Statut}";
        }

        public string GetNume()
        {
            return Nume;
        }


        public string GetProfesie()
        {
            return Profesie;
        }

        public static Angajat FromString(string linie)
        {
            try
            {
                string[] date = linie.Split(',');
                if (date.Length != 6) return null;

                // Parsare corectă a enum-ului
                StatutAngajat statut;
                if (!Enum.TryParse(date[5].Trim(), true, out statut))
                {
                    // Valoare implicită dacă parsarea eșuează
                    statut = StatutAngajat.Ofiter;
                }

                return new Angajat(
                    nume: date[0].Trim(),
                    dataNasterii: DateTime.ParseExact(date[3].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    profesie: date[1].Trim(),
                    vechime: int.Parse(date[2].Trim()),
                    email: date[4].Trim(),
                    statut: statut
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la parsare angajat: {ex.Message}");
                return null;
            }
        }
    }
}
