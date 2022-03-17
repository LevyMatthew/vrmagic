using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookSpell : Spell
{
    [SerializeField] public float thrustAcceleration = 12f;    
    [SerializeField] public float minTakeoffPercent = 0.7f;

    private ParticleSystem[] particleSystems;
    private CharacterBody bodyReceivingRecoil;

    public override void Begin(SpellCaster caster, Transform target)
    {
        base.Begin(caster, target);
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        //bodyReceivingRecoil = caster.body.gameObject.GetComponent<CharacterBody>();
    }

	public override void Pull(Vector3 velocity)
    {
        Vector3 thrustVector = -velocity * thrustAcceleration;
        //bodyReceivingRecoil.AddForce(thrustVector, ForceMode.Impulse);
    }
}
