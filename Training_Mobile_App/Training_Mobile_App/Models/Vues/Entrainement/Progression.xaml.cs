using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using Training_Mobile_App.Models.Classes;
using Training_Mobile_App.Models.FctBiblio;

namespace Training_Mobile_App.Models.Vues.Entrainement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Progression : ContentPage
    {
        #region Attributs

        /// <summary>
        /// Initialisation de la classe son.
        /// </summary>
        private AudioBouton _clickAudio = new AudioBouton();

        /// <summary>
        /// Attribut qui représente le moment sélectionner.
        /// </summary>
        private string _momentSelect;

        /// <summary>
        /// Attributs de type Exercice, représente l'exercice qui est sélectionner.
        /// </summary>
        private Exercice _exerciceSelectionner;

        /// <summary>
        /// Attribues de type List<Exercice>. Représente la liste des exercices.
        /// </summary>
        private List<Exercice> _lstExercices;

        /// <summary>
        /// Conserve la progression des exercices chargées depuis conteneurPrincipal.
        /// </summary>
        private List<TupleEnListe> _progressionExercices;

        /// <summary>
        /// Attribus représente les information du graphique.
        /// </summary>
        private OxyPlotInfo _oxyPlotInfo;

        #endregion

        #region Get/Set

        /// <summary>
        /// Get/Set de _progressionExercices.
        /// </summary>
        public List<TupleEnListe> ProgressionExercices
        {
            get { return _progressionExercices; }
            set { _progressionExercices = value; }
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
        /// Get/Set représente la liste des exercices.
        /// </summary>
        public List<Exercice> LstExercices
        {
            get { return _lstExercices; }
            set { _lstExercices = value; }
        }

        /// <summary>
        /// Get/Set permet de modifier ou d'obtenir le moment selectionner.
        /// </summary>
        public string MomentSelect
        {
            get { return _momentSelect; }
            set { _momentSelect = value; }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur pour la page Progression.
        /// </summary>
        public Progression(List<TupleEnListe> pProgressionExercice, List<Exercice> pLstExercices)
        {
            InitializeComponent();
            LstExercices = pLstExercices;
            ProgressionExercices = pProgressionExercice;

            Init();
            AfficherLstExercice();
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Permet d'initialiser le graphiques.
        /// </summary>
        public void Init()
        {
            _oxyPlotInfo = new OxyPlotInfo();
            MomentSelect = "anneeActuelle";
            UpdateGraphique();
        }

        /// <summary>
        /// Ce produit quand un moment est sélectionner.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MomentFiltrerDonnerGraphique_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                switch (picker.Items[selectedIndex])
                {
                    case "Mois précédent":
                        MomentSelect = "moisPrecedent";
                        break;
                    case "Mois présent":
                        MomentSelect = "moisPresent";
                        break;                   
                    default :
                        MomentSelect = "anneeActuelle";
                        break;

                }
                UpdateGraphique();
            }
        }

        /// <summary>
        /// Ce produit quand un exercices est sélectionner.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExerciceFiltrerDonnerGraphique_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            string exerciceSelect = "";
            if (selectedIndex != -1)
            { exerciceSelect = picker.Items[selectedIndex]; }

            bool trouver = false;
            int i = 0;
            while (!trouver)
            {
                if (LstExercices[i].NomExercice == exerciceSelect)
                {
                    trouver = true;
                    ExerciceSelectionner = LstExercices[i];
                }
                else
                {
                    i++;
                }
            }
            UpdateGraphique();
        }

        /// <summary>
        /// Quand le bouton de recomandation des exercices est clicker, affiche la fenêtre.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_OnClickedRecommendationExercice(object sender, EventArgs e)
        {
            _clickAudio.JouerSon();
            FctBiblio.FctBiblio ftBiblio = new FctBiblio.FctBiblio();
            string msg = ftBiblio.RecommendationExercice(ProgressionExercices);
            DisplayAlert("Information!", msg, "OK");
        }

        /// <summary>
        /// Permet de metre a jour/et de modifier le graphique.
        /// </summary>
        private void UpdateGraphique()
        {
            TriageDonnees triageDonnees = new TriageDonnees();

            PlotModel plotModel = new PlotModel
            {
                Title = "Progression utilisateur (Charge)",
                TextColor = OxyColor.FromRgb(0, 0, 0),
                Background = OxyColor.FromRgb(235, 235, 235)
            };
            BarSeries ps = new BarSeries()
            {
                TextColor = OxyColor.FromRgb(0, 0, 0)

            };

            CategoryAxis xaxis = new CategoryAxis();
            xaxis.Position = AxisPosition.Left;
            xaxis.MajorGridlineStyle = LineStyle.Solid;
            xaxis.MinorGridlineStyle = LineStyle.Dot;

            List<TupleEnListe> lstDonnees = new List<TupleEnListe>();
            lstDonnees = triageDonnees.TriDonnees(MomentSelect, ProgressionExercices);

            if (MomentSelect == "moisPresent")
            {
                List<OxyPlotItems> items = new List<OxyPlotItems>();

                for (int j = 0; j < lstDonnees.Count; j++)
                {
                    if (ExerciceSelectionner != null && !string.IsNullOrEmpty(MomentSelect))
                    {
                        if (ExerciceSelectionner.NomExercice == lstDonnees[j].ProgUser.InfoExercice.NomExercice)
                        {
                            string nomExercice = lstDonnees[j].ProgUser.InfoExercice.NomExercice;
                            double value = lstDonnees[j].ProgUser.Poids;

                            items.Add(new OxyPlotItems { value = value, color = OxyColor.FromRgb(27, 126, 239) });
                            xaxis.Labels.Add(nomExercice);
                        }
                    }
                }

                _oxyPlotInfo.Items = items;

                int i = 0;
                foreach (var oxyitem in _oxyPlotInfo.Items)
                {
                    var bar = new BarItem(oxyitem.value, i);
                    bar.Color = oxyitem.color;
                    ps.Items.Add(bar);
                    i++;
                }

                plotModel.Axes.Add(xaxis);
                plotModel.Series.Add(ps);
                this.plotmodel.Model = plotModel;
            }
            else if (MomentSelect == "moisPrecedent")
            {
                List<OxyPlotItems> items = new List<OxyPlotItems>();

                for (int j = 0; j < lstDonnees.Count; j++)
                {
                    if (ExerciceSelectionner != null && !string.IsNullOrEmpty(MomentSelect))
                    {
                        if (ExerciceSelectionner.NomExercice == lstDonnees[j].ProgUser.InfoExercice.NomExercice)
                        {
                            string nomExercice = lstDonnees[j].ProgUser.InfoExercice.NomExercice;
                            double value = lstDonnees[j].ProgUser.Poids;
                            items.Add(new OxyPlotItems { value = value, color = OxyColor.FromRgb(27, 126, 239) });
                            xaxis.Labels.Add(nomExercice);
                        }
                    }
                }

                _oxyPlotInfo.Items = items;

                int i = 0;
                foreach (var oxyitem in _oxyPlotInfo.Items)
                {
                    var bar = new BarItem(oxyitem.value, i);
                    bar.Color = oxyitem.color;
                    ps.Items.Add(bar);
                    i++;
                }

                plotModel.Axes.Add(xaxis);
                plotModel.Series.Add(ps);

                this.plotmodel.Model = plotModel;
            }
            else if (MomentSelect == "anneeActuelle")
            {
                List<OxyPlotItems> items = new List<OxyPlotItems>();

                for (int j = 0; j < lstDonnees.Count; j++)
                {
                    if (ExerciceSelectionner != null && !string.IsNullOrEmpty(MomentSelect))
                    {
                        if (ExerciceSelectionner.NomExercice == lstDonnees[j].ProgUser.InfoExercice.NomExercice)
                        {
                            string nomExercice = lstDonnees[j].ProgUser.InfoExercice.NomExercice;
                            double value = lstDonnees[j].ProgUser.Poids;

                            items.Add(new OxyPlotItems { value = value, color = OxyColor.FromRgb(27, 126, 239) });
                            xaxis.Labels.Add(nomExercice);
                        }
                    }
                }

                _oxyPlotInfo.Items = items;

                int i = 0;
                foreach (var oxyitem in _oxyPlotInfo.Items)
                {
                    var bar = new BarItem(oxyitem.value, i);
                    bar.Color = oxyitem.color;
                    ps.Items.Add(bar);
                    i++;
                }

                plotModel.Axes.Add(xaxis);
                plotModel.Series.Add(ps);

                this.plotmodel.Model = plotModel;
            }
        }

        /// <summary>
        /// Afficher la liste des exercice dans un dropdown.
        /// </summary>
        private void AfficherLstExercice()
        {
            List<string> strExercice = new List<string>();
            foreach (Exercice elem in _lstExercices)
            {
                strExercice.Add(elem.NomExercice);
            }
            lstExercicesXAML.ItemsSource = strExercice;
        }

        #endregion
    }

    #region Classe Pour Graphique

    /// <summary>
    /// Class d'information pour le graphique.
    /// </summary>
    public class OxyPlotInfo
    {
        #region Get/Set

        /// <summary>
        /// Get/Set de l'info
        /// </summary>
        public string info { get; set; }

        #endregion

        #region Attributs

        public ICollection<OxyPlotItems> Items;

        #endregion
    }

    /// <summary>
    /// Class pour les items du graphique
    /// </summary>
    public class OxyPlotItems
    {
        #region Get/Set

        /// <summary>
        /// Get/SEt de la value
        /// </summary>
        public double value { get; set; }

        /// <summary>
        /// Get/Set de la couleur
        /// </summary>
        public OxyColor color { get; set; }

        #endregion
    }

    #endregion
}