using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;

public class Spell : MonoBehaviour
{
    [SerializeField] public List<Element> elements;
    [SerializeField] public float destroyTime = 0f;
    [SerializeField] public Vector3 force = Vector3.zero;
    [SerializeField] public ForceMode forceMode = ForceMode.Force;

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
        if (fixToHand)
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }

    public virtual void Release(Vector3 velocity)
    {
        this.target = null;
    }

    public virtual void Punch(Vector3 velocity)
    {
        
    }
}