using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitationSpell : Spell
{
    private Rigidbody influencedRigidbody;
    private Rigidbody controlTarget;
    private SpringJoint springJoint;


    // Update is called once per frame
    public override void Begin(SpellCaster caster, Transform target)
    {
        base.Begin(caster, target);
        Transform influencedTransform = caster.GetPreviewTarget();

        if (influencedTransform == null)
        {
            Destroy(this);
            return;
        }
        influencedRigidbody = influencedTransform.GetComponentInChildren<Rigidbody>();
        controlTarget = GetComponentInChildren<Rigidbody>();
        controlTarget.transform.position = influencedRigidbody.transform.position;
        springJoint = controlTarget.GetComponent<SpringJoint>();
        springJoint.connectedBody = influencedRigidbody;
    }

    public override void Hold()
    {
        base.Hold();        
    }

    public override void Release(Vector3 velocity)
    {
        base.Release(velocity);
        if (springJoint != null)
        springJoint.connectedBody = null;
    }
}
