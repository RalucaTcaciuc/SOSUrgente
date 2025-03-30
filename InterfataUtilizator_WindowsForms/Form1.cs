using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using LibrarieModele;
using NivelStocareDate;
using System.Configuration;

namespace InterfataUtilizator_WindowsForms
{
    public partial class Form1 : Form
    {
        private Administrare_angajati_FisierText adminAngajati;
        private string caleFisier;

        private Label lblTitlu;
        private Label lblNume, lblProfesie, lblVechime, lblDataNasterii, lblEmail, lblStatut;
        private Label[] lblsNume, lblsProfesie, lblsVechime, lblsDataNasterii, lblsEmail, lblsStatut;

        private const int LATIME_CONTROL = 120;
        private const int DIMENSIUNE_PAS_Y = 30;
        private const int DIMENSIUNE_PAS_X = 150;
        private const int OFFSET_VERTICAL = 80;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Evidența Angajaților";
            this.Width = 1000; // Mărit pentru a încăpea toate coloanele
            this.Height = 600;

            // Citirea configurației fișierului
            string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
            string locatieFisierSolutie = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            caleFisier = Path.Combine(locatieFisierSolutie, numeFisier);

            // Inițializare administrator angajați
            adminAngajati = new Administrare_angajati_FisierText(caleFisier);

            CreazaInterfata();
        }

        private void CreazaInterfata()
        {
            // Titlu
            lblTitlu = new Label
            {
                Text = "Lista Angajaților",
                Top = 10,
                Left = 50,
                AutoSize = true,
                Font = new Font("Arial", 14, FontStyle.Bold)
            };
            this.Controls.Add(lblTitlu);
            int topAntet = 10 + OFFSET_VERTICAL;
            // Antet tabel
            lblNume = new Label { Text = "Nume", Top = 50, Left = DIMENSIUNE_PAS_X, AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold) };
            lblProfesie = new Label { Text = "Profesie", Top = 50, Left = 2 * DIMENSIUNE_PAS_X, AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold) };
            lblVechime = new Label { Text = "Vechime", Top = 50, Left = 3 * DIMENSIUNE_PAS_X, AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold) };
            lblDataNasterii = new Label { Text = "Data Nașterii", Top = 50, Left = 4 * DIMENSIUNE_PAS_X, AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold) };
            lblEmail = new Label { Text = "Email", Top = 50, Left = 5 * DIMENSIUNE_PAS_X, AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold) };
            lblStatut = new Label { Text = "Statut", Top = 50, Left = 6 * DIMENSIUNE_PAS_X, AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold) };

            this.Controls.Add(lblNume);
            this.Controls.Add(lblProfesie);
            this.Controls.Add(lblVechime);
            this.Controls.Add(lblDataNasterii);
            this.Controls.Add(lblEmail);
            this.Controls.Add(lblStatut);
            AfiseazaAngajati();
        }

        private void AfiseazaAngajati()
        {

            var angajati = adminAngajati.GetAngajati(out int nrAngajati);

            if (nrAngajati == 0)
            {
                MessageBox.Show("Fișierul nu conține date valide!");
                return;
            }

            // Debug: afișează datele în consolă
            Console.WriteLine("Date citite din fișier:");
            foreach (var a in angajati)
            {
                Console.WriteLine($"{a.Nume}, {a.Profesie}, {a.Vechime}, {a.DataNasterii}, {a.Email}, {a.Statut}");
            }

            // Alocare memorie pentru etichete
            lblsNume = new Label[nrAngajati];
            lblsProfesie = new Label[nrAngajati];
            lblsVechime = new Label[nrAngajati];
            lblsDataNasterii = new Label[nrAngajati];
            lblsEmail = new Label[nrAngajati];
            lblsStatut = new Label[nrAngajati];

            // Adăugare date angajați
            for (int i = 0; i < nrAngajati; i++)
            {
                int topPosition = (i + 3) * DIMENSIUNE_PAS_Y;

                lblsNume[i] = new Label { Text = angajati[i].Nume, Top = topPosition, Left = DIMENSIUNE_PAS_X, AutoSize = true };
                lblsProfesie[i] = new Label { Text = angajati[i].Profesie, Top = topPosition, Left = 2 * DIMENSIUNE_PAS_X, AutoSize = true };
                lblsVechime[i] = new Label { Text = angajati[i].Vechime + " ani", Top = topPosition, Left = 3 * DIMENSIUNE_PAS_X, AutoSize = true };
                lblsDataNasterii[i] = new Label { Text = angajati[i].DataNasterii.ToString("dd/MM/yyyy"), Top = topPosition, Left = 4 * DIMENSIUNE_PAS_X, AutoSize = true };
                lblsEmail[i] = new Label { Text = angajati[i].Email, Top = topPosition, Left = 5 * DIMENSIUNE_PAS_X, AutoSize = true };
                lblsStatut[i] = new Label { Text = angajati[i].Statut.ToString(), Top = topPosition, Left = 6 * DIMENSIUNE_PAS_X, AutoSize = true };

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