namespace Coreficent.Transform
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Orbit : MonoBehaviour
    {
        private float _displacement = 5.0f;

        void Update()
        {
            PositionDestination();
        }

        private void PositionDestination()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            float distanceScale = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
            if (!(Mathf.Abs(x) < 0.001f && Mathf.Abs(y) < 0.001f))
            {
                float angle = Mathf.Atan2(y, x);
                transform.position = new Vector3(Mathf.Cos(angle) * _displacement * distanceScale, Mathf.Sin(angle) * _displacement * distanceScale, 0.0f);
            }
        }
    }
}
