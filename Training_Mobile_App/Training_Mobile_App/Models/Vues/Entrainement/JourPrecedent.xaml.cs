using System;
using System.Collections.Generic;
using Training_Mobile_App.Models.FctBiblio;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Training_Mobile_App.Models.Vues.Entrainement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JourPrecedent : ContentPage
    {

        #region Attributs

        /// <summary>
        /// Liste de progression des exercices d'un utilisateurs
        /// provenant de conteneurPrincipal.
        /// </summary>
        private List<TupleEnListe> _progressionExercices;

        /// <summary>
        /// Initialisation de la classe son.
        /// </summary>
        private AudioBouton _clickAudio = new AudioBouton();

        #endregion

        #region Get/Set
        /// <summary>
        /// Get/Set de la liste de progression des exercices.
        /// </summary>
        public List<TupleEnListe> ProgressionExercices
        {
            get { return _progressionExercices; }
            set { _progressionExercices = value; }
        }
        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur pour la page JourPrecedent
        /// </summary>
        public JourPrecedent(List<TupleEnListe> pProgressionExercices)
        {
            InitializeComponent();
            ProgressionExercices = pProgressionExercices;
            ChargerDernierEntrainement();
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Permet d'allez a l page de création d'un nouvelle entrainement.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_OnClickedNouvelEntrainement(object sender, EventArgs e)
        {
            _clickAudio.JouerSon();
            Application.Current.MainPage.Navigation.PushModalAsync(new NouvelEntrainement(), true);
        }

        /// <summary>
        /// Chargement des données de l'entraiment du jour précédent et 
        /// préparation à son affichage.
        /// </summary>
        public void ChargerDernierEntrainement()
        {
            try
            {
                int ligne = 1;

                DateTime derniereJournee = ProgressionExercices[0].Date;

                foreach (var element in ProgressionExercices)
                {

                    if (derniereJournee.Equals(element.Date))
                    {
                        AjoutLigneTableau();
                        AjoutLabelTableau(element.ProgUser.InfoExercice.NomExercice, ligne, 1);
                        AjoutLabelTableau(element.ProgUser.InfoExercice.PartieDuCorpsTravailler.ToString(), ligne, 2);
                        AjoutLabelTableau(element.ProgUser.Poids.ToString(), ligne, 3);
                        AjoutLabelTableau(element.ProgUser.NbReps.ToString(), ligne, 4);
                        AjoutLabelTableau(element.ProgUser.NbSeries.ToString(), ligne, 5);

                        ligne++;
                        derniereJournee = element.Date;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {

            }

        }

        /// <summary>
        /// Création d'une nouvelle ligne de tableau dans la page de JourPrecedent.
        /// </summary>
        public void AjoutLigneTableau()
        {
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = 20;

            DataGrid.RowDefinitions.Add(rowDefinition);
        }

        /// <summary>
        /// Création de label pour mettre à jour l'information dans le tableau
        /// du dernier entrainement.
        /// </summary>
        /// <param name="pLblNom"></param>
        public void AjoutLabelTableau(string pLblNom, int pLigne, int pColonne)
        {
            Label label = new Label();
            label.TextColor = Color.Black;
            label.Text = pLblNom;
            Grid.SetRow(label, pLigne);
            Grid.SetColumn(label, pColonne);

            DataGrid.Children.Add(label);
        }

        #endregion
    }
}