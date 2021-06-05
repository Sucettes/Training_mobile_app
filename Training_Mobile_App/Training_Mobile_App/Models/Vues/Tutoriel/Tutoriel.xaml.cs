using System;
using Training_Mobile_App.Models.FctBiblio;
using Training_Mobile_App.Models.Vues.Entrainement;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Timers;


namespace Training_Mobile_App.Models.Vues.Tutoriel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tutoriel : ContentPage
    {
        #region Attributs
        /// <summary>
        /// Initialisation de la classe son.
        /// </summary>
        private AudioBouton _clickAudio = new AudioBouton();
        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur de la page Tutoriel
        /// </summary>
        public Tutoriel()
        {
            InitializeComponent();
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Évènement quand le bouton suivant est clicker.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonClickedSuivant(object sender, EventArgs e)
        {
            _clickAudio.JouerSon();
            Application.Current.MainPage.Navigation.PushModalAsync(new NouvelEntrainement(), true);
        }

        #endregion
    }
}