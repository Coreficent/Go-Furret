namespace Coreficent.Audio
{
    using Coreficent.Utility;
    using UnityEngine;

    public class BackgroundMusic : MonoBehaviour
    {
        private static BackgroundMusic Singleton = null;

        public AudioSource Music;

        protected void Start()
        {
            if (!Singleton)
            {
                SanityCheck.Check(this, Music);

                Singleton = this;
                DontDestroyOnLoad(gameObject);
                Music.Play();

                return;
            }
            else if (Singleton == this)
            {
                return;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }
    }
}
