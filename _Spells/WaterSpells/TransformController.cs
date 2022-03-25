using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformController : MonoBehaviour
{
	public float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = speed * Input.GetAxis("Mouse X");
        float v = speed * -Input.GetAxis("Mouse Y");

        transform.Rotate(0, h, 0);
    }
}
