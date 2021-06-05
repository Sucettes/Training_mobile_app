using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Training_Mobile_App.Models.Classes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Training_Mobile_App.Models.Vues.exercices
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class listeexercices : ContentPage
    {
        #region Attributs
        public ObservableCollection<MyListModel> Items { get; set; }
        /// <summary>
        /// Attribues de type List<Exercice>. Représente la liste des exercices.
        /// </summary>
        private List<Exercice> lstExercices;
        #endregion

        #region Get/Set

        /// <summary>
        /// Get/Set permet de modifier ou d'obtenir LstExercices
        /// </summary>
        public List<Exercice> LstExercices
        {
            get { return lstExercices; }
            set { lstExercices = value; }
        }

        #endregion

        #region Constructeur
        /// <summary>
        /// Affichage de la liste d'exercice dans la vue.
        /// </summary>
        public listeexercices(List<Exercice> pLstExercices)
        {
            InitializeComponent();
            LstExercices = pLstExercices;

            Items = new ObservableCollection<MyListModel>();
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
        #endregion

        #region Méthodes

        /// <summary>
        /// Évènement quand un item de la liste des exercices est clicker.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void lstExercicesXAML_ItemTapped(object sender, ItemTappedEventArgs e)
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

            Exercice ExerciceSelectionner = lstExercicesTempo[position];

            string msg = string.Format("Nom: {0}\nPartie du corps: {1}\n\nDescription:\n{2}", ExerciceSelectionner.NomExercice,
                ExerciceSelectionner.PartieDuCorpsTravailler, ExerciceSelectionner.Description);

            DisplayAlert("Information!", msg, "OK");

        }

        #endregion

        #region Méthodes Affichage

        /// <summary>
        /// Class pour faire fonctionner la liste des exercices.
        /// </summary>
        public class MyListPageViewModel
        {
            /// <summary>
            /// Constructeur
            /// </summary>
            /// <param name="Items">L'items</param>
            /// <param name="pText">Le texte</param>
            /// <param name="pDesc">La description</param>
            /// <param name="pImage">L'image</param>
            public MyListPageViewModel(ObservableCollection<MyListModel> Items, string pText, string pDesc, ImageSource pImage)
            {
                Items.Add(new MyListModel { img = pImage, desc = pDesc, txt = pText });
            }
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
            /// Texte
            /// </summary>
            private string _txt;

            #endregion

            #region Get/Set

            /// <summary>
            /// Get/Set de la source de l'image
            /// </summary>
            public ImageSource img
            {
                get { return _img; }
                set { _img = value; }
            }

            /// <summary>
            /// Get/Set de la description
            /// </summary>
            public string desc
            {
                get { return _desc; }
                set { _desc = value; }
            }

            /// <summary>
            /// Get/Set de texte
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
