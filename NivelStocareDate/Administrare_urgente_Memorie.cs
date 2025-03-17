using System;
using System.Collections.Generic;
using LibrarieModele;
namespace SituatiiUrgenta
{
    public class Administrare_urgente_Memorie
    {

        private const int NR_MAX_URGENTE = 50;
        private Urgente[] urgente;
        private int nrUrgente;

        public Administrare_urgente_Memorie()
        {
            urgente = new Urgente[NR_MAX_URGENTE];
            nrUrgente = 0;
        }

        public void AddUrgenta(Urgente urgenta)
        {
            if (nrUrgente < NR_MAX_URGENTE)
            {
                urgente[nrUrgente] = urgenta;
                nrUrgente++;
            }
            //else
            //{
            //    Console.WriteLine("Nu mai este loc pentru alte urgente.");
            //}
        }

        public Urgente[] GetUrgente(out int nrUrgente)
        {
            nrUrgente = this.nrUrgente;
            return urgente;
        }
    }
}
