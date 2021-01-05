namespace Coreficent.Food
{
    using Coreficent.Utility;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Cooker : MonoBehaviour
    {
        public List<GameObject> FruitVacuum = new List<GameObject>();
        private int _lastVacuumSize = 0;


        // Start is called before the first frame update
        protected void Start()
        {

        }

        // Update is called once per frame
        protected void Update()
        {
            foreach (GameObject fruit in FruitVacuum)
            {
                float maximumHeightScaler = 1.0f;
                float heightOffsetScaler = 0.5f;

                float distance = Vector2.SqrMagnitude(new Vector2(transform.position.x - fruit.transform.position.x, transform.position.z - fruit.transform.position.z));
                Vector3 destination = transform.position + transform.up * distance * maximumHeightScaler + transform.up * heightOffsetScaler;

                fruit.transform.position = Vector3.MoveTowards(fruit.transform.position, destination, 1.0f * Time.deltaTime);
            }
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Fruit>())
            {
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                other.gameObject.GetComponent<Collider>().enabled = false;

                FruitVacuum.Add(other.gameObject);
            }
        }

        protected void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<Fruit>())
            {
                FruitVacuum.Remove(other.gameObject);
            }
        }

        public bool FruitVacuumSizeChanged()
        {
            if (_lastVacuumSize == FruitVacuum.Count)
            {
                return false;
            }
            else
            {
                _lastVacuumSize = FruitVacuum.Count;
                return true;
            }
        }
    }
}
