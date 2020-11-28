using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    private float speed = 20f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal") * speed * Mathf.Deg2Rad;
        float y = Input.GetAxis("Vertical") * speed * Mathf.Deg2Rad;

        transform.Rotate(Vector3.up, -x);
        transform.Rotate(Vector3.right, y);
    }
}
