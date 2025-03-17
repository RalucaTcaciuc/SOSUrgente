//using LibrarieModele;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;
namespace LibrarieModele
{

    public class Angajat : Persoana
    {
        //List<Angajat> angajati = new List<Angajat>();
        private string profesie;
        private int vechime;
        private string email;

        public Angajat() : base()
        {
            profesie = string.Empty;
            vechime = 0;
            email = string.Empty;
        }

        public Angajat(string nume, DateTime dataNasterii, string profesie, int vechime, string email)
            : base(nume, dataNasterii)
        {
            this.profesie = profesie;
            this.vechime = vechime;
            this.email = email;
        }

        public string Info()
        {
            return base.Info() + $",{profesie},{vechime},{email}";
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

            if (parts.Length != 5) return null;

            string nume = parts[0];
            DateTime dataNasterii = DateTime.ParseExact(parts[1], "dd/MM/yyyy", null);
            string profesie = parts[2];
            int vechime = int.Parse(parts[3]);
            string email = parts[4];

            return new Angajat(nume, dataNasterii, profesie, vechime, email);
        }
       
    }
}
