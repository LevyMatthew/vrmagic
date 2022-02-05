using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Spell
{
    public override void Release(Vector3 velocity)
    {
        destroyTime = 15f;
        base.Release(velocity);
        rb.velocity = velocity;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.velocity *= 10f;
    }
}
