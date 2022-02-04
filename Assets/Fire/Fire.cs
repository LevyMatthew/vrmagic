using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{    
    [SerializeField] public float destroyTime = 15f;

    protected Rigidbody target;
    protected Rigidbody rb;

    public virtual void Start()
    {

    }

    // Start is called before the first frame update
    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Hold();
        }
    }

    public virtual void Spawn(Rigidbody origin)
    {
        this.target = origin;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        rb.isKinematic = true;
        rb.position = target.position;
    }

    public virtual void Hold()
    {
        rb.rotation = target.rotation;
    }

    public virtual void Release(Vector3 velocity)
    {
        target = null;
        Object.Destroy(gameObject, destroyTime);
    }
}
