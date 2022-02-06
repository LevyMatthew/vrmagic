using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;

public class BallSpell : Spell
{
    public float velocityMultiplier = 10f;
    protected Rigidbody rb;

    public override void Awake()
    {
        base.Awake();
        this.rb = GetComponent<Rigidbody>();
    }

    public override void Grab(Transform target)
    {
        this.target = target;
        rb.useGravity = false;
        rb.isKinematic = true;
        Hold();
    }

    public override void Hold()
    {
        rb.rotation = target.rotation;
        rb.position = target.position;
    }

    public override void Release(Vector3 velocity)
    {
        destroyTime = 15f;
        base.Release(velocity);
        rb.velocity = velocity;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.velocity *= velocityMultiplier;
    }
}
