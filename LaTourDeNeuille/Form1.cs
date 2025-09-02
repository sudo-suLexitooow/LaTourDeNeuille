using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaTourDeNeuille
{
    public partial class Form1 : Form
    {
        private Dictionary<Button, Color> originalColors = new Dictionary<Button, Color>();
        private Color highlightColor = Color.White;
        public Form1()
        {
            InitializeComponent();
            // Sauvegarde couleur de base 
            originalColors[btnFondation] = btnFondation.BackColor;
            originalColors[btnGrand] = btnGrand.BackColor;
            originalColors[btnMoyen] = btnMoyen.BackColor;
            originalColors[btnPetit] = btnPetit.BackColor;
            // abandonner les disques à l'événement 
            btnFondation.Click += btnDisque_Click;
            btnGrand.Click += btnDisque_Click;
            btnMoyen.Click += btnDisque_Click;
            btnPetit.Click += btnDisque_Click;
            
            
        }

        private void btnDisque_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null) { 
                //afficher la position
                Point position = btn.Location;
                lblPosition.Text = position.ToString();

                //afficher le nom du bouton
                lblDisque.Text = btn.Name;

                //reinitialiser les couleurs des btn
                foreach (var b in originalColors)
                {
                    b.Key.BackColor = b.Value;
                }
                btn.BackColor = highlightColor;
            }


        }
    }
}
