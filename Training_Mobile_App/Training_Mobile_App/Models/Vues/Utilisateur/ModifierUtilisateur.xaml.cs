using System;
using System.Linq;
using Training_Mobile_App.Models.Classes;
using Training_Mobile_App.Models.FctBiblio;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Training_Mobile_App.Models.Vues.Utilisateur
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModifierUtilisateur : ContentPage
    {
        #region Attributs
        /// <summary>
        /// Attributs qui représente gestionFichiers.
        /// </summary>
        private GestionFichiers _gestionFichiers;

        /// <summary>
        /// La progression du poids d'un utilisateur. La données
        /// vient de conteneurPrincipal.
        /// </summary>
        private string[] _poidsUtilisateur;

        private AudioBouton _clickAudio = new AudioBouton();
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de la page modifierUtilisateur
        /// </summary>
        public ModifierUtilisateur(GestionFichiers pGestionFichiers, string[] pPoidsUtilisateur)
        {
            InitializeComponent();
            _gestionFichiers = pGestionFichiers;
            _poidsUtilisateur = pPoidsUtilisateur;
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Permet de supprimer l'utilisateur existant et de supprimer c'est information
        /// du téléphone.
        /// </summary>
        private async void btnSUpprimerUtilisateur_Click(object sender, EventArgs e)
        {
            _clickAudio.JouerSon();
            string message = "Êtes-vous sûr de vouloir réinitialisé l'utilisateur?\n" +
                             "Toutes les données seront perdues...";
            string action = await DisplayActionSheet
                               ($"{message}", "Non", "Oui");
            bool fichierResetter = false;

            if (action.Equals("Oui"))
            {
                _gestionFichiers.ReinitialisationUtilisateur();
                fichierResetter = true;
            }

            if (fichierResetter)
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage(), false);
            }

        }

        /// <summary>
        /// Permet à l'utilisateur de modifier son poids.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModifierUtilisateur_Click(object sender, EventArgs e)
        {
            _clickAudio.JouerSon();
            ReinitPlacheholder();
            this.messageFeedback.Text = "";
            SetToVisible(true);

            string[] infoUtilisateur;

            try
            {

                if (_poidsUtilisateur.Length < 2)
                {
                    infoUtilisateur = _poidsUtilisateur[0].Split(';');
                }
                else
                {
                    infoUtilisateur = _poidsUtilisateur
                                [_poidsUtilisateur.Count() - 2].Split(';');
                }

                poids.Placeholder += infoUtilisateur[0];
                musculaire.Placeholder += infoUtilisateur[1];
                grasse.Placeholder += infoUtilisateur[2];
            }
            catch
            {
                SetToVisible(false);
            }

        }

        /// <summary>
        /// Évènement quand le bouton accepter changement est clicker.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAccepterChangement_Click(object sender, EventArgs e)
        {
            _clickAudio.JouerSon();
            if (!ChampValide(musculaire.Text))
            {
                musculaire.Text = "0";
            }

            if (!ChampValide(grasse.Text))
            {
                grasse.Text = "0";
            }

            if (ChampValide(poids.Text))
            {
                try
                {
                    _gestionFichiers.EnregistrerProgres(poids.Text, musculaire.Text, grasse.Text);
                    this.messageFeedback.Text = "Enregistrement réussi!";
                    this.messageFeedback.TextColor = Color.Green;
                }
                catch
                {
                }

            }
            else
            {
                this.messageFeedback.Text = "Données insérées invalides. \n" +
                                            "La progression ne sera pas enregistrée.";
                this.messageFeedback.TextColor = Color.Red;
                SetToVisible(false);
            }

        }

        /// <summary>
        /// Affiche ou cache les éléments à l'affichage.
        /// </summary>
        /// <param name="isVisible"></param>
        private void SetToVisible(bool isVisible)
        {
            poids.IsVisible = isVisible;
            musculaire.IsVisible = isVisible;
            grasse.IsVisible = isVisible;
            btnChangement.IsVisible = isVisible;
            this.btnModifUtilisateur.IsEnabled = !isVisible;
            ReinitPlacheholder();
        }

        /// <summary>
        /// Vérifie que les champs du formulaire sont valides.
        /// </summary>
        /// <param name="champAVerifier"></param>
        /// <returns>True si les champs sont valides sinon false.</returns>
        private bool ChampValide(string champAVerifier)
        {
            double elementAParser = 0;

            if (string.IsNullOrEmpty(champAVerifier) ||
                !double.TryParse(champAVerifier, out elementAParser))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Réinitialisation du texte dans les placeholders d'informations.
        /// </summary>
        private void ReinitPlacheholder()
        {
            this.poids.Placeholder = "Votre Poids ";
            this.musculaire.Placeholder = "Votre masse musculaire ";
            this.grasse.Placeholder = "Votre masse grasse ";
        }
        #endregion

    }
}