using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Training_Mobile_App.Models.Classes;
using Training_Mobile_App.Models.FctBiblio;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;

namespace Training_Mobile_App.Models.Vues.Entrainement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NouvelEntrainement : ContentPage
    {
        #region Attributs

        /// <summary>
        /// Attribues de type List<Exercice>. Représente la liste des exercices.
        /// </summary>
        private List<Exercice> _lstExercices;

        /// <summary>
        /// Initialisation de la classe son.
        /// </summary>
        private AudioBouton _clickAudio = new AudioBouton();

        /// <summary>
        /// Attributs de type list exercice, représente la liste des exercices qui sont ajouté a l'entrainement.
        /// </summary>
        private List<Exercice> _lstExercicesAjouter;

        /// <summary>
        /// Attributs de type Exercice, représente l'exercice qui est sélectionner.
        /// </summary>
        private Exercice _exerciceSelectionner;

        /// <summary>
        /// Attributs de type Exercice, représente l'exercice sélectioner a supprimer.
        /// </summary>
        private Exercice _exerciceSelectionnerASupp;

        /// <summary>
        /// Attributs de type list<TupleEnListe>, représente la liste des exercice en tuple.
        /// </summary>
        private List<TupleEnListe> _listTupleExercice;

        /// <summary>
        /// GET/SET Liste des items de la premiere liste des exercice (la liste complette)
        /// </summary>
        public ObservableCollection<MyListModel> Items { get; set; }

        #endregion

        #region Get/Set

        /// <summary>
        /// GET/SET Liste des items de la deuxiene liste des exercice (la liste de l'entrainement)
        /// </summary>
        public ObservableCollection<MyListModel> Items2 { get; set; }

        /// <summary>
        /// Get/set permet de modifier ou d'obtenir ListTupleExercice
        /// </summary>
        public List<TupleEnListe> ListTupleExercice
        {
            get { return _listTupleExercice; }
            set { _listTupleExercice = value; }
        }

        /// <summary>
        /// Get/Set permet de modifier ou d'obtenir LstExercices
        /// </summary>
        public List<Exercice> LstExercices
        {
            get { return _lstExercices; }
            set { _lstExercices = value; }
        }

        /// <summary>
        /// Get/Set permet de modifier ou d'obtenir LstExercicesAjouter.
        /// </summary>
        public List<Exercice> LstExercicesAjouter
        {
            get { return _lstExercicesAjouter; }
            set { _lstExercicesAjouter = value; }
        }

        /// <summary>
        /// Get/Set permet de modifier ou d'obtenir ExerciceSelectionner
        /// </summary>
        public Exercice ExerciceSelectionner
        {
            get { return _exerciceSelectionner; }
            set { _exerciceSelectionner = value; }
        }

        /// <summary>
        /// Get/Set pour l'exercice a supprimer
        /// </summary>
        public Exercice ExerciceSelectionnerASupp
        {
            get { return _exerciceSelectionnerASupp; }
            set { _exerciceSelectionnerASupp = value; }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public NouvelEntrainement()
        {
            InitializeComponent();

            _listTupleExercice = new List<TupleEnListe>();
            _lstExercicesAjouter = new List<Exercice>();
            GestionExercice gestionExercice = new GestionExercice();
            GestionFichiers gestionFichiers = new GestionFichiers();
            gestionFichiers.PreparationListeExercicesAffichage(gestionExercice);
            LstExercices = gestionExercice.LstExercices;

            AfficherLstExercice();
        }

        #endregion

        #region Méthodes


        /// <summary>
        /// Afficher la liste des exercice dans la liste.
        /// </summary>
        private void AfficherLstExercice()
        {
            Items = new ObservableCollection<MyListModel>();
            FctBiblio.FctBiblio fctBiblio = new FctBiblio.FctBiblio();

            ImageSource source = new FileImageSource();
            foreach (var exercice in LstExercices)
            {
                string imgName = exercice.NomExercice.Trim();
                imgName = Regex.Replace(imgName, " ", "_");
                source = ImageSource.FromResource("Training_Mobile_App.Resources." + imgName + ".png");


                string txt = exercice.NomExercice;
                string desc = exercice.PartieDuCorpsTravailler.ToString();
                BindingContext = new MyListPageViewModel(Items, txt, desc, source);
            }

            lstExercicesXAML.ItemsSource = Items;
        }

        /// <summary>
        /// Évènement quand le bouton Ajouter est clicker, permet d'ajouter l'exercice a l'entrainement.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonClickedAjouter(object sender, EventArgs e)
        {
            try
            {
                _clickAudio.JouerSon();
                float poids;
                byte nbRep;
                byte nbSerie;

                if (string.IsNullOrEmpty(poidsExercice.Text))
                {
                    throw new ArgumentNullException("Le poids de l'exercice ne doit pas être vide.", (Exception)null);
                }

                if (float.TryParse(poidsExercice.Text, out float poidsExerciceSortie))
                {
                    poids = poidsExerciceSortie;
                }
                else
                {
                    throw new ArgumentException("Le poids de l'exercice doit être un nombre.");
                }

                if (string.IsNullOrEmpty(nbSerieExercice.Text))
                {
                    throw new ArgumentNullException("Le nombre de série ne doit pas être vide.", (Exception)null);
                }

                if (byte.TryParse(nbSerieExercice.Text, out byte nbSerieSortie))
                {
                    nbSerie = nbSerieSortie;
                }
                else
                {
                    throw new ArgumentException("Le nombre de série doit être un nombre entier.");
                }

                if (string.IsNullOrEmpty(nbRepExercice.Text))
                {
                    throw new ArgumentNullException("Le nombre de répétitions ne doit pas être vide.",
                        (Exception)null);
                }

                if (byte.TryParse(nbRepExercice.Text, out byte nbRepSortie))
                {
                    nbRep = nbRepSortie;
                }
                else
                {
                    throw new ArgumentException("Le nombre de répétitions doit être un nombre entier.");
                }

                ProgressionUtilisateur progressionUtilisateur =
                    new ProgressionUtilisateur(ExerciceSelectionner, poids, nbRep, nbSerie);

                DateTime date = DateTime.Today;

                TupleEnListe tupleExercice = new TupleEnListe(progressionUtilisateur, date);

                ListTupleExercice.Add(tupleExercice);

                LstExercicesAjouter.Add(ExerciceSelectionner);

                Items2 = new ObservableCollection<MyListModel>();
                ImageSource source = new FileImageSource();
                foreach (var exercice in LstExercicesAjouter)
                {

                    string imgName = exercice.NomExercice.Trim();
                    imgName = Regex.Replace(imgName, " ", "_");
                    source = ImageSource.FromResource("Training_Mobile_App.Resources." + imgName + ".png");
                    string txt = exercice.NomExercice;
                    string desc = exercice.PartieDuCorpsTravailler.ToString();
                    BindingContext = new MyListPageViewModel(Items2, txt, desc, source);
                }
                lstExercicesAjouterXAML.ItemsSource = Items2;

                lstExercicesXAML.SelectedItem = null;
                poidsExercice.Text = null;
                nbSerieExercice.Text = null;
                nbRepExercice.Text = null;
            }
            catch (Exception exeption)
            {
                DisplayAlert("Erreur", exeption.Message, "Ok");
            }
        }

        /// <summary>
        /// Méthode pour enregistrer les donnees
        /// </summary>
        private void EnregistrerDonnee()
        {
            GestionFichiers gestionFichiers = new GestionFichiers();

            foreach (TupleEnListe TupleEnListe in ListTupleExercice)
            {
                gestionFichiers.EnregistrementProgressionExercice(TupleEnListe);
            }
        }

        /// <summary>
        /// évènement quand le bouton supprimer est clicker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonClickedSupprimer(object sender, EventArgs e)
        {
            try
            {
                _clickAudio.JouerSon();
                if (ExerciceSelectionnerASupp is null)
                {
                    throw new ArgumentNullException("Aucun exercice sélectionné.", (Exception)null);
                }

                int position = 0;
                foreach (var exercice in ListTupleExercice)
                {
                    if (ExerciceSelectionnerASupp.NomExercice == exercice.ProgUser.InfoExercice.NomExercice)
                    {
                        continue;
                    }

                    position++;
                }

                int positionAjt = 0;
                foreach (var exercice in LstExercicesAjouter)
                {
                    if (ExerciceSelectionnerASupp.NomExercice == exercice.NomExercice)
                    {
                        continue;
                    }

                    positionAjt++;
                }

                ListTupleExercice.RemoveAt(position);
                LstExercicesAjouter.RemoveAt(positionAjt);

                lstExercicesAjouterXAML.SelectedItem = null;

                Items2 = new ObservableCollection<MyListModel>();
                ImageSource source = new FileImageSource();
                foreach (var exercice in LstExercicesAjouter)
                {

                    string imgName = exercice.NomExercice.Trim();
                    imgName = Regex.Replace(imgName, " ", "_");
                    source = ImageSource.FromResource("Training_Mobile_App.Resources." + imgName + ".png");

                    string txt = exercice.NomExercice;
                    string desc = exercice.PartieDuCorpsTravailler.ToString();
                    BindingContext = new MyListPageViewModel(Items2, txt, desc, source);
                }
                lstExercicesAjouterXAML.ItemsSource = Items2;
            }
            catch (Exception exeption)
            {
                DisplayAlert("Error", exeption.Message, "Ok");
            }
        }

        /// <summary>
        /// Évènement quand le bouton Suivant est cliker. Redirection sur la page JourPrecedent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonClickedSuivant(object sender, EventArgs e)
        {
            _clickAudio.JouerSon();
            EnregistrerDonnee();
            Application.Current.MainPage.Navigation.PushModalAsync(new ConteneurPrincipal.ConteneurPrincipal(), true);
        }

        /// <summary>
        /// Permet de continuer sans sauvegarder les données.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void continuerSansEnregistrer(object sender, EventArgs e)
        {
            _clickAudio.JouerSon();
            Application.Current.MainPage.Navigation.PushModalAsync(new ConteneurPrincipal.ConteneurPrincipal(), true);
        }

        /// <summary>
        /// Évènement quand un item de la liste d'exercice est sélectionner. Permet de savoir
        /// quelle exercice est selectionner.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            List<Exercice> lstExercicesTempo = LstExercices;

            bool trouver = false;
            int position = 0;

            int i = 0;
            int j = 0;
            do
            {
                object test = e.Item;
                if (test == Items[i])
                {
                    trouver = true;
                    j = i;
                }

                i++;

            } while (trouver == false);

            trouver = false;
            i = 0;
            do
            {
                if (lstExercicesTempo[i].NomExercice == Items[j].txt)
                {
                    position = i;
                    trouver = true;
                }

                i++;
            } while (trouver == false);

            ExerciceSelectionner = lstExercicesTempo[position];

            string msg = string.Format("Nom: {0}\nPartie du corps: {1}\n\nDescription:\n{2}", ExerciceSelectionner.NomExercice,
                ExerciceSelectionner.PartieDuCorpsTravailler, ExerciceSelectionner.Description);

            DisplayAlert("Information!", msg, "OK");
        }

        /// <summary>
        /// Évènement quand un item est sélectionner dans la liste exerciceAjouterXAML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Handle_ItemTappedAjouter(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            List<Exercice> lstExercicesTempo = LstExercicesAjouter;

            bool trouver = false;
            int position = 0;

            int i = 0;
            int j = 0;
            do
            {
                object test = e.Item;
                if (test == Items2[i])
                {
                    trouver = true;
                    j = i;
                }

                i++;

            } while (trouver == false);

            trouver = false;
            i = 0;
            do
            {
                if (lstExercicesTempo[i].NomExercice == Items2[j].txt)
                {
                    position = i;
                    trouver = true;
                }

                i++;
            } while (trouver == false);

            ExerciceSelectionnerASupp = lstExercicesTempo[position];
        }

        /// <summary>
        /// Class pour faire fonctionner la liste des exercices.
        /// </summary>
        public class MyListPageViewModel
        {
            #region Constructeur

            /// <summary>
            /// Constructeur
            /// </summary>
            /// <param name="Items">Items</param>
            /// <param name="pText">texte</param>
            /// <param name="pDesc">Description</param>
            /// <param name="pImage">source de l'image</param>
            public MyListPageViewModel(ObservableCollection<MyListModel> Items, string pText, string pDesc, ImageSource pImage)
            {
                Items.Add(new MyListModel { img = pImage, desc = pDesc, txt = pText });
            }

            #endregion
        }

        /// <summary>
        /// Class pour faire fonctionner la liste des exercices.
        /// </summary>
        public class MyListModel
        {
            #region Attributs

            /// <summary>
            /// Source de l'image
            /// </summary>
            private ImageSource _img;
            /// <summary>
            /// Description
            /// </summary>
            private string _desc;
            /// <summary>
            /// texte
            /// </summary>
            private string _txt;

            #endregion

            #region Get/Set

            /// <summary>
            /// Permet d'obtenir ou de modifier la source de l'image.
            /// </summary>
            public ImageSource img
            {
                get { return _img; }
                set { _img = value; }
            }

            /// <summary>
            /// Permet d'obtenir ou de modifier la description
            /// </summary>
            public string desc
            {
                get { return _desc; }
                set { _desc = value; }
            }

            /// <summary>
            /// Permet d'obtenir ou de modifier le texte
            /// </summary>
            public string txt
            {
                get { return _txt; }
                set { _txt = value; }
            }

            #endregion
        }
        #endregion
    }
}