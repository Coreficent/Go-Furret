namespace Coreficent.Main
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Coreficent.Utility;
    using Coreficent.Control;

    public class Main : MonoBehaviour
    {
        public GameObject Furret;

        private FurretControl _furretControl;
        private void Start()
        {
            SanityCheck.Check(this, Furret);
            _furretControl = new FurretControl(Furret.GetComponent<Animator>());
        }
        private void Update()
        {
            /*
                check for user input
                run AI
                move enemies
                resolve collisions
                draw graphics
                play sounds
             */
            _furretControl.Run();
        }
    }
}