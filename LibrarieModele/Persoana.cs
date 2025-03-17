using System;

namespace LibrarieModele
{
    public class Persoana
    {
        string nume { get; set; }
        DateTime dataNasterii { get; set; }

        public Persoana()
        {
            nume = string.Empty;
            this.dataNasterii = DateTime.MinValue;
        }

        public Persoana(string _nume, DateTime _dataNasterii)
        {
            nume = _nume;
            this.dataNasterii = _dataNasterii;
        }

        public virtual string Info()
        {
            return $"{nume},{dataNasterii:dd/MM/yyyy}";
        }

        public string GetNume() => nume;
    }
}
