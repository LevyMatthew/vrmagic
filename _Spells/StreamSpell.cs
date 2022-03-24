using UnityEngine;

public class StreamSpell : Spell
{
    [SerializeField] public float thrustAcceleration = 12f;    
    [SerializeField] public float minTakeoffFactor = 0.7f;

    public ParticleSystem streamParticleSystem;
    public ParticleSystem indicatorParticleSystem;
    public Quaternion rotationOffset;

    private CharacterBody bodyReceivingRecoil;
    private bool autoplay = false;
    private bool thrusting;

    public override void Begin(SpellCaster caster, Transform target)
    {
        base.Begin(caster, target); 
        bodyReceivingRecoil = caster.recoilRecipient;
        indicatorParticleSystem.Play();
        streamParticleSystem.Stop();

        if (autoplay)
        {
            thrusting = true;
            streamParticleSystem.Play();
            indicatorParticleSystem.Stop();
        }
    }


    public override void Hold()
    {
        base.Hold();

        transform.Rotate(rotationOffset.eulerAngles);

        if (thrusting) {
            //Apply Recoil
            float height = transform.position.y;
            Vector3 thrustVector = -transform.forward * thrustAcceleration;           
            bodyReceivingRecoil?.AddForce(thrustVector, ForceMode.Acceleration);        
        }       
    }

    public void Activate()
    {
        thrusting = true;
        streamParticleSystem.Play();
        indicatorParticleSystem.Stop();
    }

    public void Deactivate()
    {
        thrusting = false;
        streamParticleSystem.Stop();
        indicatorParticleSystem.Play();
    }

    public override void Punch(Vector3 velocity)
    {
        Activate();
    }

    public override void Pull(Vector3 velocity)
    {
        Deactivate();
    }

    public override void Release(Vector3 velocity)
    {
        Deactivate();
        target = null;
        Object.Destroy(gameObject, destroyTime);
    }
}
