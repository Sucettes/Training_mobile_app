using System.IO;
using Plugin.SimpleAudioPlayer;
using Xamarin.Essentials;
using Training_Mobile_App.Properties;

namespace Training_Mobile_App.Models.FctBiblio
{
    public class AudioBouton
    {
        #region Attributs

        private string _streamAudio = Path.Combine(FileSystem.CacheDirectory, "bd1.wav");
        private ISimpleAudioPlayer _player;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur de la class audioBouton
        /// </summary>
        public AudioBouton()
        {
            JouerSon();
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Permet de jouer du son
        /// </summary>
        public void JouerSon()
        {
            try
            {
                Stream stream = Resources.ResourceManager.GetStream("bd1");
                _player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                _player.Load(stream);
                _player.Volume = 3.0;
                _player.Play();
            }
            catch { }
        }

        #endregion
    }
}