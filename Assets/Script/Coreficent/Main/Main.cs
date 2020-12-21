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
            Furret.transform.Find("Display").transform.Find("model").GetComponent<SkinnedMeshRenderer>().materials[2].SetFloat("_ExpressionIndex", 1.0f);
        }
        private void Update()
        {
            Furret.transform.Find("Display").GetComponent<Animator>().SetBool("Started", Input.GetKey(KeyCode.Alpha1));

            /*
                check for user input
                run AI
                move enemies
                resolve collisions
                draw graphics
                play sounds
             */
        }
    }
}