using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibrarieModele;
using NivelStocareDate;

namespace InterfataUtilizator_WindowsForms
{
    public partial class Form1: Form
    {
        private Label lblTitlu;
        private Label lblNume;

        public Form1()
        {

            this.Text = "Interfață Utilizator";
            this.Width = 400;
            this.Height = 300;

            lblTitlu = new Label();
            lblTitlu.Text = "Titlu";
            lblTitlu.Top = 20;
            lblTitlu.Left = 50;
            lblTitlu.AutoSize = true;
            lblTitlu.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);

            lblNume = new Label();
            lblNume.Text = "Nume";
            lblNume.Top = 60;
            lblNume.Left = 50;
            lblNume.AutoSize = true;
            lblNume.Font = new System.Drawing.Font("Arial", 12);

            this.Controls.Add(lblTitlu);
            this.Controls.Add(lblNume);
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
