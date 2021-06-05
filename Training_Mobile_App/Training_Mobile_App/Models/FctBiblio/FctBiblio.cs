using System;
using System.Collections.Generic;
using System.Linq;

namespace Training_Mobile_App.Models.FctBiblio
{
    /// <summary>
    /// Liste de fonctions utiles pour plusieurs méthodes.
    /// </summary>
    public class FctBiblio
    {
        #region Méthodes

        /// <summary>
        /// Compile les exercices depuis les 30 derniers jours pour faire
        /// des proposition à l'utilisateur.
        ///compteurPartieCorps[0] Bras
        ///compteurPartieCorps[1] Dos
        ///compteurPartieCorps[2] Pect
        ///compteurPartieCorps[3] Epaule
        ///compteurPartieCorps[4] Abdo
        ///compteurPartieCorps[5] Jambes
        /// </summary>
        /// <param name="gestionFichier"></param>
        /// <returns>un string des recomendation</returns>
        public string RecommendationExercice(List<TupleEnListe> pProgressionExercices)
        {
            string recommendation;
            DateTime dateAujourdhui = DateTime.Today;
            DateTime premierJournee = dateAujourdhui.AddDays(-30);

            int[] compteurPartieCorps = new int[6];

            foreach (TupleEnListe element in pProgressionExercices)
            {
                if (element.Date >= premierJournee)
                {
                    switch (element.ProgUser.InfoExercice.PartieDuCorpsTravailler)
                    {
                        case PartieDuCorpsTravailler.Abdos:
                            compteurPartieCorps[0]++;
                            break;
                        case PartieDuCorpsTravailler.Bras:
                            compteurPartieCorps[1]++;
                            break;
                        case PartieDuCorpsTravailler.Dos:
                            compteurPartieCorps[2]++;
                            break;
                        case PartieDuCorpsTravailler.Epaule:
                            compteurPartieCorps[3]++;
                            break;
                        case PartieDuCorpsTravailler.Jambes:
                            compteurPartieCorps[4]++;
                            break;
                        case PartieDuCorpsTravailler.Pect:
                            compteurPartieCorps[5]++;
                            break;
                    }
                }
            }
            
            List<int> lstTemp = compteurPartieCorps.ToList();
            lstTemp.Sort();

            int[] donneesCompilees = lstTemp.ToArray();

            recommendation = MessageRecommendation(donneesCompilees, compteurPartieCorps);

            return recommendation;
        }

        /// <summary>
        /// Renvoi un message de suggestion pour travailler les 3 parties du corps
        /// les moins travaillées.
        /// </summary>
        /// <returns>Le message de recommendation du top 3 des exercices les moins
        /// fait.</returns>
        private string MessageRecommendation(int[] arrayTrier, int[] donnessCompilees)
        {

            Queue<int> qteExecutionExercice  = new Queue<int>();
            qteExecutionExercice.Enqueue(arrayTrier[0]);
            qteExecutionExercice.Enqueue(arrayTrier[1]);
            qteExecutionExercice.Enqueue(arrayTrier[2]);
            string messageFormatter = "";

            while (qteExecutionExercice.Count > 0)
            {
                int nbExecutionExercice = qteExecutionExercice.Dequeue();

                if (nbExecutionExercice == donnessCompilees[0] && !messageFormatter.Contains("abdos"))
                { messageFormatter += "Vous devriez faire plus d'abdos. "; }
                else if(nbExecutionExercice == donnessCompilees[1] && !messageFormatter.Contains("bras"))
                { messageFormatter += "Vous devriez faire plus de bras. "; }
                else if (nbExecutionExercice == donnessCompilees[2] && !messageFormatter.Contains("dos"))
                { messageFormatter += "Vous devriez faire plus de dos. "; }
                else if (nbExecutionExercice == donnessCompilees[3] && !messageFormatter.Contains("épaules"))
                { messageFormatter += "Vous devriez faire plus d'épaules. "; }
                else if (nbExecutionExercice == donnessCompilees[4] && !messageFormatter.Contains("jambes"))
                { messageFormatter += "Vous devriez faire plus de jambes. "; }
                else { messageFormatter += "Vous devriez faire plus de pectoraux.. "; }
            }
            
            return messageFormatter;
        }

        #endregion
    }
}
