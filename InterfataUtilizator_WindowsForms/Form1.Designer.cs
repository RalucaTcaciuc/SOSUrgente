namespace InterfataUtilizator_WindowsForms
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitlu = new System.Windows.Forms.Label();
            this.lblNume = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // 
            // lblTitlu
            // 
            this.lblTitlu.AutoSize = true;
            this.lblTitlu.Location = new System.Drawing.Point(50, 20);
            this.lblTitlu.Name = "lblTitlu";
            this.lblTitlu.Size = new System.Drawing.Size(35, 13);
            this.lblTitlu.Text = "Titlu";

            // 
            // lblNume
            // 
            this.lblNume.AutoSize = true;
            this.lblNume.Location = new System.Drawing.Point(50, 60);
            this.lblNume.Name = "lblNume";
            this.lblNume.Size = new System.Drawing.Size(40, 13);
            this.lblNume.Text = "Nume";

            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.lblTitlu);
            this.Controls.Add(this.lblNume);
            this.Text = "Interfață Utilizator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}

