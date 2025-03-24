using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LibrarieModele

{
    public enum TipUrgente
    {
        Accident=1,
        Incendiu = 2,
        Inundatie=3,
        Criminalitate=4
    }
    public class Urgente
    {
        
        public string oras { get; set; } = string.Empty;
        private string strada;
        private int nr;
        public TipUrgente tipUrgente { get; set; }

        public Urgente()
        {
            oras = string.Empty;
            strada = string.Empty;
            nr = 1;
            tipUrgente = TipUrgente.Accident;
        }

        public Urgente(string oras, string strada, int nr, TipUrgente tipUrgente)
        {
            if (nr <= 0)
                throw new ArgumentException("Numarul cladirii trebuie sa fie pozitiv.");

            this.oras = oras;
            this.strada = strada;
            this.nr = nr;
            this.tipUrgente = tipUrgente;
        }

        public string Info()
        {
            return $"{oras},{strada},{nr}, {tipUrgente}";
        }
        public static Urgente FromString(string linie)
        {
            string[] parts = linie.Split(',');
            if (parts.Length != 4) return null;
            if (Enum.TryParse(parts[3], out TipUrgente tipUrgenta))  // Verifică dacă tipul urgenței este valid
            {
                return new Urgente(parts[0], parts[1], int.Parse(parts[2]), tipUrgenta);
            }
            return null;
        }

      
    }
}