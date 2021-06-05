using System;
using Training_Mobile_App.Models.Classes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Training_Mobile_App.Models.FctBiblio;

namespace Training_Mobile_App.Models.Vues.Utilisateur
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NouvelUtilisateurPage2 : ContentPage
    {
        #region Attributs
        /// <summary>
        /// Initialisation de la classe son.
        /// </summary>
        private AudioBouton _clickAudio = new AudioBouton();

        /// <summary>
        /// Attributs de type string. représente le nom
        /// </summary>
        private string _nom;
        /// <summary>
        /// Attributs de type string. représente le prénom
        /// </summary>
        private string _prenom;
        /// <summary>
        /// Attributs de type byte. représente l'age
        /// </summary>
        private byte _age;
        /// <summary>
        /// Attributs de type float. représente le poids
        /// </summary>
        private float _poids;
        /// <summary>
        /// Attributs de type float. représente la grandeur
        /// </summary>
        private float _grandeur;
        /// <summary>
        /// Attributs de type float. représente la masse grasse
        /// </summary>
        private float _masseGrasse;
        /// <summary>
        /// Attributs de type float. représente la masse musculaire
        /// </summary>
        private float _masseMusculaire;
        /// <summary>
        /// Attributs de type string. représente le gnere
        /// </summary>
        private string _genre;

        #endregion

        #region Get/Set
        /// <summary>
        /// Get/Set permet de modifier ou d'obtenir le Nom
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
        /// Get/Set permet de modifier ou d'obtenir le poids
        /// </summary>
        public float Poids
        {
            get { return _poids; }
            set { _poids = value; }
        }
        /// <summary>
        /// Get/Set permet de modifier ou d'obtenir la grandeur
        /// </summary>
        public float Grandeur
        {
            get { return _grandeur; }
            set { _grandeur = value; }
        }
        /// <summary>
        /// Get/Set permet de modifier ou d'obtenir la masse grasse
        /// </summary>
        public float MasseGrasse
        {
            get { return _masseGrasse; }
            set { _masseGrasse = value; }
        }
        /// <summary>
        /// Get/Set permet de modifier ou d'obtenir la masse musculaire
        /// </summary>
        public float MasseMusculaire
        {
            get { return _masseMusculaire; }
            set { _masseMusculaire = value; }
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
        /// Constructeur, permet d'initialiser les attribus en affectant les valeurs recus en paramètre.
        /// </summary>
        /// <param name="pNom"></param>
        /// <param name="pPrenom"></param>
        /// <param name="pAge"></param>
        /// <param name="pGenre"></param>
        public NouvelUtilisateurPage2(string pNom, string pPrenom, byte pAge, string pGenre)
        {
            Nom = pNom;
            Prenom = pPrenom;
            Age = pAge;
            Genre = pGenre;
            InitializeComponent();
        }

        #endregion

        #region Méthodes
        /// <summary>
        /// évènement quand le bouton précédents est clicker.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonClickedPrevious(object sender, EventArgs e)
        {
            _clickAudio.JouerSon();
            Application.Current.MainPage.Navigation.PushModalAsync(
                new NouvelUtilisateur(Nom, Prenom, Age, Genre), true);
        }
        /// <summary>
        /// évènement quand le bouton suivant est clicker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonClickedNext(object sender, EventArgs e)
        {
            try
            {
                GestionUtilisateur gestionUtilisateur = new GestionUtilisateur();
                _clickAudio.JouerSon();
                if (string.IsNullOrEmpty(poidsUtilisateur.Text))
                { throw new ArgumentNullException("Le poid ne peut pas être vide.", (Exception)null); }

                if (float.TryParse(poidsUtilisateur.Text, out float poidsSortie))
                { Poids = poidsSortie; }
                else
                { throw new ArgumentException("Le poids doit être un nombre."); }


                if (string.IsNullOrEmpty(grandeurUtilisateur.Text))
                { throw new ArgumentNullException("La grandeur ne peut pas être vide.", (Exception)null); }
                if (float.TryParse(grandeurUtilisateur.Text, out float grandeurSortie))
                { Grandeur = grandeurSortie; }
                else
                { throw new ArgumentException("La grandeur doit être un nombre."); }


                if (string.IsNullOrEmpty(masseGrasseUtilisateur.Text))
                { masseGrasseUtilisateur.Text = "0"; }
                if (float.TryParse(masseGrasseUtilisateur.Text, out float masseGrasseSortie))
                { MasseGrasse = masseGrasseSortie; }
                else
                { throw new ArgumentException("La masse grasse doit être un nombre."); }

                if (string.IsNullOrEmpty(masseMusculaireUtilisateur.Text))
                { masseMusculaireUtilisateur.Text = "0"; }
                if (float.TryParse(masseMusculaireUtilisateur.Text, out float masseMusculaireSortie))
                { MasseMusculaire = masseMusculaireSortie; }
                else
                { throw new ArgumentException("La masse musculaire doit être un nombre."); }

                Sexe sexe;
                if (Genre == "Femme") { sexe = 0; }
                else if (Genre == "Homme") { sexe = (Sexe)1; }
                else { sexe = (Sexe)2; }

                gestionUtilisateur.InscrireUtilisateur(
                    Nom, Prenom, Age, sexe, Poids, Grandeur, MasseGrasse, MasseMusculaire);

                Application.Current.MainPage.Navigation.PushModalAsync(new Tutoriel.Tutoriel(), true);
            }
            catch (Exception exeption)
            {
                DisplayAlert("Erreur", exeption.Message, "Ok");
            }
        }
        #endregion
    }
}