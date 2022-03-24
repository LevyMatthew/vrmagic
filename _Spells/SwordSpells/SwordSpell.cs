using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSpell : Spell
{
    public bool isThrowable = false;
    public Joint holdJoint;

    public override void Begin(SpellCaster caster, Transform target)
    {
        RigidbodyGrab(target.GetComponent<Rigidbody>());
    }

    private void RigidbodyGrab(Rigidbody rigidbody)
    {
        SpringJoint springJoint = gameObject.AddComponent<SpringJoint>();
        holdJoint = springJoint;
        springJoint.spring = 100f;
        springJoint.damper = 2f;
        holdJoint.connectedBody = rigidbody;
    }

    public override void Release(Vector3 velocity)
    {
        base.Release(velocity);
        if (isThrowable)
            holdJoint.connectedBody = null;
    }
}
