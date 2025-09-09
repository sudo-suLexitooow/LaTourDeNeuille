using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LaTourDeNeuille
{
    public partial class Form1 : Form
    {
        private Dictionary<Button, Color> originalColors = new Dictionary<Button, Color>();
        private Color highlightColor = Color.White;

        // Suivi de la position des pièces (clé = bouton pièce, valeur = index de la tige)
        private Dictionary<Button, int> piecePosition = new Dictionary<Button, int>();

        // Taille des disques (pour validation des mouvements)
        private Dictionary<Button, int> tailleDisque = new Dictionary<Button, int>();

        // Pile de disques sur chaque tige (pour gérer l'empilement)
        private Dictionary<int, Stack<Button>> pilesParTige = new Dictionary<int, Stack<Button>>();

        // Coordonnées des tiges - MÊME HAUTEUR pour toutes
        private Dictionary<int, Point> tigePositions = new Dictionary<int, Point>
        {
            { 0, new Point(112, 150) }, // gauche
            { 1, new Point(387, 150) }, // centre
            { 2, new Point(684, 150) } // droite
        };

        // Position y de base (sole)
        private int positionSol =389;
        private int hauteurPiece = 60;

        // Déplacements autorisés
        private Dictionary<int, List<int>> tigesVoisines = new Dictionary<int, List<int>>
        {
            { 0, new List<int> { 1 } }, // tige 0, seulement tige 1
            { 1, new List<int> { 0, 2 } }, // tige 1, tige 0 ou 2
            { 2, new List<int> { 1 } } // tige 2, seulement tige 1
        };

        // Bouton sélectionné
        private Button pieceSelectionnee = null;

        public Form1()
        {
            InitializeComponent();

            // Initialiser les piles pour chaque tige
            for (int i = 0; i < 3; i++)
            {
                pilesParTige[i] = new Stack<Button>();
            }

            // Définir la taille des disques (1 = plus petit, 4 = plus grand)
            tailleDisque[btnPetit] = 1;
            tailleDisque[btnMoyen] = 2;
            tailleDisque[btnGrand] = 3;
            tailleDisque[btnFondation] = 4;

            // Sauvegarde couleur de base
            originalColors[btnFondation] = btnFondation.BackColor;
            originalColors[btnGrand] = btnGrand.BackColor;
            originalColors[btnMoyen] = btnMoyen.BackColor;
            originalColors[btnPetit] = btnPetit.BackColor;

            // Initialiser les positions (toutes les pièces sur tige 0 au début)
            // Empiler dans l'ordre : Fondation, Grand, Moyen, Petit
            InitialiserTour();

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

        private void InitialiserTour()
        {
            // Vider toutes les piles
            foreach (var pile in pilesParTige.Values)
            {
                pile.Clear();
            }

            // Empiler sur la tige 0 dans l'ordre (du plus grand au plus petit)
            List<Button> disquesOrdonnees = new List<Button> { btnFondation, btnGrand, btnMoyen, btnPetit };

            foreach (Button disque in disquesOrdonnees)
            {
                piecePosition[disque] = 0;
                pilesParTige[0].Push(disque);

                // Calculer la position Y en fonction du nombre de disques déjà empilés
                int nombreDisquesEnDessous = pilesParTige[0].Count - 1;
                int positionY = positionSol - (nombreDisquesEnDessous * hauteurPiece);

                disque.Location = new Point(tigePositions[0].X, positionY);
            }
        }

        private void btnDisque_Click(object sender, EventArgs e)
        {
            Button disqueClique = sender as Button;

            if (disqueClique != null)
            {
                // Vérifier si ce disque est au sommet de sa pile
                int tigeDuDisque = piecePosition[disqueClique];
                if (pilesParTige[tigeDuDisque].Count > 0 && pilesParTige[tigeDuDisque].Peek() == disqueClique)
                {
                    pieceSelectionnee = disqueClique;

                    // Afficher infos
                    lblPosition.Text = pieceSelectionnee.Location.ToString();
                    lblDisque.Text = pieceSelectionnee.Name;

                    // Réinitialiser couleurs
                    foreach (var b in originalColors)
                        b.Key.BackColor = b.Value;

                    // Mettre en surbrillance la pièce sélectionnée
                    pieceSelectionnee.BackColor = highlightColor;
                }
                else
                {
                    MessageBox.Show("Vous ne pouvez déplacer que le disque du sommet !");
                }
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

            // Vérifier si le déplacement est autorisé (tige voisine)
            if (!tigesVoisines[tigeActuelle].Contains(tigeCible))
            {
                MessageBox.Show("Déplacement interdit ! Vous ne pouvez déplacer que vers une tige adjacente.");
                return;
            }

            // Vérifier la règle de Hanoï : on ne peut pas mettre un grand disque sur un petit
            if (pilesParTige[tigeCible].Count > 0)
            {
                Button disqueDuDessus = pilesParTige[tigeCible].Peek();
                if (tailleDisque[piece] > tailleDisque[disqueDuDessus])
                {
                    MessageBox.Show("Déplacement interdit ! Perdu!!!");

                    foreach (var b in originalColors)
                    {
                        b.Key.BackColor = b.Value;
                    }

                    pieceSelectionnee = null;

                    InitialiserTour();
                    MettreAJourInterface();
                    return;

                }
            }

            // Retirer le disque de la pile actuelle
            if (pilesParTige[tigeActuelle].Count > 0 && pilesParTige[tigeActuelle].Peek() == piece)
            {
                pilesParTige[tigeActuelle].Pop();
            }

            // Ajouter le disque à la nouvelle pile
            pilesParTige[tigeCible].Push(piece);

            // Mise à jour de la position
            piecePosition[piece] = tigeCible;

            // Calculer la nouvelle position Y en fonction du nombre de disques sur la tige cible
            int nombreDisquesEnDessous = pilesParTige[tigeCible].Count - 1;
            int nouvellePositionY = positionSol - (nombreDisquesEnDessous * hauteurPiece);

            // Déplacer le bouton avec la bonne hauteur
            piece.Location = new Point(tigePositions[tigeCible].X, nouvellePositionY);

            // Réinitialiser couleurs
            foreach (var b in originalColors)
                b.Key.BackColor = b.Value;

            pieceSelectionnee = null;

            // Mettre à jour l'interface après le déplacement
            MettreAJourInterface();

            // Vérifier la victoire (tous les disques sur la tige 2)
            VerifierVictoire();
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

            // Pour chaque tige, activer seulement le disque du sommet
            for (int tige = 0; tige < 3; tige++)
            {
                if (pilesParTige[tige].Count > 0)
                {
                    Button disqueDuSommet = pilesParTige[tige].Peek();
                    disqueDuSommet.Enabled = true;
                }
            }
        }

        private void MettreAJourTexteTiges()
        {
            // Mettre à jour le texte des boutons de tiges avec le nombre de disques
            btnGauche.Text = $"{pilesParTige[0].Count}";
            btnCentre.Text = $"{pilesParTige[1].Count}";
            btnDroite.Text = $"{pilesParTige[2].Count}";
        }

        private void VerifierVictoire()
        {
            // Victoire si tous les disques sont sur la tige 2
            if (pilesParTige[2].Count == 4)
            {
                MessageBox.Show("Félicitations ! Vous avez gagné !", "Victoire",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (MessageBox.Show("Voulez-vous recommencer ?", "Nouvelle partie",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    InitialiserTour();
                    MettreAJourInterface();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}