using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameStream : Fire
{
    ParticleSystem[] particleSystems;

    public override void Start()
    {
        base.Start();
        particleSystems = GetComponentsInChildren<ParticleSystem>();
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
