using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class StreamSpell : Spell
{
    [SerializeField] public float thrustAcceleration = 12f;

    private ParticleSystem[] particleSystems;
    private CharacterBody bodyReceivingRecoil;

    public override void Start()
    {
        base.Start();
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        //TODO: Find a way to inject recoil-receiving physics body (needs only implement AddForce)
        bodyReceivingRecoil = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBody>();
    }


    public override void Hold()
    {
        //Maintain Joint
        transform.position = target.position;
        transform.rotation = target.rotation;

        //Apply Recoil
        float height = transform.position.y;       
        Vector3 thrustVector = -transform.forward * thrustAcceleration;
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
        base.Release(velocity);
    }
}
