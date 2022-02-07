using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class SpellInteractable : MonoBehaviour
{
    [SerializeField] public bool isFlammable = false;
    [SerializeField] public bool isOnFire = false;
    [SerializeField] public float temperature = 25f;

    private ParticleSystem onFireParticles;

    private Rigidbody rigidbody;    

    public void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        onFireParticles = GetComponentInChildren<ParticleSystem>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        Spell spell = collision.gameObject.GetComponentInParent<Spell>();
        if (spell != null)
        {
            Debug.Log(spell.elements);
            if (spell.elements != null)
                DoElementInteraction(spell);
            if (spell.force != null)
                ApplyForce(spell.force, spell.forceMode);
        }
    }

    public void OnParticleCollision(GameObject other)
    {
        Debug.Log(other);
        Spell spell = other.GetComponentInParent<Spell>();
        if (spell != null)
        {
            Debug.Log(spell.elements);
            if (spell.elements != null)
                DoElementInteraction(spell);
            if (spell.force != null)
                ApplyForce(spell.force, spell.forceMode);
        }
    }

    public void DoElementInteraction(Spell spell)
    {
        foreach (Spell.Element element in spell.elements) {
            if (element == Spell.Element.Fire && isFlammable)
            {
                Ignite();
            }
            else if (element == Spell.Element.Water)
            {
                Hydrate();
            }
        }        
    }

    public void ApplyForce(Vector3 force, ForceMode forceMode = ForceMode.Force)
    {
        rigidbody.AddForce(force, forceMode);
    }

    public void ApplyForceAtPosition(Vector3 force, Vector3 position, ForceMode forceMode = ForceMode.Force)
    {
        rigidbody.AddForceAtPosition(force, position, forceMode);
    }

    public void Ignite()
    {
        isOnFire = true;
        onFireParticles.Play();
    }

    public void Hydrate()
    {
        if (isOnFire)
        {
            Quench();
        }
    }

    public void Quench()
    {
        isOnFire = false;
        onFireParticles.Stop();
    }
}
