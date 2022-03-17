using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamSpell : Spell
{
    [SerializeField] public float thrustAcceleration = 12f;    
    [SerializeField] public float minTakeoffPercent = 0.7f;

    public ParticleSystem streamParticleSystem;
    public ParticleSystem indicatorParticleSystem;
    public Quaternion rotationOffset;

    private CharacterBody bodyReceivingRecoil;
    private bool punching;

    public override void Begin(SpellCaster caster, Transform target)
    {
        base.Begin(caster, target); 
      //  bodyReceivingRecoil = Player.instance.GetComponent<CharacterBody>();
        indicatorParticleSystem.Play();
        streamParticleSystem.Stop();
    }


    public override void Hold()
    {
        base.Hold();

        transform.Rotate(45f, 0, 0);

        if (punching) {
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
    }

    public override void Punch(Vector3 velocity)
    {
        punching = true;
        streamParticleSystem.Play();
        indicatorParticleSystem.Stop();
    }

    public override void Pull(Vector3 velocity)
    {
        punching = false;
        streamParticleSystem.Stop();
        indicatorParticleSystem.Play();
    }

    public override void Release(Vector3 velocity)
    {
        punching = false;
        streamParticleSystem.Stop();
        indicatorParticleSystem.Stop();
        target = null;
        Object.Destroy(gameObject, destroyTime);
    }
}
