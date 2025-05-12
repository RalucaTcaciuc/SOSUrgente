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
        // Database and file management
        private Administrare_angajati_FisierText adminAngajati;
        private string caleFisier;

        // UI Containers
        private Panel panelPrincipal;
        private Panel panelContainerLista;
        private Panel panelFundal;
        private Panel panelAdaugare;
        private Panel panelCautare;
        private Panel panelStatut;

        // Labels
        private Label lblTitlu;
        //private Label lblNume, lblProfesie, lblVechime, lblDataNasterii, lblEmail, lblStatut;
        private Label lblErrorNume, lblErrorProfesie, lblErrorVechime, lblErrorDataNasterii, lblErrorEmail, lblErrorStatut;

        // TextBoxes
        private TextBox txtNume, txtProfesie, txtVechime, txtDataNasterii, txtEmail;
        private TextBox txtSearchNume, txtSearchProfesie;

        // Radio Buttons
        private RadioButton rbSubofiter, rbOfiter, rbPensionar, rbPersonalAdministrativ;

        // Buttons
        private Button btnAdauga;
        private Button btnSearchNume, btnSearchProfesie, btnResetSearch;

        // Constants
        private const int DIMENSIUNE_PAS_Y = 22;
        private const int INALTIME_PANOU_LISTA = 200;
        private const int LATIME_CONTROL = 150; // Mărit de la 120
        private const int DIMENSIUNE_PAS_X = 180; // Mărit de la 150
        private const int LATIME_PANOU = 1100; // Mărit de la 950

        private Panel selectedRow;
        private Angajat selectedAngajat;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Evidența Angajaților";
            this.Width = 1200; // Mărit de la 1000
            this.Height = 700;
            this.Resize += Form1_Resize;
            InitializeApp();
        }

        private void InitializeApp()
        {
            this.Text = "Evidența Angajaților";
            this.Width = 1000;
            this.Height = 700;
            this.Resize += Form1_Resize;

            // Citire configurație
            string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
            string locatieFisierSolutie = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            caleFisier = Path.Combine(locatieFisierSolutie, numeFisier);

            adminAngajati = new Administrare_angajati_FisierText(caleFisier);
            CreazaInterfata();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (panelContainerLista != null)
            {
                panelContainerLista.Left = (this.ClientSize.Width - panelContainerLista.Width) / 2;
            }
        }

        private void CreazaInterfata()
        {
            // Panel principal
            panelPrincipal = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                AutoScroll = true
            };
            this.Controls.Add(panelPrincipal);

            CreazaListaAngajati();
            CreazaPanouAdaugare();
            CreazaPanouCautare();
        }

        private void CreazaListaAngajati()
        {
            // Panel pentru lista de angajați (cu scroll)
            panelContainerLista = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Width = LATIME_PANOU,
                Height = INALTIME_PANOU_LISTA,
                Top = 20,
                Left = (this.ClientSize.Width - LATIME_PANOU) / 2,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                AutoScroll = true
            };
            panelPrincipal.Controls.Add(panelContainerLista);

            // Panel interior pentru conținutul listei
            panelFundal = new Panel
            {
                Width = LATIME_PANOU - 20,
                Height = 70,
                Top = 0,
                Left = 0,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            panelContainerLista.Controls.Add(panelFundal);

            // Titlu și antet
            lblTitlu = new Label
            {
                Text = "LISTA ANGAJAȚILOR",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 20,
                BackColor = Color.LightSkyBlue,
                ForeColor = Color.Black,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Parent = panelFundal
            };

            // Antet tabel cu dimensiuni ajustate
            int topAntet = 35;
            CreateHeaderLabel("Nume", DIMENSIUNE_PAS_X, topAntet, 180);
            CreateHeaderLabel("Profesie", 2 * DIMENSIUNE_PAS_X, topAntet, 150);
            CreateHeaderLabel("Vechime", 3 * DIMENSIUNE_PAS_X, topAntet, 100);
            CreateHeaderLabel("Data Nașterii", 4 * DIMENSIUNE_PAS_X, topAntet, 150);
            CreateHeaderLabel("Email", 5 * DIMENSIUNE_PAS_X, topAntet, 220);
            CreateHeaderLabel("Statut", 6 * DIMENSIUNE_PAS_X, topAntet, 100);

            AfiseazaAngajati();
        }

        private void CreazaPanouAdaugare()
        {
            // Initialize panelAdaugare
            panelAdaugare = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Width = LATIME_PANOU,
                Height = 500,
                Top = panelContainerLista.Bottom + 20,
                Left = panelContainerLista.Left,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            panelPrincipal.Controls.Add(panelAdaugare);

            // Adaugă câmpuri
            int leftForm = 100; // Schimbat de la 10 la 100
            int labelWidth = 150;
            int textBoxWidth = 200;
            int spatiuY = 30;
            int currentTop = 10;

            AdaugaCamp("Nume:", out txtNume, out lblErrorNume, leftForm, labelWidth, textBoxWidth, ref currentTop, spatiuY, panelAdaugare);
            AdaugaCamp("Profesie:", out txtProfesie, out lblErrorProfesie, leftForm, labelWidth, textBoxWidth, ref currentTop, spatiuY, panelAdaugare);
            AdaugaCamp("Vechime (ani):", out txtVechime, out lblErrorVechime, leftForm, labelWidth, textBoxWidth, ref currentTop, spatiuY, panelAdaugare);
            AdaugaCamp("Data Nașterii (dd/MM/yyyy):", out txtDataNasterii, out lblErrorDataNasterii, leftForm, labelWidth, textBoxWidth, ref currentTop, spatiuY, panelAdaugare);
            AdaugaCamp("Email:", out txtEmail, out lblErrorEmail, leftForm, labelWidth, textBoxWidth, ref currentTop, spatiuY, panelAdaugare);

            // Panel pentru radio button-uri (Statut)
            panelStatut = new Panel
            {
                Top = currentTop,
                Left = leftForm + labelWidth + 10,
                Width = textBoxWidth + 60,
                Height = 120, // Mărit de la 100 la 120
                BorderStyle = BorderStyle.None
            };
            panelAdaugare.Controls.Add(panelStatut);

            // Add status error label
            lblErrorStatut = new Label
            {
                Top = currentTop,
                Left = leftForm + labelWidth + textBoxWidth + 80,
                ForeColor = Color.Red,
                AutoSize = true
            };
            panelAdaugare.Controls.Add(lblErrorStatut);

            // Adăugăm radio button-uri pentru fiecare statut
            rbSubofiter = new RadioButton
            {
                Text = "Subofițer",
                Tag = StatutAngajat.Subofiter,
                Top = 10,
                Left = 10,
                Width = 150
            };
            rbOfiter = new RadioButton
            {
                Text = "Ofițer",
                Tag = StatutAngajat.Ofiter,
                Top = 35,
                Left = 10,
                Width = 150
            };
            rbPensionar = new RadioButton
            {
                Text = "Pensionar",
                Tag = StatutAngajat.Pensionar,
                Top = 60,
                Left = 10,
                Width = 150
            };
            rbPersonalAdministrativ = new RadioButton
            {
                Text = "Personal Administrativ",
                Tag = StatutAngajat.PersonalAdministrativ,
                Top = 85,
                Left = 10,
                Width = 200,
                AutoSize = true // Adăugat pentru a asigura că tot textul este vizibil
            };

            panelStatut.Controls.Add(rbSubofiter);
            panelStatut.Controls.Add(rbOfiter);
            panelStatut.Controls.Add(rbPensionar);
            panelStatut.Controls.Add(rbPersonalAdministrativ);

            currentTop += 120; // Actualizare top pentru butonul de adăugare

            btnAdauga = new Button
            {
                Text = "Adaugă",
                Top = currentTop + 10,
                Left = (LATIME_PANOU - 100) / 2 - 200, // Ajustat
                Width = 100,
                Height = 30,
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnAdauga.Click += BtnAdauga_Click;
            panelAdaugare.Controls.Add(btnAdauga);
        }

        private Panel GetPanelAdaugare()
        {
            if (panelAdaugare == null)
            {
                CreazaPanouAdaugare();
            }
            return panelAdaugare;
        }

        private void AdaugaCamp(string labelText, out TextBox textBox, out Label errorLabel, int leftForm, int labelWidth, int textBoxWidth, ref int currentTop, int spatiuY, Panel panelAdaugare)
        {

            // Add label
            panelAdaugare.Controls.Add(new Label
            {
                Text = labelText,
                Top = currentTop,
                Left = leftForm,
                Width = labelWidth,
                TextAlign = ContentAlignment.MiddleRight
            });

            // Create and add TextBox
            textBox = new TextBox
            {
                Top = currentTop,
                Left = leftForm + labelWidth + 10,
                Width = textBoxWidth
            };
            panelAdaugare.Controls.Add(textBox);

            // Create and add error Label
            errorLabel = new Label
            {
                Top = currentTop,
                Left = leftForm + labelWidth + textBoxWidth + 20,
                ForeColor = Color.Red,
                AutoSize = true
            };
            panelAdaugare.Controls.Add(errorLabel);

            // Update the current top position for the next control
            currentTop += spatiuY + textBox.Height;
        }

        private void AdaugaCampCautare(string labelText, out TextBox textBox, out Button button, int searchLeft, ref int searchTop)
        {
            panelCautare.Controls.Add(new Label
            {
                Text = labelText,
                Top = searchTop,
                Left = searchLeft,
                Width = 120
            });

            textBox = new TextBox
            {
                Top = searchTop,
                Left = searchLeft + 125,
                Width = 150
            };
            panelCautare.Controls.Add(textBox);

            button = new Button
            {
                Text = "Caută",
                Top = searchTop,
                Left = searchLeft + 280,
                Width = 80,
                BackColor = Color.LightGray,
                FlatStyle = FlatStyle.Flat
            };
            panelCautare.Controls.Add(button);

            searchTop += 30;
        }

        private void CreateHeaderLabel(string text, int left, int top, int width)
        {
            var label = new Label
            {
                Text = text,
                Top = top,
                Left = left,
                Width = width,
                Font = new Font("Arial", 9, FontStyle.Bold),
                BackColor = Color.LightSkyBlue,
                Parent = panelFundal,
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private void AfiseazaAngajati()
        {
            var angajati = adminAngajati.GetAngajati(out int nrAngajati);

            // Șterge rândurile existente
            foreach (Control control in panelFundal.Controls.OfType<Label>().Where(l => l.Top > 50).ToList())
            {
                panelFundal.Controls.Remove(control);
            }

            if (nrAngajati == 0)
            {
                panelFundal.Height = 70;
                return;
            }

            // Adaugă rânduri cu dimensiuni ajustate
            for (int i = 0; i < nrAngajati; i++)
            {
                int topPosition = 60 + (i * DIMENSIUNE_PAS_Y);

                CreateDataLabel(angajati[i].Nume, DIMENSIUNE_PAS_X, topPosition, i, 180);
                CreateDataLabel(angajati[i].Profesie, 2 * DIMENSIUNE_PAS_X, topPosition, i, 150);
                CreateDataLabel(angajati[i].Vechime + " ani", 3 * DIMENSIUNE_PAS_X, topPosition, i, 100);
                CreateDataLabel(angajati[i].DataNasterii.ToString("dd/MM/yyyy"), 4 * DIMENSIUNE_PAS_X, topPosition, i, 150);
                CreateDataLabel(angajati[i].Email, 5 * DIMENSIUNE_PAS_X, topPosition, i, 220);
                CreateDataLabel(angajati[i].Statut.ToString(), 6 * DIMENSIUNE_PAS_X, topPosition, i, 100);
            }

            // Ajustează înălțimea panel-ului
            panelFundal.Height = 60 + (nrAngajati * DIMENSIUNE_PAS_Y);
            panelContainerLista.AutoScroll = panelFundal.Height > panelContainerLista.Height;
        }

        private void CreateDataLabel(string text, int left, int top, int rowIndex, int width = 0)
        {
            // Create a panel for the entire row if it doesn't exist
            Panel rowPanel = panelFundal.Controls.OfType<Panel>()
                .FirstOrDefault(p => p.Top == top) as Panel;

            if (rowPanel == null)
            {
                rowPanel = new Panel
                {
                    Top = top,
                    Left = 0,
                    Width = LATIME_PANOU - 20,
                    Height = DIMENSIUNE_PAS_Y,
                    BackColor = rowIndex % 2 == 0 ? Color.White : Color.Lavender,
                    Tag = rowIndex,
                    Cursor = Cursors.Hand // Add hand cursor to indicate clickability
                };
                rowPanel.Click += RowPanel_Click;
                panelFundal.Controls.Add(rowPanel);
            }

            var label = new Label
            {
                Text = text,
                Top = 0,
                Left = left,
                Width = width > 0 ? width : LATIME_CONTROL,
                Height = DIMENSIUNE_PAS_Y,
                Font = new Font("Arial", 9),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true,
                Cursor = Cursors.Hand // Add hand cursor to indicate clickability
            };
            label.Click += (s, e) => RowPanel_Click(rowPanel, e);
            rowPanel.Controls.Add(label);
        }

        private void BtnAdauga_Click(object sender, EventArgs e)
        {
            ResetErrorLabels();

            if (ValidateFields())
            {
                try
                {
                    // Get selected status
                    StatutAngajat statutSelectat = StatutAngajat.Ofiter; // Default value
                    if (rbSubofiter.Checked) statutSelectat = StatutAngajat.Subofiter;
                    else if (rbOfiter.Checked) statutSelectat = StatutAngajat.Ofiter;
                    else if (rbPensionar.Checked) statutSelectat = StatutAngajat.Pensionar;
                    else if (rbPersonalAdministrativ.Checked) statutSelectat = StatutAngajat.PersonalAdministrativ;

                    if (selectedAngajat != null)
                    {
                        // Update existing employee
                        var angajati = adminAngajati.GetAngajati(out _).ToList();
                        int index = angajati.FindIndex(a => 
                            a.Nume == selectedAngajat.Nume && 
                            a.DataNasterii == selectedAngajat.DataNasterii);

                        if (index != -1)
                        {
                            // Create updated employee
                            angajati[index] = new Angajat(
                                txtNume.Text.Trim(),
                                txtProfesie.Text.Trim(),
                                int.Parse(txtVechime.Text),
                                DateTime.ParseExact(txtDataNasterii.Text, "dd/MM/yyyy", null),
                                txtEmail.Text.Trim(),
                                statutSelectat
                            );

                            // Save all employees back to file
                            if (Administrare_angajati_FisierText.ScrieAngajatiInFisier(angajati))
                            {
                                MessageBox.Show("Angajat actualizat cu succes!");
                                ClearFormFields();
                                AfiseazaAngajati();
                            }
                            else
                            {
                                MessageBox.Show("Eroare la actualizarea angajatului!");
                            }
                        }
                    }
                    else
                    {
                        // Add new employee
                        Angajat angajatNou = new Angajat(
                            txtNume.Text.Trim(),
                            txtProfesie.Text.Trim(),
                            int.Parse(txtVechime.Text),
                            DateTime.ParseExact(txtDataNasterii.Text, "dd/MM/yyyy", null),
                            txtEmail.Text.Trim(),
                            statutSelectat
                        );

                        if (adminAngajati.AdaugaAngajat(angajatNou))
                        {
                            MessageBox.Show("Angajat adăugat cu succes!");
                            ClearFormFields();
                            AfiseazaAngajati();
                        }
                        else
                        {
                            MessageBox.Show("Eroare la salvarea angajatului!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Eroare: {ex.Message}");
                }
            }
        }

        private bool ValidateFields()
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(txtNume.Text) || txtNume.Text.Length > 15)
            {
                isValid = false;
                lblErrorNume.Text = "Nume invalid!";
            }

            if (string.IsNullOrWhiteSpace(txtProfesie.Text) || txtProfesie.Text.Length > 15)
            {
                isValid = false;
                lblErrorProfesie.Text = "Profesie invalidă!";
            }

            if (!int.TryParse(txtVechime.Text, out int vechime) || vechime < 0)
            {
                isValid = false;
                lblErrorVechime.Text = "Vechime invalidă!";
            }

            if (!DateTime.TryParseExact(txtDataNasterii.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out _))
            {
                isValid = false;
                lblErrorDataNasterii.Text = "Data nașterii invalidă!";
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                isValid = false;
                lblErrorEmail.Text = "Email invalid!";
            }

            // Verifică dacă un statut a fost selectat
            bool isStatutSelected = rbSubofiter.Checked || rbOfiter.Checked || 
                                  rbPensionar.Checked || rbPersonalAdministrativ.Checked;

            if (!isStatutSelected)
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

        private void ClearFormFields()
        {
            txtNume.Clear();
            txtProfesie.Clear();
            txtVechime.Clear();
            txtDataNasterii.Clear();
            txtEmail.Clear();
            
            rbSubofiter.Checked = false;
            rbOfiter.Checked = false;
            rbPensionar.Checked = false;
            rbPersonalAdministrativ.Checked = false;

            selectedAngajat = null;
            if (selectedRow != null)
            {
                selectedRow.BackColor = ((int)selectedRow.Tag % 2 == 0) ? Color.White : Color.Lavender;
                selectedRow = null;
            }
            btnAdauga.Text = "Adaugă";
        }

        private void CautaDupaNume(string numeCautat)
        {
            var angajati = adminAngajati.GetAngajati(out _);
            var angajatiGasiti = Administrare_angajati_Memorie.CautaAngajatDupaNume(angajati, numeCautat);
            AfiseazaRezultateCautare(angajatiGasiti);
        }

        private void CautaDupaProfesie(string profesieCautata)
        {
            var angajati = adminAngajati.GetAngajati(out _);
            var angajatiGasiti = Administrare_angajati_Memorie.CautaAngajatDupaSpecializare(angajati, profesieCautata);
            AfiseazaRezultateCautare(angajatiGasiti);
        }

        private void AfiseazaRezultateCautare(List<Angajat> angajatiGasiti)
        {
            // Clear existing data rows
            foreach (Control control in panelFundal.Controls.OfType<Label>().Where(l => l.Top > 50).ToList())
            {
                panelFundal.Controls.Remove(control);
            }

            if (angajatiGasiti.Count == 0)
            {
                MessageBox.Show("Nu s-au găsit angajați care să corespundă criteriului de căutare.");
                panelFundal.Height = 70;
                return;
            }

            for (int i = 0; i < angajatiGasiti.Count; i++)
            {
                int topPosition = 60 + (i * DIMENSIUNE_PAS_Y);

                CreateDataLabel(angajatiGasiti[i].Nume, DIMENSIUNE_PAS_X, topPosition, i);
                CreateDataLabel(angajatiGasiti[i].Profesie, 2 * DIMENSIUNE_PAS_X, topPosition, i);
                CreateDataLabel(angajatiGasiti[i].Vechime + " ani", 3 * DIMENSIUNE_PAS_X, topPosition, i);
                CreateDataLabel(angajatiGasiti[i].DataNasterii.ToString("dd/MM/yyyy"), 4 * DIMENSIUNE_PAS_X, topPosition, i);
                CreateDataLabel(angajatiGasiti[i].Email, 5 * DIMENSIUNE_PAS_X, topPosition, i);
                CreateDataLabel(angajatiGasiti[i].Statut.ToString(), 6 * DIMENSIUNE_PAS_X, topPosition, i);
            }

            panelFundal.Height = 60 + (angajatiGasiti.Count * DIMENSIUNE_PAS_Y);
            panelContainerLista.AutoScroll = panelFundal.Height > panelContainerLista.Height;
        }

        private void CreazaPanouCautare()
        {
            // Panel pentru căutare
            panelCautare = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Width = LATIME_PANOU,
                Height = 120,
                Top = panelAdaugare.Bottom + 20,
                Left = panelContainerLista.Left,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            panelPrincipal.Controls.Add(panelCautare);

            // Controale căutare
            int searchTop = 20;
            int searchLeft = 100;

            // Câmpuri de căutare
            AdaugaCampCautare("Caută după nume:", out txtSearchNume, out btnSearchNume, searchLeft, ref searchTop);
            AdaugaCampCautare("Caută după profesie:", out txtSearchProfesie, out btnSearchProfesie, searchLeft, ref searchTop);

            btnSearchNume.Click += (s, e) => CautaDupaNume(txtSearchNume.Text);
            btnSearchProfesie.Click += (s, e) => CautaDupaProfesie(txtSearchProfesie.Text);

            // Buton resetare
            btnResetSearch = new Button
            {
                Text = "Resetează căutarea",
                Top = searchTop + 10,
                Left = searchLeft,
                Width = 200,
                BackColor = Color.LightGray,
                FlatStyle = FlatStyle.Flat
            };
            btnResetSearch.Click += (s, e) => AfiseazaAngajati();
            panelCautare.Controls.Add(btnResetSearch);
        }

        private void RowPanel_Click(object sender, EventArgs e)
        {
            var panel = sender as Panel;
            if (panel == null) return;

            // Get the employee data
            var angajati = adminAngajati.GetAngajati(out _);
            int rowIndex = (int)panel.Tag;
            if (rowIndex >= angajati.Count) return;

            // Update selected row highlighting
            if (selectedRow != null)
            {
                selectedRow.BackColor = ((int)selectedRow.Tag % 2 == 0) ? Color.White : Color.Lavender;
            }
            panel.BackColor = Color.LightBlue;
            selectedRow = panel;

            // Store selected employee and load their data
            selectedAngajat = angajati[rowIndex];
            LoadEmployeeDataToForm(selectedAngajat);

            // Change button text to indicate edit mode
            btnAdauga.Text = "Actualizează";
        }

        private void LoadEmployeeDataToForm(Angajat angajat)
        {
            if (angajat == null) return;

            txtNume.Text = angajat.Nume;
            txtProfesie.Text = angajat.Profesie;
            txtVechime.Text = angajat.Vechime.ToString();
            txtDataNasterii.Text = angajat.DataNasterii.ToString("dd/MM/yyyy");
            txtEmail.Text = angajat.Email;

            // Set the appropriate radio button based on status
            rbSubofiter.Checked = angajat.Statut == StatutAngajat.Subofiter;
            rbOfiter.Checked = angajat.Statut == StatutAngajat.Ofiter;
            rbPensionar.Checked = angajat.Statut == StatutAngajat.Pensionar;
            rbPersonalAdministrativ.Checked = angajat.Statut == StatutAngajat.PersonalAdministrativ;
        }
    }
}