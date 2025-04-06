using System;
using System.Drawing;
using System.IO;
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

        private TextBox txtNume, txtProfesie, txtVechime, txtDataNasterii, txtEmail, txtStatut;
        private Button btnAdauga;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Adăugare Angajat";
            this.Width = 600;
            this.Height = 400;

            // Configurare cale fișier
            string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
            string locatieFisierSolutie = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            caleFisier = Path.Combine(locatieFisierSolutie, numeFisier);

            adminAngajati = new Administrare_angajati_FisierText(caleFisier);

            CreazaInterfata();
        }

        private void CreazaInterfata()
        {
            int startTop = 30;
            int leftLabel = 50;
            int leftTextBox = leftLabel + 150;
            int pasY = 30;

            // Nume
            Controls.Add(new Label { Text = "Nume:", Top = startTop, Left = leftLabel, Width = 140 });
            txtNume = new TextBox { Top = startTop, Left = leftTextBox, Width = 200 };
            Controls.Add(txtNume);

            // Profesie
            Controls.Add(new Label { Text = "Profesie:", Top = startTop + pasY, Left = leftLabel, Width = 140 });
            txtProfesie = new TextBox { Top = startTop + pasY, Left = leftTextBox, Width = 200 };
            Controls.Add(txtProfesie);

            // Vechime
            Controls.Add(new Label { Text = "Vechime (ani):", Top = startTop + 2 * pasY, Left = leftLabel, Width = 140 });
            txtVechime = new TextBox { Top = startTop + 2 * pasY, Left = leftTextBox, Width = 200 };
            Controls.Add(txtVechime);

            // Data nașterii
            Controls.Add(new Label { Text = "Data Nașterii (dd/MM/yyyy):", Top = startTop + 3 * pasY, Left = leftLabel, Width = 140 });
            txtDataNasterii = new TextBox { Top = startTop + 3 * pasY, Left = leftTextBox, Width = 200 };
            Controls.Add(txtDataNasterii);

            // Email
            Controls.Add(new Label { Text = "Email:", Top = startTop + 4 * pasY, Left = leftLabel, Width = 140 });
            txtEmail = new TextBox { Top = startTop + 4 * pasY, Left = leftTextBox, Width = 200 };
            Controls.Add(txtEmail);

            // Statut
            Controls.Add(new Label { Text = "Statut:", Top = startTop + 5 * pasY, Left = leftLabel, Width = 140 });
            txtStatut = new TextBox { Top = startTop + 5 * pasY, Left = leftTextBox, Width = 200 };
            Controls.Add(txtStatut);

            // Buton Adauga
            btnAdauga = new Button
            {
                Text = "Adaugă",
                Top = startTop + 6 * pasY + 10,
                Left = leftTextBox,
                Width = 100
            };
            btnAdauga.Click += BtnAdauga_Click;
            Controls.Add(btnAdauga);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);
            }
        }
    }
}
