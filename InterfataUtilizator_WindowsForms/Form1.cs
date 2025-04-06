using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using LibrarieModele;
using NivelStocareDate;
using System.Configuration;
using System.Globalization;

namespace InterfataUtilizator_WindowsForms
{
    public partial class Form1 : Form
    {
        private Administrare_angajati_FisierText adminAngajati;
        private string caleFisier;
        private Panel panelFundal;

        private Label lblTitlu;
        private Label lblNume, lblProfesie, lblVechime, lblDataNasterii, lblEmail, lblStatut;
        private Label[] lblsNume, lblsProfesie, lblsVechime, lblsDataNasterii, lblsEmail, lblsStatut;
        private TextBox txtNume, txtProfesie, txtVechime, txtDataNasterii, txtEmail, txtStatut;
        private Label lblErrorNume, lblErrorProfesie, lblErrorVechime, lblErrorDataNasterii, lblErrorEmail, lblErrorStatut;
        private Button btnAdauga;

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
                panelFundal.Left =300 ;
                panelFundal.Top = 40;
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

            int topStart = panelFundal.Bottom + 30;
            int formWidth = 500;
            int leftForm = 200;
            int labelWidth = 180;
            int textBoxWidth = 250;
            int spatiuY = 35;
            int currentTop = topStart;

            // Nume
            this.Controls.Add(new Label { Text = "Nume:", Top = currentTop, Left = leftForm, Width = labelWidth, TextAlign = ContentAlignment.MiddleRight });
            txtNume = new TextBox { Top = currentTop, Left = leftForm + labelWidth + 10, Width = textBoxWidth };
            this.Controls.Add(txtNume);
            lblErrorNume = new Label { Top = currentTop + 5, Left = leftForm + labelWidth + textBoxWidth + 15, ForeColor = Color.Red, AutoSize = true };
            this.Controls.Add(lblErrorNume);

            // Profesie
            currentTop += spatiuY;
            this.Controls.Add(new Label { Text = "Profesie:", Top = currentTop, Left = leftForm, Width = labelWidth, TextAlign = ContentAlignment.MiddleRight });
            txtProfesie = new TextBox { Top = currentTop, Left = leftForm + labelWidth + 10, Width = textBoxWidth };
            this.Controls.Add(txtProfesie);
            lblErrorProfesie = new Label { Top = currentTop + 5, Left = leftForm + labelWidth + textBoxWidth + 15, ForeColor = Color.Red, AutoSize = true };
            this.Controls.Add(lblErrorProfesie);

            // Vechime
            currentTop += spatiuY;
            this.Controls.Add(new Label { Text = "Vechime (ani):", Top = currentTop, Left = leftForm, Width = labelWidth, TextAlign = ContentAlignment.MiddleRight });
            txtVechime = new TextBox { Top = currentTop, Left = leftForm + labelWidth + 10, Width = textBoxWidth };
            this.Controls.Add(txtVechime);
            lblErrorVechime = new Label { Top = currentTop + 5, Left = leftForm + labelWidth + textBoxWidth + 15, ForeColor = Color.Red, AutoSize = true };
            this.Controls.Add(lblErrorVechime);

            // Data nașterii
            currentTop += spatiuY;
            this.Controls.Add(new Label { Text = "Data Nașterii (dd/MM/yyyy):", Top = currentTop, Left = leftForm, Width = labelWidth, TextAlign = ContentAlignment.MiddleRight });
            txtDataNasterii = new TextBox { Top = currentTop, Left = leftForm + labelWidth + 10, Width = textBoxWidth };
            this.Controls.Add(txtDataNasterii);
            lblErrorDataNasterii = new Label { Top = currentTop + 5, Left = leftForm + labelWidth + textBoxWidth + 15, ForeColor = Color.Red, AutoSize = true };
            this.Controls.Add(lblErrorDataNasterii);

            // Email
            currentTop += spatiuY;
            this.Controls.Add(new Label { Text = "Email:", Top = currentTop, Left = leftForm, Width = labelWidth, TextAlign = ContentAlignment.MiddleRight });
            txtEmail = new TextBox { Top = currentTop, Left = leftForm + labelWidth + 10, Width = textBoxWidth };
            this.Controls.Add(txtEmail);
            lblErrorEmail = new Label { Top = currentTop + 5, Left = leftForm + labelWidth + textBoxWidth + 15, ForeColor = Color.Red, AutoSize = true };
            this.Controls.Add(lblErrorEmail);

            // Statut
            currentTop += spatiuY;
            this.Controls.Add(new Label { Text = "Statut (Activ/Inactiv):", Top = currentTop, Left = leftForm, Width = labelWidth, TextAlign = ContentAlignment.MiddleRight });
            txtStatut = new TextBox { Top = currentTop, Left = leftForm + labelWidth + 10, Width = textBoxWidth };
            this.Controls.Add(txtStatut);
            lblErrorStatut = new Label { Top = currentTop + 5, Left = leftForm + labelWidth + textBoxWidth + 15, ForeColor = Color.Red, AutoSize = true };
            this.Controls.Add(lblErrorStatut);

