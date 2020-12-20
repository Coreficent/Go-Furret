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
            Vector3 unitVector = new Vector3(Input.GetAxis(_horizontalControl), Input.GetAxis(_verticalControl)).normalized;
            transform.rotation *= Quaternion.AngleAxis(_rotationSpeed * unitVector.x * Time.deltaTime, transform.InverseTransformDirection(-Vector3.up));
            transform.rotation *= Quaternion.AngleAxis(_rotationSpeed * unitVector.y * Time.deltaTime, transform.InverseTransformDirection(-Vector3.left));
        }
    }
}
