using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LibrarieModele

{
    public class Urgente
    {
        //const string FisierUrgente = "urgente.txt";
        public string oras { get; set; } = string.Empty;
        private string strada;
        private int nr;
        //string misiune;

        public Urgente()
        {
            oras = string.Empty;
            strada = string.Empty;
            nr = 1;
        }

        public Urgente(string oras, string strada, int nr)
        {
            if (nr <= 0)
                throw new ArgumentException("Numarul cladirii trebuie sa fie pozitiv.");

            this.oras = oras;
            this.strada = strada;
            this.nr = nr;
        }

        public string Info()
        {
            return $"{oras},{strada},{nr}";
        }
        public static Urgente FromString(string linie)
        {
            string[] parts = linie.Split(',');
            if (parts.Length != 3) return null;
            return new Urgente(parts[0], parts[1], int.Parse(parts[2]));
        }

      
    }
}