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

        // Références aux boutons tiges pour un accès facile
        private Dictionary<int, Button> boutonsTiges = new Dictionary<int, Button>();

        // Position y de base (sole) - sera mise à jour dynamiquement
        private int positionSol = 389;
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

            // Initialiser les références aux boutons tiges
            boutonsTiges[0] = btnGauche;
            boutonsTiges[1] = btnCentre;
            boutonsTiges[2] = btnDroite;

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

            // Positionner les tiges initialement
            PositionnerTiges();

            // Initialiser les positions (toutes les pièces sur tige 0 au début)
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

        private void PositionnerTiges()
        {
            // Calculer les positions des tiges en fonction de la largeur de la fenêtre
            int largeurFenetre = this.ClientSize.Width;
            int hauteurFenetre = this.ClientSize.Height;

            // Diviser l'espace en 3 zones égales
            int espacement = largeurFenetre / 3;

            // Position Y des tiges (plus haut, environ au cinquième supérieur de la fenêtre)
            int positionYTiges = hauteurFenetre / 5;

            // Positionner chaque tige au centre de sa zone
            // Tige gauche
            btnGauche.Location = new Point(
                espacement / 2 - btnGauche.Width / 2,
                positionYTiges
            );

            // Tige centre
            btnCentre.Location = new Point(
                largeurFenetre / 2 - btnCentre.Width / 2,
                positionYTiges
            );

            // Tige droite
            btnDroite.Location = new Point(
                espacement * 2 + espacement / 2 - btnDroite.Width / 2,
                positionYTiges
            );

            // Mettre à jour la position du sol (un peu au-dessus du bas de la fenêtre)
            positionSol = hauteurFenetre - 100;
        }

        private Point ObtenirPositionTige(int indexTige)
        {
            // Récupérer dynamiquement la position centrale de la tige
            Button tige = boutonsTiges[indexTige];
            int centreX = tige.Location.X + tige.Width / 2;
            int positionY = tige.Location.Y + tige.Height + 10; // Un peu en dessous de la tige

            return new Point(centreX, positionY);
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

                // Positionner le disque
                PositionnerDisque(disque, 0);
            }
        }

        private void PositionnerDisque(Button disque, int indexTige)
        {
            // Obtenir la position centrale de la tige
            Point positionTige = ObtenirPositionTige(indexTige);

            // Convertir la pile en liste pour trouver l'index
            List<Button> disquesList = pilesParTige[indexTige].ToList();
            disquesList.Reverse(); // Inverser car Stack est LIFO

            // Calculer la position Y en fonction du nombre de disques en dessous
            int nombreDisquesEnDessous = disquesList.IndexOf(disque);
            if (nombreDisquesEnDessous == -1)
            {
                // Si le disque n'est pas trouvé, c'est qu'il sera au sommet
                nombreDisquesEnDessous = pilesParTige[indexTige].Count - 1;
            }

            int positionY = positionSol - (nombreDisquesEnDessous * hauteurPiece);

            // Centrer le disque horizontalement par rapport à la tige
            int positionX = positionTige.X - disque.Width / 2;

            disque.Location = new Point(positionX, positionY);
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

            // Vérifier si on essaie de déplacer sur la même tige
            if (tigeActuelle == tigeCible)
            {
                // Annuler la sélection sans message
                foreach (var b in originalColors)
                    b.Key.BackColor = b.Value;
                pieceSelectionnee = null;
                return;
            }

            // Vérifier si le déplacement est autorisé (tige voisine)
            if (!tigesVoisines[tigeActuelle].Contains(tigeCible))
            {
                MessageBox.Show("Déplacement interdit ! Vous ne pouvez déplacer que vers une tige adjacente.");

                // Réinitialiser la sélection
                foreach (var b in originalColors)
                    b.Key.BackColor = b.Value;
                pieceSelectionnee = null;
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

            // Positionner le disque à sa nouvelle position
            PositionnerDisque(piece, tigeCible);

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

        private void Responsive_SizeChanged(object sender, EventArgs e)
        {
            // Repositionner les tiges en fonction de la nouvelle taille
            PositionnerTiges();

            // Repositionner tous les disques existants
            for (int tige = 0; tige < 3; tige++)
            {
                // Créer une liste temporaire car on ne peut pas itérer directement sur une Stack
                List<Button> disquesSurTige = new List<Button>(pilesParTige[tige]);

                // Repositionner chaque disque de cette tige
                foreach (Button disque in disquesSurTige)
                {
                    PositionnerDisque(disque, tige);
                }
            }
        }
    }
}