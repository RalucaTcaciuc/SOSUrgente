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

    public class Angajat : Persoana
    {
        private string profesie;
        private int vechime;
        private string email;
        public StatutAngajat statut;

    
        public Angajat() : base()
        {
            profesie = string.Empty;
            vechime = 0;
            email = string.Empty;
            statut = StatutAngajat.Subofiter; 
        }

      
        public Angajat(string nume, DateTime dataNasterii, string profesie, int vechime, string email, StatutAngajat statut)
            : base(nume, dataNasterii)
        {
            this.profesie = profesie;
            this.vechime = vechime;
            this.email = email;
            this.statut = statut; 
        }

      
        public string Info()
        {
            return base.Info() + $",{profesie},{vechime},{email},{statut}";
        }
        
        public string GetNume()
        {
            return base.Info().Split(',')[0];
        }


        public string GetProfesie()
        {
            return profesie;
        }

        public static Angajat FromString(string data)
        {
            string[] parts = data.Split(',');

            if (parts.Length != 6) return null; 

            string nume = parts[0];
            string[] formatePosibile = { "dd/MM/yyyy", "MM/dd/yyyy", "yyyy-MM-dd" };
            if (!DateTime.TryParseExact(parts[1].Trim(), formatePosibile, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataNasterii))
            {
                Console.WriteLine($"Eroare: Data '{parts[1]}' nu este într-un format valid.");
                return null;
            }

            string profesie = parts[2];
            int vechime = int.Parse(parts[3]);
            string email = parts[4];
            StatutAngajat statut = (StatutAngajat)Enum.Parse(typeof(StatutAngajat), parts[5]); 

            return new Angajat(nume, dataNasterii, profesie, vechime, email, statut);
        }
    }
}
