using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibrarieModele;

namespace SOSUrgente
{
    public class Administrare_urgente_FisierText
    {
        private const string FisierUrgente = "urgente.txt";
        private readonly string numeFisier;

        public Administrare_urgente_FisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;
            try
            {
                if (!File.Exists(numeFisier))
                {
                    File.Create(numeFisier).Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Eroare la crearea fisierului: {ex.Message}");
            }
        }

        public bool AddUrgenta(Urgente urgenta)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(numeFisier, true))
                {
                    writer.WriteLine(urgenta.Info());
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Urgente> GetUrgente(out int nrUrgente)
        {
            List<Urgente> urgente = new List<Urgente>();
            nrUrgente = 0;

            try
            {
                using (StreamReader reader = new StreamReader(numeFisier))
                {
                    string linieFisier;
                    while ((linieFisier = reader.ReadLine()) != null)
                    {
                        Urgente urgenta = Urgente.FromString(linieFisier);
                        if (urgenta != null)
                        {
                            urgente.Add(urgenta);
                            nrUrgente++;
                        }
                    }
                }
            }
            catch
            {
                // Se returnează lista goală în caz de eroare
            }

            return urgente;
        }

        public int GetUltimulIdUrgenta()
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
                        if (dateFisier.Length > 0 && int.TryParse(dateFisier[0], out int idUrgenta))
                        {
                            if (idUrgenta > idMax)
                            {
                                idMax = idUrgenta;
                            }
                        }
                    }
                }
            }
            catch
            {
                // Se returnează 0 în caz de eroare
            }

            return idMax;
        }

        public static List<Urgente> CitesteUrgenteDinFisier()
        {
            List<Urgente> urgente = new List<Urgente>();
            if (File.Exists(FisierUrgente))
            {
                foreach (var linie in File.ReadAllLines(FisierUrgente))
                {
                    Urgente u = Urgente.FromString(linie);
                    if (u != null) urgente.Add(u);
                }
            }
            return urgente;
        }

        public static bool ScrieUrgenteInFisier(List<Urgente> urgente)
        {
            try
            {
                if (urgente.Count > 0)
                {
                    using (StreamWriter writer = new StreamWriter(FisierUrgente, false))
                    {
                        foreach (var urgenta in urgente)
                        {
                            writer.WriteLine(urgenta.Info());
                        }
                    }
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static List<Urgente> CautaUrgenta(List<Urgente> urgente, string orasCautat)
        {
            if (string.IsNullOrWhiteSpace(orasCautat))
                return new List<Urgente>();

            return urgente.Where(u => u.Info().IndexOf(orasCautat, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }
    }
}