            // Buton Adauga
            btnAdauga = new Button
            {
                Text = "Adaugă",
                Top = currentTop + spatiuY + 10,
                Left = leftForm + (formWidth - 100) / 2,
                Width = 100,
                Height = 30
            };
            btnAdauga.Click += BtnAdauga_Click;
            this.Controls.Add(btnAdauga);

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

            // Create a 2D array for the labels (each column for each employee)
            Label[,] labels = new Label[nrAngajati, 6];

            // Add employee data
            for (int i = 0; i < nrAngajati; i++)
            {
                int topPosition = 80 + (i * DIMENSIUNE_PAS_Y);

                // Nume
                labels[i, 0] = new Label
                {
                    Text = angajati[i].Nume,
                    Top = topPosition,
                    Left = DIMENSIUNE_PAS_X,
                    AutoSize = true,
                    Parent = panelFundal,
                    BackColor = Color.Transparent
                };

                // Profesie
                labels[i, 1] = new Label
                {
                    Text = angajati[i].Profesie,
                    Top = topPosition,
                    Left = 2 * DIMENSIUNE_PAS_X,
                    AutoSize = true,
                    Parent = panelFundal,
                    BackColor = Color.Transparent
                };

                // Vechime
                labels[i, 2] = new Label
                {
                    Text = angajati[i].Vechime + " ani",
                    Top = topPosition,
                    Left = 3 * DIMENSIUNE_PAS_X,
                    AutoSize = true,
                    Parent = panelFundal,
                    BackColor = Color.Transparent
                };

                // Data Nasterii
                labels[i, 3] = new Label
                {
                    Text = angajati[i].DataNasterii.ToString("dd/MM/yyyy"),
                    Top = topPosition,
                    Left = 4 * DIMENSIUNE_PAS_X,
                    AutoSize = true,
                    Parent = panelFundal,
                    BackColor = Color.Transparent
                };

                // Email
                labels[i, 4] = new Label
                {
                    Text = angajati[i].Email,
                    Top = topPosition,
                    Left = 5 * DIMENSIUNE_PAS_X,
                    AutoSize = true,
                    Parent = panelFundal,
                    BackColor = Color.Transparent
                };

                // Statut
                labels[i, 5] = new Label
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

        private void BtnAdauga_Click(object sender, EventArgs e)
        {
            ResetErrorLabels();

            bool isValid = ValidateFields();

            if (isValid)
            {
                try
                {
                    string nume = txtNume.Text;
                    string profesie = txtProfesie.Text;
                    int vechime = int.Parse(txtVechime.Text);
                    DateTime dataNasterii = DateTime.ParseExact(txtDataNasterii.Text, "dd/MM/yyyy", null);
                    string email = txtEmail.Text;
                    StatutAngajat statut = (StatutAngajat)Enum.Parse(typeof(StatutAngajat), txtStatut.Text, true);

                    Angajat angajatNou = new Angajat(nume, profesie, vechime, dataNasterii, email, statut);
                    adminAngajati.AdaugaAngajat(angajatNou);

                    MessageBox.Show("Angajat adăugat cu succes!");

                    // Curățare câmpuri
                    txtNume.Clear();
                    txtProfesie.Clear();
                    txtVechime.Clear();
                    txtDataNasterii.Clear();
                    txtEmail.Clear();
                    txtStatut.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Eroare: " + ex.Message);
                }
                AfiseazaAngajati();
            }
        }

        private bool ValidateFields()
        {
            bool isValid = true;

            // Validate Nume
            if (string.IsNullOrWhiteSpace(txtNume.Text) || txtNume.Text.Length > 15)
            {
                isValid = false;
                lblErrorNume.Text = "Nume invalid!";
            }

            // Validate Profesie
            if (string.IsNullOrWhiteSpace(txtProfesie.Text) || txtProfesie.Text.Length > 15)
            {
                isValid = false;
                lblErrorProfesie.Text = "Profesie invalidă!";
            }

            // Validate Vechime
            if (!int.TryParse(txtVechime.Text, out int vechime) || vechime < 0)
            {
                isValid = false;
                lblErrorVechime.Text = "Vechime invalidă!";
            }

            // Validate Data Nasterii
            if (!DateTime.TryParseExact(txtDataNasterii.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dataNasterii))
            {
                isValid = false;
                lblErrorDataNasterii.Text = "Data nașterii invalidă!";
            }

            // Validate Email
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                isValid = false;
                lblErrorEmail.Text = "Email invalid!";
            }

            // Validate Statut
            if (!Enum.TryParse(txtStatut.Text, true, out StatutAngajat statut))
            {
                isValid = false;
                lblErrorStatut.Text = "Statut invalid!";
            }

            return isValid;
        }

        private void ResetErrorLabels()
        {
            lblErrorNume.Text = string.Empty;
            lblErrorProfesie.Text = string.Empty;
            lblErrorVechime.Text = string.Empty;
            lblErrorDataNasterii.Text = string.Empty;
            lblErrorEmail.Text = string.Empty;
            lblErrorStatut.Text = string.Empty;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AfiseazaAngajati();
        }
    }
}