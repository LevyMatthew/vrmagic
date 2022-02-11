using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PulseSpell : Spell
{
    [SerializeField] public float deltaVelocity = 3f;
    private CharacterBody bodyReceivingRecoil;

    public override void Begin(SpellCaster caster, Transform target)
    {
        base.Begin(caster, target);
        bodyReceivingRecoil = Player.instance.GetComponent<CharacterBody>();
        Hold();
        target = null;
    }


    public override void Hold()
    {
        base.Hold();
        //Maintain Joint
        transform.position = target.position;
        transform.rotation = target.rotation;
        //Apply Recoil
        Vector3 thrustVector = -transform.forward * deltaVelocity;
        bodyReceivingRecoil.AddForce(thrustVector, ForceMode.VelocityChange);
    }
}
