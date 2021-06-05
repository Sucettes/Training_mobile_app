using System.Collections.Generic;

namespace Training_Mobile_App.Models.Classes
{
    public class GestionExercice
    {
        #region Attributs

        /// <summary>
        /// Attribues _lstExercices de type liste d'exercice.
        /// Représente la liste de touts les exercices.
        /// </summary>
        private List<Exercice> _lstExercices = new List<Exercice>();

        #endregion

        #region Get/Set

        /// <summary>
        /// Get, Set pour la liste LstExercices de type Exercice.
        /// </summary>
        public List<Exercice> LstExercices
        {
            get { return _lstExercices; }
            set { _lstExercices = value; }
        }

        #endregion
    }
}