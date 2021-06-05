using System;
using System.Collections.Generic;
using Training_Mobile_App.Models.FctBiblio;

namespace Training_Mobile_App.Models.Classes
{
    public class TriageDonnees
    {
        #region Attributs

        /// <summary>
        /// Liste de la progression des exercices d'un utilisateur. La données
        /// provient de la classe progression.cs
        /// </summary>
        private List<TupleEnListe> _progressionExercice;

        #endregion

        #region Get/Set

        /// <summary>
        /// Get/Set de la liste de progression d'exercices.
        /// </summary>
        public List<TupleEnListe> ProgressionExercice
        {
            get { return _progressionExercice; }
            set { _progressionExercice = value; }
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Permet de trier les donner selon le string recus en paramètre.
        /// </summary>
        /// <param name="triChoisi">string pour faire le trie</param>
        /// <returns>La liste trier</returns>
        public List<TupleEnListe> TriDonnees(string triChoisi, List<TupleEnListe> pProgressionExercices)
        {
            ProgressionExercice = pProgressionExercices;
            DateTime dateAujourdhui = DateTime.Today;
            DateTime moisActuel = new DateTime(dateAujourdhui.Year, dateAujourdhui.Month, 1);
            DateTime premierJourDuMois;
            DateTime dernierJourDuMois;
            DateTime anneeActuelle;

            List<TupleEnListe> tupleExerciceTries = new List<TupleEnListe>();

            switch (triChoisi)
            {
                case "moisPresent":
                    dernierJourDuMois = moisActuel.AddDays(-1);
                    tupleExerciceTries = RechercheSelonDate(dernierJourDuMois, dateAujourdhui);
                    break;

                case "moisPrecedent":
                    premierJourDuMois = moisActuel.AddMonths(-1);
                    dernierJourDuMois = moisActuel.AddDays(-1);
                    tupleExerciceTries = RechercheSelonDate(dernierJourDuMois, premierJourDuMois);
                    break;

                case "anneeActuelle":
                    anneeActuelle = moisActuel.AddMonths(-12);
                    tupleExerciceTries = RechercheSelonDate(anneeActuelle, dateAujourdhui);
                    break;
            }

            return tupleExerciceTries;
        }

        /// <summary>
        /// Récupère les progressions en partant de la date la plus proche vers la
        /// de départ du temps choisi.
        /// </summary>
        /// <param name="dateDepart"></param>
        /// <param name="dateLaPlusRecente"></param>
        /// <param name="listeATriee"></param>
        /// <returns></returns>
        private List<TupleEnListe> RechercheSelonDate(DateTime dateDepart, DateTime dateLaPlusRecente)
        {
            List<TupleEnListe> tupleDeRetour = new List<TupleEnListe>();

            int compteur = 0;

            while (dateDepart <= dateLaPlusRecente && compteur < ProgressionExercice.Count)
            {
                tupleDeRetour.Add(ProgressionExercice[compteur]);
                compteur++;
                dateLaPlusRecente = dateLaPlusRecente.AddDays(-1);
            }

            return tupleDeRetour;
        }

        #endregion
    }
}
