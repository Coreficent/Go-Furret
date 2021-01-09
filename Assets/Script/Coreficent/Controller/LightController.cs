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
            _eulerRotation = new Vector3(_rotationSpeed, _rotationSpeed, _rotationSpeed);
            SanityCheck.Check(this, _rotationSpeed > 0.0f);
        }

        protected void Update()
        {
            transform.Rotate(_eulerRotation * Time.deltaTime);
        }
    }
}
