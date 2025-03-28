using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LibrarieModele;
using NivelStocareDate;

namespace InterfataUtilizator_WindowsForms
{
    public partial class Form1 : Form
    {
        Administrare_angajati_FisierText adminAngajati;

        private Label lblTitlu;
        private Label lblNume, lblProfesie, lblVechime;
        private Label[] lblsNume, lblsProfesie, lblsVechime;
        private Label lblDataNasterii, lblEmail, lblStatut;
        private Label[] lblsDataNasterii, lblsEmail, lblsStatut;

        private const int LATIME_CONTROL = 120;
        private const int DIMENSIUNE_PAS_Y = 30;
        private const int DIMENSIUNE_PAS_X = 150;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Evidența Angajaților";
            this.Width = 600;
            this.Height = 400;

            string caleFisier = "angajati.txt";
            adminAngajati = new Administrare_angajati_FisierText(caleFisier);

            // Titlul
            lblTitlu = new Label
            {
                Text = "Lista Angajaților",
                Top = 10,
                Left = 50,
                AutoSize = true,
                Font = new Font("Arial", 14, FontStyle.Bold)
            };
            this.Controls.Add(lblTitlu);

            // Etichete coloane (titluri tabel)
            lblNume = new Label { Text = "Nume", Top = 50, Left = DIMENSIUNE_PAS_X, AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold) };
            lblProfesie = new Label { Text = "Profesie", Top = 50, Left = 2 * DIMENSIUNE_PAS_X, AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold) };
            lblVechime = new Label { Text = "Vechime", Top = 50, Left = 3 * DIMENSIUNE_PAS_X, AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold) };
            lblDataNasterii = new Label { Text = "Data Nașterii", Top = 50, Left = 4 * DIMENSIUNE_PAS_X, AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold) };
            lblEmail = new Label { Text = "Email", Top = 50, Left = 5 * DIMENSIUNE_PAS_X, AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold) };
            lblStatut = new Label { Text = "Statut", Top = 50, Left = 6 * DIMENSIUNE_PAS_X, AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold) };

            // Adăugare etichete în formular
            this.Controls.Add(lblNume);
            this.Controls.Add(lblProfesie);
            this.Controls.Add(lblVechime);
            this.Controls.Add(lblDataNasterii);
            this.Controls.Add(lblEmail);
            this.Controls.Add(lblStatut);

        }


        private void AfiseazaAngajati()
        {
            adminAngajati = new Administrare_angajati_FisierText("angajati.txt");
            Angajat[] angajati = adminAngajati.GetAngajati(out int nrAngajati);

            if (nrAngajati == 0)
            {
                MessageBox.Show("Nu există angajați în fișier!", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ștergere elemente vechi
            foreach (var lbl in this.Controls.OfType<Label>().Where(l => l.Top > 50).ToList())
            {
                this.Controls.Remove(lbl);
                lbl.Dispose();
            }

            // Definire array-uri pentru etichete dinamice
            lblsNume = new Label[nrAngajati];
            lblsProfesie = new Label[nrAngajati];
            lblsVechime = new Label[nrAngajati];
            lblsDataNasterii = new Label[nrAngajati];
            lblsEmail = new Label[nrAngajati];
            lblsStatut = new Label[nrAngajati];

            // Adăugare etichete pentru titluri
            Label lblDataNasterii = new Label { Text = "Data Nașterii", Top = 50, Left = 4 * DIMENSIUNE_PAS_X, AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold) };
            Label lblEmail = new Label { Text = "Email", Top = 50, Left = 5 * DIMENSIUNE_PAS_X, AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold) };
            Label lblStatut = new Label { Text = "Statut", Top = 50, Left = 6 * DIMENSIUNE_PAS_X, AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold) };

            this.Controls.Add(lblDataNasterii);
            this.Controls.Add(lblEmail);
            this.Controls.Add(lblStatut);

            for (int i = 0; i < nrAngajati; i++)
            {
                lblsNume[i] = new Label { Text = angajati[i].nume, Top = (i + 2) * DIMENSIUNE_PAS_Y, Left = DIMENSIUNE_PAS_X, AutoSize = true };
                lblsProfesie[i] = new Label { Text = angajati[i].profesie, Top = (i + 2) * DIMENSIUNE_PAS_Y, Left = 2 * DIMENSIUNE_PAS_X, AutoSize = true };
                lblsVechime[i] = new Label { Text = angajati[i].vechime + " ani", Top = (i + 2) * DIMENSIUNE_PAS_Y, Left = 3 * DIMENSIUNE_PAS_X, AutoSize = true };

                lblsDataNasterii[i] = new Label { Text = angajati[i].dataNasterii.ToString("dd/MM/yyyy"), Top = (i + 2) * DIMENSIUNE_PAS_Y, Left = 4 * DIMENSIUNE_PAS_X, AutoSize = true };
                lblsEmail[i] = new Label { Text = angajati[i].email, Top = (i + 2) * DIMENSIUNE_PAS_Y, Left = 5 * DIMENSIUNE_PAS_X, AutoSize = true };
                lblsStatut[i] = new Label { Text = angajati[i].statut.ToString(), Top = (i + 2) * DIMENSIUNE_PAS_Y, Left = 6 * DIMENSIUNE_PAS_X, AutoSize = true };

                this.Controls.Add(lblsNume[i]);
                this.Controls.Add(lblsProfesie[i]);
                this.Controls.Add(lblsVechime[i]);
                this.Controls.Add(lblsDataNasterii[i]);
                this.Controls.Add(lblsEmail[i]);
                this.Controls.Add(lblsStatut[i]);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            AfiseazaAngajati();
        }
    }
}
