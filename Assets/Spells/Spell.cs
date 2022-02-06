using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;

public class Spell : MonoBehaviour
{
    [SerializeField] public List<Element> elements;
    [SerializeField] public float destroyTime = 15f;

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

    public virtual void Grab(Transform target)
    {
        this.target = target;
        transform.position = target.position;
        transform.rotation = target.rotation;
    }

    public virtual void Hold()
    {
    }

    public virtual void Release(Vector3 velocity)
    {
        target = null;
        Object.Destroy(gameObject, destroyTime);
    }

}
