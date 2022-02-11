using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class StreamSpell : Spell
{
    [SerializeField] public float thrustAcceleration = 12f;    
    [SerializeField] public float minTakeoffPercent = 0.7f;

    private ParticleSystem[] particleSystems;
    private CharacterBody bodyReceivingRecoil;

    public override void Begin(SpellCaster caster, Transform target)
    {
        base.Begin(caster, target);
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        bodyReceivingRecoil = Player.instance.gameObject.GetComponent<CharacterBody>();
    }


    public override void Hold()
    {
        //Maintain Joint
        transform.position = target.position;
        transform.rotation = target.rotation;

        //Apply Recoil
        float height = transform.position.y;
        Vector3 thrustVector = -transform.forward * thrustAcceleration;

        if (bodyReceivingRecoil.grounded && bodyReceivingRecoil.stickyFeet)
        {
            if (thrustVector.y < thrustAcceleration * minTakeoffPercent)
                thrustVector = Vector3.zero;
            else
                bodyReceivingRecoil.AddForce(Vector3.up * 1f, ForceMode.VelocityChange);
        }

        //float pitch = 
        bodyReceivingRecoil.AddForce(thrustVector, ForceMode.Acceleration);        
    }

    public override void Release(Vector3 velocity)
    {
        if (particleSystems != null)
        {
            foreach (ParticleSystem ps in particleSystems)
            {
                ps.Stop();
            }
        }
        target = null;
        Object.Destroy(gameObject, destroyTime);
    }
}
