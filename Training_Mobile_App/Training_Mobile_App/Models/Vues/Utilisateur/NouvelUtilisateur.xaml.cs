using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Training_Mobile_App.Models.FctBiblio;

namespace Training_Mobile_App.Models.Vues.Utilisateur
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NouvelUtilisateur : ContentPage
    {
        #region Attributs
        /// <summary>
        /// Initialisation de la classe son.
        /// </summary>
        private AudioBouton _clickAudio = new AudioBouton();

        /// <summary>
        /// Attributs de type string, représente le nom
        /// </summary>
        private string _nom;
        /// <summary>
        /// Attributs de type string, représente le prenom
        /// </summary>
        private string _prenom;
        /// <summary>
        /// Attributs de type byte, représente l'age
        /// </summary>
        private byte _age;
        /// <summary>
        /// Attributs de type string, représente le genre
        /// </summary>
        private string _genre;
        #endregion

        #region Get/Set
        /// <summary>
        /// Get/Set permet de modifier ou d'obtenir le nom
        /// </summary>
        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }
        /// <summary>
        /// Get/Set permet de modifier ou d'obtenir le prenom
        /// </summary>
        public string Prenom
        {
            get { return _prenom; }
            set { _prenom = value; }
        }
        /// <summary>
        /// Get/Set permet de modifier ou d'obtenir l'age
        /// </summary>
        public byte Age
        {
            get { return _age; }
            set { _age = value; }
        }
        /// <summary>
        /// Get/Set permet de modifier ou d'obtenir le genre
        /// </summary>
        public string Genre
        {
            get { return _genre; }
            set { _genre = value; }
        }
        #endregion

        #region Constructeur
        /// <summary>
        /// constructeur de la page NouvelUtilisateur (de base)
        /// </summary>
        public NouvelUtilisateur()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructeur avec paramètre fournis.
        /// </summary>
        /// <param name="pPrenom">pPrenom de type string, représente le prenom</param>
        /// <param name="pNom">pNom de type string, représente le nom</param>
        /// <param name="pAge">pAge de type byte, représente l'age</param>
        /// <param name="pGenre">pGenre de type string, représente le genre</param>
        public NouvelUtilisateur(string pPrenom, string pNom, byte pAge, string pGenre)
        {
            InitializeComponent();

            BackButtonBehavior backButton = new BackButtonBehavior();
            backButton.IsEnabled = false;

            Prenom = pPrenom;
            prenomUtilisateur.Text = Prenom;
            Nom = pNom;
            nomUtilisateur.Text = Nom;
            Age = pAge;
            ageUtilisateur.Text = Age.ToString();
            Genre = pGenre;
            genreUtilisateur.SelectedItem = Genre;
        }
        #endregion

        #region Méthodes

        /// <summary>
        /// évènement quand le bouton est cliker.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonClicked(object sender, EventArgs e)
        {
            try
            {
                _clickAudio.JouerSon();
                if (string.IsNullOrEmpty(prenomUtilisateur.Text))
                { throw new ArgumentNullException("Le prénom ne peut pas être vide.", (Exception)null); }
                Prenom = prenomUtilisateur.Text;

                if (string.IsNullOrEmpty(nomUtilisateur.Text))
                { throw new ArgumentNullException("Le nom ne peut pas être vide.", (Exception)null); }
                Nom = nomUtilisateur.Text;

                if (string.IsNullOrEmpty(ageUtilisateur.Text))
                { throw new ArgumentNullException("L'âge ne peut pas être vide.", (Exception)null); }
                if (byte.TryParse(ageUtilisateur.Text, out byte ageSortie)) { Age = ageSortie; }
                else
                { throw new ArgumentException("L'âge doit être un nombre entier."); }

                if (string.IsNullOrEmpty(Genre))
                { throw new ArgumentNullException("Vous devez choisir un genre.", (Exception)null); }

                Application.Current.MainPage.Navigation.PushModalAsync(new NouvelUtilisateurPage2(Nom, Prenom, Age, Genre), true);
            }
            catch (Exception exeption)
            { DisplayAlert("Erreur", exeption.Message, "Ok"); }
        }

        /// <summary>
        /// évènement quand le sexe de l'utilisateur est modifier.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenreUtilisateur_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            { Genre = picker.Items[selectedIndex]; }
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