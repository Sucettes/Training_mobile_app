using Training_Mobile_App.Models.Classes;

namespace Training_Mobile_App.Models.FctBiblio
{
    /// <summary>
    /// Objet qui sauvegarde en mémoire temporaire
    /// les progressions d'un utilisateur et chacun de ses exercices.
    /// </summary>
    public class ProgressionUtilisateur
    {
        #region Attributs

        /// <summary>
        /// Attribus de type Exercice. Représente les exerices.
        /// </summary>
        private Exercice _exercice;
        /// <summary>
        /// Attribus de type float représente le poids.
        /// </summary>
        private float _poids;
        /// <summary>
        /// Attribus de type byte représente le nombre de répétition.
        /// </summary>
        private byte _nbReps;
        /// <summary>
        /// Attribus de type byte représente le nombre de series.
        /// </summary>
        private byte _nbSeries;

        #endregion

        #region Get/Set

        /// <summary>
        /// Get/Set permet de modifier ou d'obtenir l'exercices.
        /// </summary>
        public Exercice InfoExercice
        {
            get { return _exercice; }
            set { _exercice = value; }
        }
        /// <summary>
        /// Get/Set permet de modifier ou d'obtenir le poids.
        /// </summary>
        public float Poids
        {
            get { return _poids; }
            set { _poids = value; }
        }
        /// <summary>
        /// Get/Set permet de modifier ou d'obtenir le nombre de repétitions.
        /// </summary>
        public byte NbReps
        {
            get { return _nbReps; }
            set { _nbReps = value; }
        }
        /// <summary>
        /// Get/Set permet de modifier ou d'obtenir le nombres de series.
        /// </summary>
        public byte NbSeries
        {
            get { return _nbSeries; }
            set { _nbSeries = value; }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur, permet d'initialiser les attribues avec les valeurs recus en paramètre.
        /// </summary>
        /// <param name="pExercice">Exercice</param>
        /// <param name="pPoids">poids</param>
        /// <param name="pNbReps">nombre de répétitions</param>
        /// <param name="pNbSeries">nombre de séries</param>
        public ProgressionUtilisateur(Exercice pExercice, float pPoids,
            byte pNbReps, byte pNbSeries)
        {
            InfoExercice = pExercice;
            Poids = pPoids;
            NbReps = pNbReps;
            NbSeries = pNbSeries;
        }

        #endregion
    }
}