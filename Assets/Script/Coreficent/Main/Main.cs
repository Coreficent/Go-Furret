namespace Coreficent.Main
{
    using UnityEngine;
    using Coreficent.Utility;

    public class Main : MonoBehaviour
    {
        public GameObject Furret;

        private void Start()
        {
            SanityCheck.Check(this, Furret);
        }
    }
}
