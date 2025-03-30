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
        private Panel panelFundal; // Added panel reference

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
            this.Width = 1000;
            this.Height = 600;
            this.Resize += new EventHandler(Form1_Resize);

            // Citirea configurației fișierului
            string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
            string locatieFisierSolutie = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            caleFisier = Path.Combine(locatieFisierSolutie, numeFisier);

            // Inițializare administrator angajați
            adminAngajati = new Administrare_angajati_FisierText(caleFisier);

            CreazaInterfata();
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (panelFundal != null)
            {
                panelFundal.Left = (this.ClientSize.Width - panelFundal.Width) / 2;
                panelFundal.Top = (this.ClientSize.Height - panelFundal.Height) / 2;
            }
        }
        private void CreazaInterfata()
        {
            int latimeTabel = 6 * DIMENSIUNE_PAS_X + LATIME_CONTROL;
            int inaltimeTabel = 100 + (10 * DIMENSIUNE_PAS_Y);

            // Create blue background panel
            panelFundal = new Panel
            {
               // BackColor = Color.LightSkyBlue,
                Width = latimeTabel,
                Height = inaltimeTabel,
                Left = (this.ClientSize.Width - latimeTabel) / 2,
                Top = 20,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(panelFundal);
            panelFundal.SendToBack();

            // Create title label
            lblTitlu = new Label
            {
                Text = "LISTA ANGAJAȚILOR",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.LightSkyBlue,
                ForeColor = Color.Black,
                Font = new Font("Arial", 14, FontStyle.Bold),
                Parent = panelFundal
            };

            // Create headers
            int topAntet = 50;
            lblNume = new Label
            {
                Text = "Nume",
                Top = topAntet,
                Left = DIMENSIUNE_PAS_X,
                AutoSize = true,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Parent = panelFundal,
                BackColor = Color.Transparent
            };

            lblProfesie = new Label 
            { 
                Text = "Profesie", 
                Top = topAntet, 
                Left = 2 * DIMENSIUNE_PAS_X, 
                AutoSize = true, 
                Font = new Font("Arial", 12, FontStyle.Bold),
                Parent = panelFundal,
                BackColor = Color.Transparent
            };

            lblVechime = new Label 
            { 
                Text = "Vechime", 
                Top = topAntet, 
                Left = 3 * DIMENSIUNE_PAS_X, 
                AutoSize = true, 
                Font = new Font("Arial", 12, FontStyle.Bold),
                Parent = panelFundal,
                BackColor = Color.Transparent
            };

            lblDataNasterii = new Label 
            { 
                Text = "Data Nașterii", 
                Top = topAntet, 
                Left = 4 * DIMENSIUNE_PAS_X, 
                AutoSize = true, 
                Font = new Font("Arial", 12, FontStyle.Bold),
                Parent = panelFundal,
                BackColor = Color.Transparent
            };

            lblEmail = new Label 
            { 
                Text = "Email", 
                Top = topAntet, 
                Left = 5 * DIMENSIUNE_PAS_X, 
                AutoSize = true, 
                Font = new Font("Arial", 12, FontStyle.Bold),
                Parent = panelFundal,
                BackColor = Color.Transparent
            };

            lblStatut = new Label 
            { 
                Text = "Statut", 
                Top = topAntet, 
                Left = 6 * DIMENSIUNE_PAS_X, 
                AutoSize = true, 
                Font = new Font("Arial", 12, FontStyle.Bold),
                Parent = panelFundal,
                BackColor = Color.Transparent
            };

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

            // Clear existing data rows
            foreach (Control control in panelFundal.Controls.OfType<Label>().Where(l => l.Top > 80).ToList())
            {
                panelFundal.Controls.Remove(control);
            }

            // Initialize label arrays
            lblsNume = new Label[nrAngajati];
            lblsProfesie = new Label[nrAngajati];
            lblsVechime = new Label[nrAngajati];
            lblsDataNasterii = new Label[nrAngajati];
            lblsEmail = new Label[nrAngajati];
            lblsStatut = new Label[nrAngajati];

            // Add employee data
            for (int i = 0; i < nrAngajati; i++)
            {
                int topPosition = 80 + (i * DIMENSIUNE_PAS_Y);

                lblsNume[i] = new Label 
                { 
                    Text = angajati[i].Nume, 
                    Top = topPosition, 
                    Left = DIMENSIUNE_PAS_X, 
                    AutoSize = true,
                    Parent = panelFundal,
                    BackColor = Color.Transparent
                };

                lblsProfesie[i] = new Label 
                { 
                    Text = angajati[i].Profesie, 
                    Top = topPosition, 
                    Left = 2 * DIMENSIUNE_PAS_X, 
                    AutoSize = true,
                    Parent = panelFundal,
                    BackColor = Color.Transparent
                };

                lblsVechime[i] = new Label 
                { 
                    Text = angajati[i].Vechime + " ani", 
                    Top = topPosition, 
                    Left = 3 * DIMENSIUNE_PAS_X, 
                    AutoSize = true,
                    Parent = panelFundal,
                    BackColor = Color.Transparent
                };

                lblsDataNasterii[i] = new Label 
                { 
                    Text = angajati[i].DataNasterii.ToString("dd/MM/yyyy"), 
                    Top = topPosition, 
                    Left = 4 * DIMENSIUNE_PAS_X, 
                    AutoSize = true,
                    Parent = panelFundal,
                    BackColor = Color.Transparent
                };

                lblsEmail[i] = new Label 
                { 
                    Text = angajati[i].Email, 
                    Top = topPosition, 
                    Left = 5 * DIMENSIUNE_PAS_X, 
                    AutoSize = true,
                    Parent = panelFundal,
                    BackColor = Color.Transparent
                };

                lblsStatut[i] = new Label 
                { 
                    Text = angajati[i].Statut.ToString(), 
                    Top = topPosition, 
                    Left = 6 * DIMENSIUNE_PAS_X, 
                    AutoSize = true,
                    Parent = panelFundal,
                    BackColor = Color.Transparent
                };
            }

            // Resize panel to fit all employees
            panelFundal.Height = 100 + (nrAngajati * DIMENSIUNE_PAS_Y);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AfiseazaAngajati();
        }
    }
}