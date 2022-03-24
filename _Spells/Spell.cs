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
    public bool destroyOnRelease = true;

    protected SpellCaster caster;
    protected Transform target;    

    private KinematicTracker kinematicTracker;

    void FixedUpdate()
    {
        if (target != null)
        {
            Hold();
        }
    }

    public void OnAwake()
    {

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
        target = null;
        if (destroyOnRelease)
        {
            Destroy(gameObject, destroyTime);
        }
    }

    public virtual void Punch(Vector3 velocity)
    {
        Debug.Log("Spell: Punch Event Received");
    }

    public virtual void PunchUp()
    {
         Debug.Log("Spell: PunchUp Event Received");
    }

    public virtual void Pull(Vector3 velocity)
    {
        Debug.Log("Spell: Pull Event Received");
    }
}