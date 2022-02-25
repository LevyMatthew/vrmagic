using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpell : Spell
{
    public float velocityMultiplier = 10f;
    public GameObject punchSpawnTemplate;
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
        rb.rotation = target.rotation;
        rb.position = target.position;
    }

    public override void Punch(Vector3 velocity)
    {
        Debug.Log(velocity.magnitude);
        base.Punch(velocity);
        if (punchSpawnTemplate != null)
        {
            GameObject punchInstance = GameObject.Instantiate(punchSpawnTemplate, transform.position, transform.rotation);
            Rigidbody punchRigidbody = punchInstance.GetComponent<Rigidbody>();
            punchRigidbody.velocity = velocity * velocityMultiplier;
            Destroy(punchInstance, 0.8f);
        }
    }    

    public override void Release(Vector3 velocity)
    {
        Destroy(gameObject);
    }
}
