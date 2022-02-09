using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;

public class Spell : MonoBehaviour
{
    [SerializeField] public List<Element> elements;
    [SerializeField] public float destroyTime = 0f;

    public enum Element
    {
        Water,
        Fire,
        Air,
        Earth
    }

    public bool fixToHand = true;
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

    public virtual void Begin(Transform casterTransform)
    {        
        this.target = casterTransform;
        transform.position = casterTransform.position;
        transform.rotation = casterTransform.rotation;
    }

    public virtual void Hold()
    {
        if (fixToHand)
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }

    public virtual void Release(Vector3 velocity)
    {
        target = null;
        Object.Destroy(gameObject, destroyTime);
    }

}
