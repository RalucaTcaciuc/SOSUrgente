using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using LibrarieModele;

namespace NivelStocareDate
{
    public class Administrare_angajati_Memorie
    {
        private const int NR_MAX_ANGAJATI = 50;
        private Angajat[] angajati;
        private int nrAngajati;

        public Administrare_angajati_Memorie()
        {
            angajati = new Angajat[NR_MAX_ANGAJATI];
            nrAngajati = 0;
        }

        public bool AddAngajat(Angajat angajat)
        {
            if (nrAngajati < NR_MAX_ANGAJATI)
            {
                angajati[nrAngajati] = angajat;
                nrAngajati++;
                return true;
            }
            return false; 
        }

        public Angajat[] GetAngajati(out int nrAngajati)
        {
            nrAngajati = this.nrAngajati;
            return angajati;
        }

        public static List<Angajat> CautaAngajatDupaNume(List<Angajat> angajati, string numeCautat)
        {
            return angajati
                .Where(a => a.GetNume() != null &&
                            a.GetNume().Trim().ToLower().Contains(numeCautat.Trim().ToLower()))
                .ToList();
        }

        public static List<Angajat> CautaAngajatDupaSpecializare(List<Angajat> angajati, string specializareCautata)
        {
            return angajati
                .Where(a => a.GetProfesie() != null &&
                            a.GetProfesie().Trim().ToLower().Contains(specializareCautata.Trim().ToLower()))
                .ToList();
        }
    }
}
