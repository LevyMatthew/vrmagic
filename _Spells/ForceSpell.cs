using UnityEngine;

public class ForceSpell : Spell
{
    [SerializeField] public Vector3 force = Vector3.zero;
    [SerializeField] public ForceMode forceMode = ForceMode.Force;
    [SerializeField] public float deltaVelocity = 3f;
    private CharacterBody bodyReceivingRecoil;

    public override void Begin(SpellCaster caster, Transform target)
    {
        base.Begin(caster, target);
        //bodyReceivingRecoil = caster.body.GetComponent<CharacterBody>();
        Hold();
        target = null;
    }


    public override void Hold()
    {
        base.Hold();

        //Apply Recoil
        Vector3 thrustVector = -transform.forward * deltaVelocity;
        bodyReceivingRecoil.AddForce(thrustVector, ForceMode.VelocityChange);
    }
}
