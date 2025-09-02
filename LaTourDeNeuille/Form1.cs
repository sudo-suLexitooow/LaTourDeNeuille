using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LaTourDeNeuille
{
    public partial class Form1 : Form
    {
        private Dictionary<Button, Color> originalColors = new Dictionary<Button, Color>();
        private Color highlightColor = Color.White;

        // Suivi de la position des pièces (clé = bouton pièce, valeur = index de la tige)
        private Dictionary<Button, int> piecePosition = new Dictionary<Button, int>();

        // Coordonnées des tiges - MÊME HAUTEUR pour toutes
        private Dictionary<int, Point> tigePositions = new Dictionary<int, Point>
        {
            { 0, new Point(112, 150) }, // gauche
            { 1, new Point(387, 150) }, // centre
            { 2, new Point(684, 150) } // droite
        };

        // Déplacements autorisés
        private Dictionary<int, List<int>> tigesVoisines = new Dictionary<int, List<int>>
        {
            { 0, new List<int> { 1 } }, // tige 0 → seulement tige 1
            { 1, new List<int> { 0, 2 } }, // tige 1 → tige 0 ou 2
            { 2, new List<int> { 1 } } // tige 2 → seulement tige 1
        };

        // Bouton sélectionné
        private Button pieceSelectionnee = null;

        public Form1()
        {
            InitializeComponent();

            // Sauvegarde couleur de base
            originalColors[btnFondation] = btnFondation.BackColor;
            originalColors[btnGrand] = btnGrand.BackColor;
            originalColors[btnMoyen] = btnMoyen.BackColor;
            originalColors[btnPetit] = btnPetit.BackColor;

            // Initialiser les positions (toutes les pièces sur tige 0 au début)
            piecePosition[btnFondation] = 0;
            piecePosition[btnGrand] = 0;
            piecePosition[btnMoyen] = 0;
            piecePosition[btnPetit] = 0;

            // Événements sur les pièces
            btnFondation.Click += btnDisque_Click;
            btnGrand.Click += btnDisque_Click;
            btnMoyen.Click += btnDisque_Click;
            btnPetit.Click += btnDisque_Click;

            // Événements sur les tiges
            btnGauche.Click += btnTige_Click;
            btnCentre.Click += btnTige_Click;
            btnDroite.Click += btnTige_Click;

            // Activer seulement les disques du sommet et mettre à jour les textes
            MettreAJourInterface();
        }

        private void btnDisque_Click(object sender, EventArgs e)
        {
            pieceSelectionnee = sender as Button;

            if (pieceSelectionnee != null)
            {
                // Afficher infos
                lblPosition.Text = pieceSelectionnee.Location.ToString();
                lblDisque.Text = pieceSelectionnee.Name;

                // Réinitialiser couleurs
                foreach (var b in originalColors)
                    b.Key.BackColor = b.Value;

                // Mettre en surbrillance la pièce sélectionnée
                pieceSelectionnee.BackColor = highlightColor;
            }
        }

        private void btnTige_Click(object sender, EventArgs e)
        {
            if (pieceSelectionnee == null)
            {
             
                return;
            }

            // Identifier la tige cliquée
            int tigeCible = (sender == btnGauche) ? 0 :
                           (sender == btnCentre) ? 1 : 2;

            DeplacerPiece(pieceSelectionnee, tigeCible);
        }

        private void DeplacerPiece(Button piece, int tigeCible)
        {
            int tigeActuelle = piecePosition[piece];

            // Vérifier si le déplacement est autorisé
            if (!tigesVoisines[tigeActuelle].Contains(tigeCible))
            {
                MessageBox.Show("Déplacement interdit !");
                return;
            }

            // Mise à jour de la position
            piecePosition[piece] = tigeCible;

            // Déplacer le bouton - GARDER la position Y actuelle, changer seulement X
            piece.Location = new Point(tigePositions[tigeCible].X, piece.Location.Y);

            // Réinitialiser couleurs
            foreach (var b in originalColors)
                b.Key.BackColor = b.Value;

            pieceSelectionnee = null;

            // Mettre à jour l'interface après le déplacement
            MettreAJourInterface();
        }

        private void MettreAJourInterface()
        {
            // Mettre à jour les boutons actifs
            MettreAJourBoutonsActifs();

            // Mettre à jour le texte des tiges avec les hauteurs
            MettreAJourTexteTiges();
        }

        private void MettreAJourBoutonsActifs()
        {
            // Liste de toutes les pièces
            List<Button> pieces = new List<Button> { btnPetit, btnMoyen, btnGrand, btnFondation };

            // Désactiver toutes les pièces d'abord
            foreach (Button piece in pieces)
            {
                piece.Enabled = false;
            }

            // Pour chaque tige, trouver la pièce du sommet (plus petite Y = plus haute)
            for (int tige = 0; tige < 3; tige++)
            {
                Button pieceDuSommet = null;
                int yLePlusHaut = int.MaxValue;

                foreach (Button piece in pieces)
                {
                    if (piecePosition[piece] == tige && piece.Location.Y < yLePlusHaut)
                    {
                        yLePlusHaut = piece.Location.Y;
                        pieceDuSommet = piece;
                    }
                }

                // Activer seulement la pièce du sommet de cette tige
                if (pieceDuSommet != null)
                {
                    pieceDuSommet.Enabled = true;
                }
            }
        }

        private void MettreAJourTexteTiges()
        {
            // Compter le nombre de pièces sur chaque tige
            int[] hauteurTiges = new int[3];

            foreach (var kvp in piecePosition)
            {
                hauteurTiges[kvp.Value]++;
            }

            // Mettre à jour le texte des boutons de tiges
            btnGauche.Text = $"{hauteurTiges[0]}";
            btnCentre.Text = $"{hauteurTiges[1]}";
            btnDroite.Text = $"{hauteurTiges[2]}";
        }
    }
}