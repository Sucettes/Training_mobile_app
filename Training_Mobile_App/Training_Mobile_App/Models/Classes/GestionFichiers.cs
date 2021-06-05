using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Training_Mobile_App.Properties;
using String = System.String;
using Training_Mobile_App.Models.FctBiblio;
using System.IO;

namespace Training_Mobile_App.Models.Classes
{
    public class GestionFichiers
    {
        #region Attributs

        private const string DONNEES_UTILISATEURS = "donneesDUtilisateur.csv";
        private const string DONNEES_PROGRES_POIDS = "progresPoids.csv";

        /// <summary>
        /// Attributs de type string. Représente le fichier utilisateur dans les resources.
        /// </summary>
        private string _cheminRepertoire =  Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        
        /// <summary>
        /// Attributs de type string. Représente le fichier d'exercices dans les resources.
        /// </summary>
        private readonly string _fichierExercices = Resources.exercices;

        /// <summary>
        /// Attributs de type string[], représente les donner utilisateur en traitement.
        /// </summary>
        private string[] _donneesUtilisateurTraitement;

        /// <summary>
        /// Attributs de type string[]. Représente les exercices qui est en traitement.
        /// </summary>
        private string[] _donneesListeExercicesTraitement;

        /// <summary>
        /// Attributs de type string[]. Représente les donnees utilisateur en sortie.
        /// </summary>
        private string[] _donneesUtilisateurSortie;

        /// <summary>
        /// Attributs de type List<TupleEnListe>. Représente la progression des exercices.
        /// </summary>
        private List<TupleEnListe> _progressionExercices = new List<TupleEnListe>();
        /// <summary>
        /// Attributs de type string[] représente les donnesPoidsUtilisateur.
        /// </summary>
        private string[] _donnessPoidsUtilisateur;

        #endregion

        #region Get/Set
        /// <summary>
        /// Get/Set permet d'obtenir ou de modifier FichierUtilisateur.
        /// </summary>
        public string CheminRepertoire
        {
            get { return _cheminRepertoire; }
            set { _cheminRepertoire = value; }
        }
        /// <summary>
        /// Get/Set permet d'obtenir ou de modifier FichierExercices.
        /// </summary>
        public string FichierExercices
        {
            get { return _fichierExercices; }
        }
        /// <summary>
        /// Get/Set permet d'obtenir ou de modifier ProgressionExercices.
        /// </summary>
        public List<TupleEnListe> ProgressionExercices
        {
            get { return _progressionExercices; }
            set { _progressionExercices = value; }
        }
        /// <summary>
        /// Get/Set permet d'obtenir ou de modifier DonneesUtilisateurSortie.
        /// </summary>
        public string[] DonneesUtilisateurSortie
        {
            get { return _donneesUtilisateurSortie; }
            set { _donneesUtilisateurSortie = value; }
        }
        /// <summary>
        /// Get/Set permet d'obtenir ou de modifier DonneesListeExercicesTraitement
        /// </summary>
        public string[] DonneesListeExercicesTraitement
        {
            get { return _donneesListeExercicesTraitement; }
            set { _donneesListeExercicesTraitement = value; }
        }
        /// <summary>
        /// Get/Set permet d'obtenir ou de modifier DonneesUtilisateurTraitement
        /// </summary>
        public string[] DonneesUtilisateurTraitement
        {
            get { return _donneesUtilisateurTraitement; }
            set { _donneesUtilisateurTraitement = value; }
        }

