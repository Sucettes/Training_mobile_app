using Training_Mobile_App.Models.FctBiblio;

namespace Training_Mobile_App.Models.Classes
{
    public class Utilisateur
    {
        #region Attributs

        /// <summary>
        /// Attribus de type String. Représente le nom de l'utilisateur.
        /// </summary>
        private string _nom;
        /// <summary>
        /// Attribus de type String. Représente le prénom de l'utilisateur.
        /// </summary>
        private string _prenom;
        /// <summary>
        /// Attribus de type byte. Représente l'age de l'utilisateur.
        /// </summary>
        private byte _age;
        /// <summary>
        /// Attribus de type Sexe. Représente le sexe de l'utilisateur.
        /// </summary>
        private Sexe _genre;
        /// <summary>
        /// Attribus de type Float. Représente le poids de l'utilisateur.
        /// </summary>
        private float _poids;
        /// <summary>
        /// Attribus de type float. Représente la grandeur de l'utilisateur.
        /// </summary>
        private float _grandeur;
        /// <summary>
        /// Attribus de type float. Représente la masse Grasse de l'utilisateur.
        /// </summary>
        private float _masseGrasse;
        /// <summary>
        /// Attribus de type float. Représente la masse musculaire de l'utilisateur.
        /// </summary>
        private float _masseMusculaire;

        #endregion

        #region Get/Set

        /// <summary>
        /// Get/Set modiffier ou obtenir le nom de l'utilisateur.
        /// </summary>
        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }
        /// <summary>
        /// Get/Set modiffier ou obtenir le prénom de l'utilisateur.
        /// </summary>
        public string Prenom
        {
            get { return _prenom; }
            set { _prenom = value; }
        }
        /// <summary>
        /// Get/Set modiffier ou obtenir l'âge de l'utilisateur.
        /// </summary>
        public byte Age
        {
            get { return _age; }
            set { _age = value; }
        }
        /// <summary>
        /// Get/Set modiffier ou obtenir le genre de l'utilisateur. (Homme, Femme, Autre)
        /// </summary>
        public Sexe Genre
        {
            get { return _genre; }
            set { _genre = value; }
        }
        /// <summary>
        /// Get/Set modiffier ou obtenir le poids de l'utilisateur.
        /// </summary>
        public float Poids
        {
            get { return _poids; }
            set { _poids = value; }
        }
        /// <summary>
        /// Get/Set modiffier ou obtenir la grandeur de l'utilisateur.
        /// </summary>
        public float Grandeur
        {
            get { return _grandeur; }
            set { _grandeur = value; }
        }
        /// <summary>
        /// Get/Set modiffier ou obtenir la masse grasse de l'utilisateur.
        /// </summary>
        public float MasseGrasse
        {
            get { return _masseGrasse; }
            set { _masseGrasse = value; }
        }
        /// <summary>
        /// Get/Set modiffier ou obtenir la masse musculaire de l'utilisateur.
        /// </summary>
        public float MasseMusculaire
        {
            get { return _masseMusculaire; }
            set { _masseMusculaire = value; }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur, permet de créé un utilisateur, prend
        /// en paramètre le nom, prénom, age, genre, poids, grandeur, masse grasse, masse musculaire.
        /// </summary>
        /// <param name="pNom">String, nom de l'utilisateur.</param>
        /// <param name="pPrenom">String, prenom de l'utilisateur</param>
        /// <param name="pAge">Byte, age de l'utilisateur</param>
        /// <param name="pGenre">Sexe, genre de l'utilisateur(Homme, Femme, Autre)</param>
        /// <param name="pPoids">float, poids de l'utilisateur</param>
        /// <param name="pGrandeur">float, grandeur de l'utilisateur</param>
        /// <param name="pMasseGrasse">float, masse grasse de l'utilisateur. Pas oubligatoire
        /// si il n'est pas défini la valeur est de 0.</param>
        /// <param name="pMasseMusculaire">float, masse musculaire de l'utilisateur. Pas oubligatoire
        /// si il n'est pas défini la valeur est de 0.</param>
        public Utilisateur(string pNom, string pPrenom, byte pAge, Sexe pGenre, float pPoids, float pGrandeur, float pMasseGrasse, float pMasseMusculaire)
        {
            Nom = pNom;
            Prenom = pPrenom;
            Age = pAge;
            Genre = pGenre;
            Poids = pPoids;
            Grandeur = pGrandeur;
            MasseGrasse = pMasseGrasse;
            MasseMusculaire = pMasseMusculaire;
        }

        #endregion
    }
}