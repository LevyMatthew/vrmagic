using UnityEngine;

[RequireComponent(typeof(KinematicTracker))]
public class FireSpell : Spell
{
    public float velocityMultiplier = 10f;
    public GameObject punchSpawnTemplate;
    public KinematicTracker kinematicTracker;
    protected Rigidbody rb;

    public override void Begin(SpellCaster caster, Transform target)
    {
        base.Begin(caster, target);        
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        Hold();
    }

    public override void Hold()
    {
        Debug.Log(kinematicTracker.velocity);
        if (kinematicTracker.velocity.magnitude > 1f)
        {
            Debug.Log("Spell Said punch");
            TryDirectionalSpawn(kinematicTracker.velocity);
        }
    }

    public override void Punch(Vector3 velocity)
    {
        base.Punch(velocity);
        TryDirectionalSpawn(velocity);
    }    

    public override void Release(Vector3 velocity)
    {
        Destroy(gameObject);
    }

    public void TryDirectionalSpawn(Vector3 velocity)
    {
        if (punchSpawnTemplate != null)
        {
            GameObject punchInstance = GameObject.Instantiate(punchSpawnTemplate, transform.position, transform.rotation);
            Rigidbody punchRigidbody = punchInstance.GetComponent<Rigidbody>();
            punchRigidbody.velocity = velocity * velocityMultiplier;
            Destroy(punchInstance, 0.8f);
        }

    }
}
