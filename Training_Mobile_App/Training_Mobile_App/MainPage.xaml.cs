using System;
using Xamarin.Forms;
using Training_Mobile_App.Models.Classes;
using Training_Mobile_App.Models.Vues.Utilisateur;

namespace Training_Mobile_App
{
    public partial class MainPage : ContentPage
    {
        #region Constructeur

        /// <summary>
        /// Constructeru de la page principale.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            GestionFichiers gestionFichiers = new GestionFichiers();
            GestionUtilisateur gestionUtilisateur = new GestionUtilisateur();

            try
            {
                gestionFichiers.ChargerFichier();
                gestionUtilisateur.ChargerDonneesUtilisateurs(gestionFichiers);
            }
            catch (NullReferenceException e)
            {

            }

            if (gestionUtilisateur.UnUtilisateur != null)
            {
                UtilisateurExistant utilisateurExistant = new UtilisateurExistant(gestionUtilisateur.UnUtilisateur);
                Navigation.PushModalAsync(utilisateurExistant, true);
            }
            else
            {
                NouvelUtilisateur nouvelUtilisateurPage = new NouvelUtilisateur();
                Navigation.PushModalAsync(nouvelUtilisateurPage, true);
            }
        }

        #endregion
    }
}

