namespace Coreficent.Main
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Coreficent.Utility;

    public class Main : MonoBehaviour
    {
        public GameObject Furret;
        private void Start()
        {
            SanityCheck.Check(this, Furret);
            Furret.GetComponent<Animator>().SetBool("Started", true);
        }
        private void Update()
        {

        }
    }
}