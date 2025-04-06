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
        private Panel panelFundal;

        private Label lblTitlu;
        private Label lblNume, lblProfesie, lblVechime, lblDataNasterii, lblEmail, lblStatut;
        private Label[] lblsNume, lblsProfesie, lblsVechime, lblsDataNasterii, lblsEmail, lblsStatut;
        private TextBox txtNume, txtProfesie, txtVechime, txtDataNasterii, txtEmail, txtStatut;
        private Button btnAdauga;

        private const int LATIME_CONTROL = 120;
        private const int DIMENSIUNE_PAS_Y = 30;
        private const int DIMENSIUNE_PAS_X = 150;
        private const int MARGINE_SUPERIOARA = 20;
        private const int SPAIU_INTRE_ELEMENTE = 30;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Evidența Angajaților";
            this.Width = 1000;
            this.Height = 700; // Mărim înălțimea inițială
            this.Resize += new EventHandler(Form1_Resize);

            string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
            string locatieFisierSolutie = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            caleFisier = Path.Combine(locatieFisierSolutie, numeFisier);

            adminAngajati = new Administrare_angajati_FisierText(caleFisier);

            CreazaInterfata();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (panelFundal != null)
            {
                panelFundal.Left = (this.ClientSize.Width - panelFundal.Width) / 2;
                // Panelul rămâne mereu în partea de sus
                panelFundal.Top = MARGINE_SUPERIOARA;

                // Repozitionăm elementele formularului sub panel
                RepositionFormElements();
            }
        }

        private void CreazaInterfata()
        {
            int latimeTabel = 6 * DIMENSIUNE_PAS_X + LATIME_CONTROL;
            int inaltimeTabel = 100 + (10 * DIMENSIUNE_PAS_Y);

            // Creăm panelul pentru lista de angajați
            panelFundal = new Panel
            {
                Width = latimeTabel,
                Height = inaltimeTabel,
                Left = (this.ClientSize.Width - latimeTabel) / 2,
                Top = MARGINE_SUPERIOARA,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(panelFundal);

            // Titlul panelului
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

            // Antetele tabelului
            int topAntet = 50;
            lblNume = new Label { Text = "Nume", Top = topAntet, Left = DIMENSIUNE_PAS_X, AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold), Parent = panelFundal };
            lblProfesie = new Label { Text = "Profesie", Top = topAntet, Left = 2 * DIMENSIUNE_PAS_X, AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold), Parent = panelFundal };
            lblVechime = new Label { Text = "Vechime", Top = topAntet, Left = 3 * DIMENSIUNE_PAS_X, AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold), Parent = panelFundal };
            lblDataNasterii = new Label { Text = "Data Nașterii", Top = topAntet, Left = 4 * DIMENSIUNE_PAS_X, AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold), Parent = panelFundal };
            lblEmail = new Label { Text = "Email", Top = topAntet, Left = 5 * DIMENSIUNE_PAS_X, AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold), Parent = panelFundal };
            lblStatut = new Label { Text = "Statut", Top = topAntet, Left = 6 * DIMENSIUNE_PAS_X, AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold), Parent = panelFundal };

            AfiseazaAngajati();

            // Creăm formularul de adăugare sub panel
            CreazaFormularAdaugare();
        }

        private void CreazaFormularAdaugare()
        {
            int topStart = panelFundal.Bottom + SPAIU_INTRE_ELEMENTE;
            int formWidth = 500;
            int leftForm = (this.ClientSize.Width - formWidth) / 2;
            int labelWidth = 180;
            int textBoxWidth = 250;
            int spatiuY = 35;
            int currentTop = topStart;

            // Grupăm toate elementele formularului într-un panel separat
            Panel panelFormular = new Panel
            {
                Top = topStart,
                Left = leftForm - 10,
                Width = formWidth + 20,
                Height = 300,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.WhiteSmoke
            };
            this.Controls.Add(panelFormular);

            // Adjustăm poziția relativă în interiorul noului panel
            currentTop = 20;

            // Nume
            Label lblNumeForm = new Label { Text = "Nume:", Top = currentTop, Left = 10, Width = labelWidth, TextAlign = ContentAlignment.MiddleRight };
            panelFormular.Controls.Add(lblNumeForm);
            txtNume = new TextBox { Top = currentTop, Left = labelWidth + 20, Width = textBoxWidth };
            panelFormular.Controls.Add(txtNume);

            // Profesie
            currentTop += spatiuY;
            Label lblProfesieForm = new Label { Text = "Profesie:", Top = currentTop, Left = 10, Width = labelWidth, TextAlign = ContentAlignment.MiddleRight };
            panelFormular.Controls.Add(lblProfesieForm);
            txtProfesie = new TextBox { Top = currentTop, Left = labelWidth + 20, Width = textBoxWidth };
            panelFormular.Controls.Add(txtProfesie);

            // Vechime
            currentTop += spatiuY;
            Label lblVechimeForm = new Label { Text = "Vechime (ani):", Top = currentTop, Left = 10, Width = labelWidth, TextAlign = ContentAlignment.MiddleRight };
            panelFormular.Controls.Add(lblVechimeForm);
            txtVechime = new TextBox { Top = currentTop, Left = labelWidth + 20, Width = textBoxWidth };
            panelFormular.Controls.Add(txtVechime);

            // Data nașterii
            currentTop += spatiuY;
            Label lblDataNasteriiForm = new Label { Text = "Data Nașterii (dd/MM/yyyy):", Top = currentTop, Left = 10, Width = labelWidth, TextAlign = ContentAlignment.MiddleRight };
            panelFormular.Controls.Add(lblDataNasteriiForm);
            txtDataNasterii = new TextBox { Top = currentTop, Left = labelWidth + 20, Width = textBoxWidth };
            panelFormular.Controls.Add(txtDataNasterii);

            // Email
            currentTop += spatiuY;
            Label lblEmailForm = new Label { Text = "Email:", Top = currentTop, Left = 10, Width = labelWidth, TextAlign = ContentAlignment.MiddleRight };
            panelFormular.Controls.Add(lblEmailForm);
            txtEmail = new TextBox { Top = currentTop, Left = labelWidth + 20, Width = textBoxWidth };
            panelFormular.Controls.Add(txtEmail);

            // Statut
            currentTop += spatiuY;
            Label lblStatutForm = new Label { Text = "Statut (Activ/Inactiv):", Top = currentTop, Left = 10, Width = labelWidth, TextAlign = ContentAlignment.MiddleRight };
            panelFormular.Controls.Add(lblStatutForm);
            txtStatut = new TextBox { Top = currentTop, Left = labelWidth + 20, Width = textBoxWidth };
            panelFormular.Controls.Add(txtStatut);

            // Buton Adauga
            btnAdauga = new Button
            {
                Text = "Adaugă",
                Top = currentTop + spatiuY,
                Left = (panelFormular.Width - 100) / 2,
                Width = 100,
                Height = 30
            };
            btnAdauga.Click += BtnAdauga_Click;
            panelFormular.Controls.Add(btnAdauga);

            // Actualizăm înălțimea panelului formularului
            panelFormular.Height = btnAdauga.Bottom + 20;

            // Actualizăm înălțimea totală a formularului
            this.Height = panelFormular.Bottom + 50;
        }

        private void RepositionFormElements()
        {
            // Găsim panelul formularului (ultimul panel adăugat)
            Panel panelFormular = this.Controls.OfType<Panel>().LastOrDefault();
            if (panelFormular != null)
            {
                // Repozitionăm panelul formularului sub panelul principal
                panelFormular.Top = panelFundal.Bottom + SPAIU_INTRE_ELEMENTE;
                panelFormular.Left = (this.ClientSize.Width - panelFormular.Width) / 2;

                // Actualizăm înălțimea totală a formularului
                this.Height = panelFormular.Bottom + 50;
            }
        }

        private void AfiseazaAngajati()
        {
            var angajati = adminAngajati.GetAngajati(out int nrAngajati);

            // Ștergem vechile etichete de date
            foreach (Control control in panelFundal.Controls.OfType<Label>().Where(l => l.Top > 80).ToList())
            {
                panelFundal.Controls.Remove(control);
            }

            if (nrAngajati == 0)
            {
                panelFundal.Height = 100; // Dimensiune minimă
                return;
            }

            // Adăugăm noile date
            for (int i = 0; i < nrAngajati; i++)
            {
                int topPosition = 80 + (i * DIMENSIUNE_PAS_Y);

                new Label { Text = angajati[i].Nume, Top = topPosition, Left = DIMENSIUNE_PAS_X, AutoSize = true, Parent = panelFundal };
                new Label { Text = angajati[i].Profesie, Top = topPosition, Left = 2 * DIMENSIUNE_PAS_X, AutoSize = true, Parent = panelFundal };
                new Label { Text = angajati[i].Vechime + " ani", Top = topPosition, Left = 3 * DIMENSIUNE_PAS_X, AutoSize = true, Parent = panelFundal };
                new Label { Text = angajati[i].DataNasterii.ToString("dd/MM/yyyy"), Top = topPosition, Left = 4 * DIMENSIUNE_PAS_X, AutoSize = true, Parent = panelFundal };
                new Label { Text = angajati[i].Email, Top = topPosition, Left = 5 * DIMENSIUNE_PAS_X, AutoSize = true, Parent = panelFundal };
                new Label { Text = angajati[i].Statut.ToString(), Top = topPosition, Left = 6 * DIMENSIUNE_PAS_X, AutoSize = true, Parent = panelFundal };
            }

            // Actualizăm înălțimea panelului
            panelFundal.Height = 100 + (nrAngajati * DIMENSIUNE_PAS_Y);

            // Repozitionăm formularul sub noul panel
            RepositionFormElements();
        }

        private void BtnAdauga_Click(object sender, EventArgs e)
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

                AfiseazaAngajati();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AfiseazaAngajati();
        }
    }
}