using System;

namespace LibrarieModele
{
    public class Persoana
    {
        public string nume { get; set; }
        public DateTime dataNasterii { get; set; }

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
