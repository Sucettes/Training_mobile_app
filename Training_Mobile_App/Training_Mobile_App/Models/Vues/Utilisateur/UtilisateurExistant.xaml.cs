using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Training_Mobile_App.Models.Classes;
using Training_Mobile_App.Models.Vues.Entrainement;
using Training_Mobile_App.Models.FctBiblio;

namespace Training_Mobile_App.Models.Vues.Utilisateur
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UtilisateurExistant : ContentPage
    {
        #region Attributs
        /// <summary>
        ///  Initialisation de la classe son.
        /// </summary>
        private AudioBouton _clickAudio = new AudioBouton();
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de la page UtilisateurExistant
        /// </summary>
        /// <param name="unUtilisateur"></param>
        public UtilisateurExistant(Classes.Utilisateur unUtilisateur)
        {
            InitializeComponent();

            BackButtonBehavior backButton = new BackButtonBehavior();
            backButton.IsEnabled = false;

            this.nomUtilisateur.Text = "Bonjour " + unUtilisateur.Prenom + (" ") +
                                       unUtilisateur.Nom;
        }
        #endregion

        #region Méthodes

        /// <summary>
        /// Évènement quand le button est clicker.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_OnClicked(object sender, EventArgs e)
        {
            GestionFichiers gestionFichiers = new GestionFichiers();
            gestionFichiers.ChargerFichier();

            if (gestionFichiers.ProgressionExercices.Count < 1)
            {
                _clickAudio.JouerSon();
                Application.Current.MainPage.Navigation.PushModalAsync(new NouvelEntrainement(), true);
            }
            else
            {
                _clickAudio.JouerSon();
                Application.Current.MainPage.Navigation.PushModalAsync(new ConteneurPrincipal.ConteneurPrincipal(), true);
            }
        }

        /// <summary>
        /// Désactive le retour à page MainPage.
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            return false;
        }

        #endregion
    }
}