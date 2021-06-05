using Training_Mobile_App.Models.FctBiblio;

namespace Training_Mobile_App.Models.Classes
{
    public class Exercice
    {
        #region Attributs

        /// <summary>
        /// Attribus string, représente le nom de l'exercice.
        /// </summary>
        private string _nomExercice;
        /// <summary>
        /// Attribbus Enum PartieDuCorpsTravailler, représente la partie du corps qui
        /// est travailler.
        /// </summary>
        private PartieDuCorpsTravailler _partieDuCorpsTravailler;
        /// <summary>
        /// Attribus string, représente la description de l'exercices.
        /// </summary>
        private string _description;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur, permet de crée un exercice.
        /// Prends en paramètre le nom, la partie du corps travailler et la description.
        /// </summary>
        /// <param name="pNomExercice">String, nom de l'exercice.</param>
        /// <param name="pPartieDuCorpsTravailler">Enum PartieDuCorpsTravailler, partie du corps qui est travailler.</param>
        /// <param name="pDescription">String, la description de l'exercice.</param>
        public Exercice(string pNomExercice, PartieDuCorpsTravailler pPartieDuCorpsTravailler, string pDescription)
        {
            NomExercice = pNomExercice;
            PartieDuCorpsTravailler = pPartieDuCorpsTravailler;
            Description = pDescription;
        }

        #endregion

        #region Get/Set

        /// <summary>
        /// Get,Set Obtenir ou modiffier le nom de l'exercice.
        /// </summary>
        public string NomExercice
        {
            get { return _nomExercice; }
            set { _nomExercice = value; }
        }
        /// <summary>
        /// Get,Set Obtenir ou modiffier la partie du corps qui est travailler par l'exercice.
        /// </summary>
        public PartieDuCorpsTravailler PartieDuCorpsTravailler
        {
            get { return _partieDuCorpsTravailler; }
            set { _partieDuCorpsTravailler = value; }
        }
        /// <summary>
        /// Get,Set Obtenir ou modiffier la description de l'exercice.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        #endregion
    }
}