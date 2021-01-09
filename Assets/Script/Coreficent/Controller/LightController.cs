namespace Coreficent.Controller
{
    using Coreficent.Utility;
    using UnityEngine;

    public class LightController : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed;

        private Vector3 _eulerRotation = Vector3.zero;
        protected void Start()
        {
            SanityCheck.Check(this, _rotationSpeed);
        }

        protected void Update()
        {
            transform.Rotate(_eulerRotation * Time.deltaTime);
        }
    }
}
