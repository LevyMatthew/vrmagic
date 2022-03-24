using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class SpellInteractable : MonoBehaviour
{
    [SerializeField] public bool isFlammable = false;
    [SerializeField] public bool isLevitatable = false;
    [SerializeField] public bool isBreakable = false; //TODO: Implement
    [SerializeField] public bool isOnFire = false;
    [SerializeField] public float temperature = 25f;

    public ParticleSystem onFireParticles;

    private Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        Spell spell = collision.gameObject.GetComponentInParent<Spell>();
        if (spell != null)
        {
            Debug.Log(spell.elements);
            if (spell.elements != null)
                DoElementInteraction(spell);
        }
    }

    public void OnParticleCollision(GameObject other)
    {
        Spell spell = other.GetComponentInParent<Spell>();
        if (spell != null)
        {
            if (spell.elements != null)
                DoElementInteraction(spell);
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
        rb.AddForce(force, forceMode);
    }

    public void ApplyForceAtPosition(Vector3 force, Vector3 position, ForceMode forceMode = ForceMode.Force)
    {
        rb.AddForceAtPosition(force, position, forceMode);
    }

    public void Ignite()
    {
        isOnFire = true;
        onFireParticles.gameObject.SetActive(true);
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
