using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    protected SpellCaster caster;
    protected Transform target;    

    void FixedUpdate()
    {
        if (target != null)
        {
            Hold();
        }
    }

    public virtual void Begin(SpellCaster caster, Transform target)
    {        
        this.caster = caster;
        this.target = target;
    }

    public virtual void Hold()
    {
        if (fixToHand && target != null)
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }

    public virtual void Release(Vector3 velocity)
    {
        this.target = null;
        if (destroyTime >= 0f)
        {
            Destroy(this, destroyTime);
        }
    }

    public virtual void Punch(Vector3 velocity)
    {
        
    }

    public virtual void Pull(Vector3 velocity)
    {
        
    }
}