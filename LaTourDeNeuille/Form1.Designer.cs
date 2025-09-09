namespace LaTourDeNeuille
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGauche = new System.Windows.Forms.Button();
            this.btnCentre = new System.Windows.Forms.Button();
            this.btnDroite = new System.Windows.Forms.Button();
            this.btnFondation = new System.Windows.Forms.Button();
            this.btnGrand = new System.Windows.Forms.Button();
            this.btnMoyen = new System.Windows.Forms.Button();
            this.btnPetit = new System.Windows.Forms.Button();
            this.lblPosition = new System.Windows.Forms.Label();
            this.lblDisque = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGauche
            // 
            this.btnGauche.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnGauche.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGauche.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnGauche.Location = new System.Drawing.Point(111, 29);
            this.btnGauche.Name = "btnGauche";
            this.btnGauche.Size = new System.Drawing.Size(50, 911);
            this.btnGauche.TabIndex = 0;
            this.btnGauche.Text = "4";
            this.btnGauche.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnGauche.UseVisualStyleBackColor = false;
            this.btnGauche.Click += new System.EventHandler(this.btnTige_Click);
            // 
            // btnCentre
            // 
            this.btnCentre.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnCentre.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCentre.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCentre.Location = new System.Drawing.Point(387, 29);
            this.btnCentre.Name = "btnCentre";
            this.btnCentre.Size = new System.Drawing.Size(50, 838);
            this.btnCentre.TabIndex = 1;
            this.btnCentre.Text = "4";
            this.btnCentre.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCentre.UseVisualStyleBackColor = false;
            this.btnCentre.Click += new System.EventHandler(this.btnTige_Click);
            // 
            // btnDroite
            // 
            this.btnDroite.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnDroite.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDroite.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDroite.Location = new System.Drawing.Point(684, 29);
            this.btnDroite.Name = "btnDroite";
            this.btnDroite.Size = new System.Drawing.Size(50, 859);
            this.btnDroite.TabIndex = 2;
            this.btnDroite.Text = "4";
            this.btnDroite.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDroite.UseVisualStyleBackColor = false;
            this.btnDroite.Click += new System.EventHandler(this.btnTige_Click);
            // 
            // btnFondation
            // 
            this.btnFondation.BackColor = System.Drawing.Color.Red;
            this.btnFondation.FlatAppearance.BorderSize = 0;
            this.btnFondation.Location = new System.Drawing.Point(4, 387);
            this.btnFondation.Name = "btnFondation";
            this.btnFondation.Size = new System.Drawing.Size(269, 60);
            this.btnFondation.TabIndex = 3;
            this.btnFondation.Text = "btnFondation";
            this.btnFondation.UseVisualStyleBackColor = false;
            this.btnFondation.Click += new System.EventHandler(this.btnDisque_Click);
            // 
            // btnGrand
            // 
            this.btnGrand.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btnGrand.FlatAppearance.BorderSize = 0;
            this.btnGrand.Location = new System.Drawing.Point(37, 332);
            this.btnGrand.Name = "btnGrand";
            this.btnGrand.Size = new System.Drawing.Size(198, 60);
            this.btnGrand.TabIndex = 4;
            this.btnGrand.Text = "btbGrand";
            this.btnGrand.UseVisualStyleBackColor = false;
            this.btnGrand.Click += new System.EventHandler(this.btnDisque_Click);
            // 
            // btnMoyen
            // 
            this.btnMoyen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnMoyen.FlatAppearance.BorderSize = 0;
            this.btnMoyen.Location = new System.Drawing.Point(77, 278);
            this.btnMoyen.Name = "btnMoyen";
            this.btnMoyen.Size = new System.Drawing.Size(123, 60);
            this.btnMoyen.TabIndex = 5;
            this.btnMoyen.Text = "btnMoyen";
            this.btnMoyen.UseVisualStyleBackColor = false;
            this.btnMoyen.Click += new System.EventHandler(this.btnDisque_Click);
            // 
            // btnPetit
            // 
            this.btnPetit.BackColor = System.Drawing.Color.Yellow;
            this.btnPetit.FlatAppearance.BorderSize = 0;
            this.btnPetit.Location = new System.Drawing.Point(96, 221);
            this.btnPetit.Name = "btnPetit";
            this.btnPetit.Size = new System.Drawing.Size(80, 60);
            this.btnPetit.TabIndex = 6;
            this.btnPetit.Text = "btnPetit";
            this.btnPetit.UseVisualStyleBackColor = false;
            this.btnPetit.Click += new System.EventHandler(this.btnDisque_Click);
            // 
            // lblPosition
            // 
            this.lblPosition.AutoSize = true;
            this.lblPosition.Location = new System.Drawing.Point(13, 13);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(54, 13);
            this.lblPosition.TabIndex = 7;
            this.lblPosition.Text = "lblPosition";
            // 
            // lblDisque
            // 
            this.lblDisque.AutoSize = true;
            this.lblDisque.Location = new System.Drawing.Point(92, 13);
            this.lblDisque.Name = "lblDisque";
            this.lblDisque.Size = new System.Drawing.Size(50, 13);
            this.lblDisque.TabIndex = 8;
            this.lblDisque.Text = "lblDisque";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImage = global::LaTourDeNeuille.Properties.Resources.simple_background_backgrounds_passion_simple_1_5c9b95d2b9dfb;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(858, 447);
            this.Controls.Add(this.lblDisque);
            this.Controls.Add(this.lblPosition);
            this.Controls.Add(this.btnPetit);
            this.Controls.Add(this.btnMoyen);
            this.Controls.Add(this.btnGrand);
            this.Controls.Add(this.btnFondation);
            this.Controls.Add(this.btnDroite);
            this.Controls.Add(this.btnCentre);
            this.Controls.Add(this.btnGauche);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
            this.SizeChanged += new System.EventHandler(this.Responsive_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGauche;
        private System.Windows.Forms.Button btnCentre;
        private System.Windows.Forms.Button btnDroite;
        private System.Windows.Forms.Button btnFondation;
        private System.Windows.Forms.Button btnGrand;
        private System.Windows.Forms.Button btnMoyen;
        private System.Windows.Forms.Button btnPetit;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Label lblDisque;
    }
}

