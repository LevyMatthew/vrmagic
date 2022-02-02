using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Rigidbody target;
    private float k = 4f;
    private float c;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 targetDisplacement = target.position - transform.position;
            Vector3 springForce = k * targetDisplacement;
            rb.position = target.position;
        }
    }

    public void Grab(Rigidbody target)
    {
        this.target = target;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        rb.isKinematic = true;
        rb.position = target.position;
    }

    public void Throw(Vector3 velocity)
    {
        target = null;
        rb.velocity = velocity;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = true;
        rb.isKinematic = false;
        Object.Destroy(this, 15.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
