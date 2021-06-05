using System;

namespace Training_Mobile_App.Models.FctBiblio
{
    public class TupleEnListe
    {
        #region Attributs

        /// <summary>
        /// Attribus de type ProgressionUtilisateur, représente la progression de l'utilisateur.
        /// </summary>
        private ProgressionUtilisateur _progUser;
        /// <summary>
        /// Attribus de type DateTime, représente la date.
        /// </summary>
        private DateTime date;

        #endregion

        #region Get/Set

        /// <summary>
        ///  Get/Set permet de modifier ou d'obtenir la Date
        /// </summary>
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        /// <summary>
        /// Get/Set permet de modifier ou d'obtenir ProgUser
        /// </summary>
        public ProgressionUtilisateur ProgUser
        {
            get { return _progUser; }
            set { _progUser = value; }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Transforme les informations de progression de l'utilisateur en tuple.
        /// </summary>
        /// <param name="pProgUser"></param>
        /// <param name="pDate"></param>
        public TupleEnListe(ProgressionUtilisateur pProgUser, DateTime pDate)
        {
            Date = pDate;
            ProgUser = pProgUser;
        }

        #endregion
    }
}