        /// <summary>
        /// Get/Set Permet de modifier ou d'obtenir ProgressionPoidsUtilisateur
        /// </summary>
        public string[] ProgressionPoidsUtilisateur
        {
            get { return _donnessPoidsUtilisateur; }
            set { _donnessPoidsUtilisateur = value; }
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Méthode qui permet de charger les fichiers.
        /// </summary>
        public void ChargerFichier()
        {
            string fichierUtilisateur = Path.Combine(CheminRepertoire, DONNEES_UTILISATEURS);
            string fichierProgresPoids = Path.Combine(CheminRepertoire, DONNEES_PROGRES_POIDS);

            if (!File.Exists(fichierUtilisateur))
            {
                StreamWriter sw = new StreamWriter(fichierUtilisateur);
                sw.Close();
            }

            if (!File.Exists(fichierProgresPoids))
            {
                StreamWriter sw = new StreamWriter(fichierProgresPoids);
                sw.Close();
            }

            StreamReader srUtilisateur = new StreamReader(fichierUtilisateur);
            string fichierUtilisateurLu = srUtilisateur.ReadToEnd();
            bool validation = ArrayNonVide(fichierUtilisateurLu);

            if (validation)
            {
                fichierUtilisateurLu.Replace("\r", "\n");
                DonneesUtilisateurTraitement = Regex.Split(fichierUtilisateurLu, "\n");
                DonneesUtilisateurSortie = DonneesUtilisateurTraitement[0].Trim().Split(';');
                PreparationProgressionUtilisateur();
            }

            srUtilisateur.Close();

            StreamReader srPoids = new StreamReader(fichierProgresPoids);
            string fichierPoidsLu = srPoids.ReadToEnd();

            validation = ArrayNonVide(fichierPoidsLu);

            if (validation)
            {
                fichierPoidsLu.Replace("\r", "\n");
                ProgressionPoidsUtilisateur = Regex.Split(fichierPoidsLu, "\n");
            }

            srPoids.Close();

        }

        /// <summary>
        /// Transforme les données de progression en liste pour facilité leurs utilisation
        /// dans l'application
        /// </summary>
        public void PreparationProgressionUtilisateur()
        {
            int i = 1;

            string[] tempArray;

            while (i < DonneesUtilisateurTraitement.Length)
            {

                if (DonneesUtilisateurTraitement[i] == "") 
                    break;

                tempArray = DonneesUtilisateurTraitement[i].Trim().Split(';');
                PartieDuCorpsTravailler enumParsing = (PartieDuCorpsTravailler)Enum.Parse(typeof(PartieDuCorpsTravailler), tempArray[1]);
                Exercice tExercice = new Exercice(tempArray[0], enumParsing, "vide");

                byte nbRep = byte.Parse(tempArray[3].Trim());
                byte nbSeries = byte.Parse(tempArray[4].Trim());

                ProgressionUtilisateur progUser = new ProgressionUtilisateur(tExercice, float.Parse(tempArray[2]),
                   nbRep, nbSeries);

                DateTime dateDeLexercice = DateTime.Parse(tempArray[5]);

                ProgressionExercices.Add(new TupleEnListe(progUser, dateDeLexercice));

                i++;
            }

        }

        /// <summary>
        /// Charge le fichier de la liste d'exercice et rempli la liste dans gestionExercice.
        /// </summary>
        public void PreparationListeExercicesAffichage(GestionExercice gestionExercice)
        {
            int i = 0;
            string[] tempArray;

            DonneesListeExercicesTraitement = Regex.Split(FichierExercices, "\n");

            while (i < DonneesListeExercicesTraitement.Length)
            {
                tempArray = DonneesListeExercicesTraitement[i].Trim().Split(';');

                List<Exercice> lstExercices = gestionExercice.LstExercices;
                lstExercices.Add(new Exercice(tempArray[0],
                            (PartieDuCorpsTravailler)Enum.Parse(typeof(PartieDuCorpsTravailler), tempArray[1]),
                            tempArray[2]));
                gestionExercice.LstExercices = lstExercices;
                i++;
            }
        }

        /// <summary>
        /// Création d'un nouvel utilisateur au fichier des données utilisateurs.
        /// </summary>
        /// <param name="utilisateur"></param>
        public void AjoutNouvelUtilisateur(Utilisateur unUtilisateur)
        {
            string fichierUtilisateur = Path.Combine(CheminRepertoire, "donneesDUtilisateur.csv");
            StreamWriter sw = new StreamWriter(fichierUtilisateur);

            string preparationEnregistrement = 
                $"{unUtilisateur.Nom};" + 
                $"{unUtilisateur.Prenom};" + 
                $"{unUtilisateur.Age};" +
                $"{unUtilisateur.Genre};" +
                $"{unUtilisateur.Poids};" +
                $"{unUtilisateur.Grandeur};" +
                $"{unUtilisateur.MasseGrasse};" +
                $"{unUtilisateur.MasseMusculaire};";

            sw.WriteLine(preparationEnregistrement);
            sw.Close();

        }

        /// <summary>
        /// Enregistre les exercices de la journée en cours.
        /// </summary>
        /// <param name="progExercice"></param>
        public void EnregistrementProgressionExercice(TupleEnListe progExercice)
        {
            string fichierUtilisateur = Path.Combine(CheminRepertoire, "donneesDUtilisateur.csv");
            StreamWriter sw = new StreamWriter(fichierUtilisateur, append: true);

            DateTime aujourdhui = DateTime.Today;

            string preparationEnregistrement =
                $"{progExercice.ProgUser.InfoExercice.NomExercice}; " +
                $"{progExercice.ProgUser.InfoExercice.PartieDuCorpsTravailler}; " +
                $"{progExercice.ProgUser.Poids}; " +
                $"{progExercice.ProgUser.NbReps}; " +
                $"{progExercice.ProgUser.NbSeries}; " +
                $"{aujourdhui};";
                

            sw.WriteLine(preparationEnregistrement);
            sw.Close();

        }

        /// <summary>
        /// Sauvegarde le progrès de poids dans le fichier progrèsPoids à
        /// partir des informations utilisateurs fournit lors de la modification
        /// d'une donnée utilisateurs.
        /// </summary>
        /// <param name="utilisateur"></param>
        public void EnregistrerProgres(string poids, string masseMuscu, string massGrasse)
        {
            string fichierPoids = Path.Combine(CheminRepertoire, "progresPoids.csv");
            StreamWriter sw = new StreamWriter(fichierPoids, append: true);

            string preparationEnregistrement =
                $"{poids}; " +
                $"{masseMuscu}; " +
                $"{massGrasse};";

            sw.WriteLine(preparationEnregistrement);
            sw.Close();

        }

        /// <summary>
        /// Efface l'utilisateur et ses progressions. Les fichiers sont réinitialisés
        /// à zéro pour créer un nouvel utilisateur.
        /// </summary>
        public void ReinitialisationUtilisateur()
        {
            string fichierUtilisateur = Path.Combine(CheminRepertoire, DONNEES_UTILISATEURS);
            string fichierProgresPoids = Path.Combine(CheminRepertoire, DONNEES_PROGRES_POIDS);

            FileStream fs;
            Byte[] fichierVide = {};

            if(fichierUtilisateur.Length > 0)
            {
                fs = File.Open(fichierUtilisateur, FileMode.Create);
                fs.Write(fichierVide, 0 ,0);
                fs.Close();
            }

            if (fichierUtilisateur.Length > 0)
            {
                fs = File.Open(fichierProgresPoids, FileMode.Create);
                fs.Write(fichierVide, 0, 0);
                fs.Close();
            }
        }

        /// <summary>
        /// Vérifie si le fichier DonneesUtilisateurTraitement est vide.
        /// </summary>
        /// <param name="arrayAVerifier"></param>
        /// <returns>Si vide = false, donc doit créer un utilisateur, sinon charger
        /// l'utilisateur existant.</returns>
        public bool ArrayNonVide(string pFichierLu)
        {
            if (String.IsNullOrEmpty(pFichierLu))
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}