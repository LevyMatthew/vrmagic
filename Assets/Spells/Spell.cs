using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] public List<Element> elements;
    [SerializeField] public float destroyTime = 15f;

    protected Transform target;
    protected Rigidbody rb;

    public virtual void Start()
    {

    }

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

    public virtual void Grab(Transform origin)
    {
        this.target = origin;
        rb.useGravity = false;
        rb.isKinematic = true;
        Hold();
    }

    public virtual void Hold()
    {
        rb.rotation = target.rotation;
        rb.position = target.position;
    }

    public virtual void Release(Vector3 velocity)
    {
        target = null;
        Object.Destroy(gameObject, destroyTime);
    }

}
