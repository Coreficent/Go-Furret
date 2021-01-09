namespace Coreficent.Audio
{
    using UnityEngine;

    public class BackgroundMusic : MonoBehaviour
    {
        public AudioSource Music;

        protected void Start()
        {
            Music.Play();
        }
    }
}
