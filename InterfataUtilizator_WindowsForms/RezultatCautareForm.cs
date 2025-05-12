using LibrarieModele;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterfataUtilizator_WindowsForms
{
    public partial class RezultatCautareForm: Form
    {
        List<Angajat> angajatiGasiti;
        public RezultatCautareForm(List<Angajat> angajatiGasiti)
        {
            this.angajatiGasiti = angajatiGasiti;
            InitializeComponent();
        }

        private void RezultatCautareForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = angajatiGasiti.Select(u => new
            {
                u.Nume,
                u.Profesie,
                u.Vechime,
                u.DataNasterii,
                u.Email,
                u.Statut
            }).ToList();
        }
    }
}
