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
            //Furret.transform.Find("Display").transform.Find("model").GetComponent<SkinnedMeshRenderer>().sharedMaterials[2].SetFloat("_ExpressionX", 1.0f);
            //Furret.transform.Find("Display").transform.Find("model").GetComponent<SkinnedMeshRenderer>().sharedMaterials[2].SetFloat("_ExpressionY", 2.0f);

            //Furret.transform.Find("Display").transform.Find("model").GetComponent<SkinnedMeshRenderer>().sharedMaterials[1].SetFloat("_ExpressionX", 1.0f);
            //Furret.transform.Find("Display").transform.Find("model").GetComponent<SkinnedMeshRenderer>().sharedMaterials[1].SetFloat("_ExpressionY", 1.0f);
        }
        private void Update()
        {
            // Furret.transform.Find("FurretDisplay").GetComponent<Animator>().SetBool("Started", Input.GetKey(KeyCode.Alpha1));

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