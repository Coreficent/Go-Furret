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

            _eulerRotation.x += _rotationSpeed;
        }

        protected void Update()
        {
            transform.eulerAngles += _eulerRotation * Time.deltaTime;
        }
    }
}
