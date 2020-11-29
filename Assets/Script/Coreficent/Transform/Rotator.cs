namespace Coreficent.Transform
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Rotator : MonoBehaviour
    {
        private readonly float _rotationSpeed = 45.0f;
        private readonly string _verticalControl = "Vertical";
        private readonly string _horizontalControl = "Horizontal";

        void Update()
        {
            ControlRotation();
        }

        private void ControlRotation()
        {
            transform.rotation *= Quaternion.AngleAxis(_rotationSpeed * Input.GetAxis(_horizontalControl) * Time.deltaTime, transform.InverseTransformDirection(-Vector3.up));
            transform.rotation *= Quaternion.AngleAxis(_rotationSpeed * Input.GetAxis(_verticalControl) * Time.deltaTime, transform.InverseTransformDirection(-Vector3.left));
        }
    }
}
