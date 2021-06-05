using System;
using Training_Mobile_App.Models.FctBiblio;

namespace Training_Mobile_App.Models.Classes
{
    public class GestionUtilisateur
    {
        #region Attributs

        /// <summary>
        /// Attribus de type Utilisateur, représente l'utilisateur qui utilise l'application.
        /// </summary>
        private Utilisateur _unUtilisateur;

        #endregion

        #region Get/Set

        /// <summary>
        /// Get,Set Obtenir ou modiffier l'utilisateur de l'application
        /// </summary>
        public Utilisateur UnUtilisateur
        {
            get { return _unUtilisateur; }
            set { _unUtilisateur = value; }
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Chargement des données de l'utilisateur de la classe gestionFichier,
        /// puis mets les valeur dans l'attribus.
        /// </summary>
        public void ChargerDonneesUtilisateurs(GestionFichiers gestionFichiers)
        {

            UnUtilisateur = new Utilisateur(
                gestionFichiers.DonneesUtilisateurSortie[0],
                gestionFichiers.DonneesUtilisateurSortie[1],
                byte.Parse(gestionFichiers.DonneesUtilisateurSortie[2]),
                (Sexe)Enum.Parse(typeof(Sexe), gestionFichiers.DonneesUtilisateurSortie[3]),
                float.Parse(gestionFichiers.DonneesUtilisateurSortie[4]),
                float.Parse(gestionFichiers.DonneesUtilisateurSortie[5]),
                float.Parse(gestionFichiers.DonneesUtilisateurSortie[6]),
                float.Parse(gestionFichiers.DonneesUtilisateurSortie[7]));
        }

        /// <summary>
        /// Reçoit en paramètre les informations pour la création d'un utiisateur.
        /// Fait en sorte de créé un utilisateur, de le mettre dans l'attribut de la
        /// class gestionUtilisateur pui fait en sorte que l'utilisateur est enregistrer
        /// dans un fichier CSV en appelant la méthode AjoutNouvelUtilisateur de la
        /// class gestionFichiers.
        /// </summary>
        public void InscrireUtilisateur(string pNom, string pPrenom, byte pAge, Sexe pSexe,
            float pPoids, float pGrandeur, float pMasseGrasse, float pMasseMusculaire)
        {
            GestionFichiers gestionFichiers = new GestionFichiers();

            UnUtilisateur = new Utilisateur(
                pNom, pPrenom, pAge, pSexe, pPoids, pGrandeur, pMasseGrasse, pMasseMusculaire);

            gestionFichiers.AjoutNouvelUtilisateur(UnUtilisateur);
            gestionFichiers.EnregistrerProgres(
                UnUtilisateur.Poids.ToString(),
                UnUtilisateur.MasseMusculaire.ToString(),
                UnUtilisateur.MasseGrasse.ToString());
        }

        #endregion
    }
}