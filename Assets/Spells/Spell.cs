using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;

public class Spell : MonoBehaviour
{
    [SerializeField] public List<Element> elements;
    [SerializeField] public float destroyTime = 15f;
    [SerializeField] public Vector3 force = Vector3.zero;
    [SerializeField] public ForceMode forceMode = ForceMode.Force;

    public enum Element
    {
        Water,
        Fire,
        Air,
        Earth
    }

    protected Transform target;

    public virtual void Start()
    {
    }

    public virtual void Awake()
    {
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Hold();
        }
    }

    //Use Hand.AttachObject when possible. Grab is for simply targeting abritrary transforms
    public virtual void Grab(Transform target)
    {
        this.target = target;
    }

    public virtual void Hold()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }

    public virtual void Release(Vector3 velocity)
    {
        this.target = null;
    }

}
