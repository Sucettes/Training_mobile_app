using System.Collections.Generic;
using Training_Mobile_App.Models.Vues.Entrainement;
using Training_Mobile_App.Models.Vues.exercices;
using Training_Mobile_App.Models.Vues.Utilisateur;
using Training_Mobile_App.Models.Classes;
using Training_Mobile_App.Models.FctBiblio;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Training_Mobile_App.Models.Vues.ConteneurPrincipal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConteneurPrincipal : TabbedPage
    {
        #region Constructeur

        /// <summary>
        /// Constructeur pour le conteneurPrincipal
        /// </summary>
        public ConteneurPrincipal()
        {
            GestionFichiers gestionFichiers = new GestionFichiers();
            gestionFichiers.ChargerFichier();
            List<TupleEnListe> progressionExercice = gestionFichiers.ProgressionExercices;
            GestionExercice gestionExercice = new GestionExercice();
            gestionFichiers.PreparationListeExercicesAffichage(gestionExercice);
            List<Exercice> lstExercices = gestionExercice.LstExercices;

            InitializeComponent();
            Color CouleurOnglet = Color.FromRgb(242, 19,19);
            this.BarBackgroundColor = CouleurOnglet;

            Children.Add(new JourPrecedent(progressionExercice) { Title = "Dernier entraînement"});
            Children.Add(new listeexercices(lstExercices) {Title = "Liste d'exercices" });
            Children.Add(new Progression(progressionExercice, lstExercices) { Title = "Statistiques"});
            Children.Add(new ModifierUtilisateur(gestionFichiers, gestionFichiers.ProgressionPoidsUtilisateur) { Title = "Modifier Utilisateur"});
        }

        #endregion

        #region Override

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