using UnityEngine;

public class RockPushSpell : Spell
{
    public float range = 10f;
    float radius = 3f;
    public float spawnRadius = 3f;
    public float pushForce = 500f;
    [SerializeField] public GameObject spawnUpRockTemplate;
    [SerializeField] public GameObject spawnDownRockTemplate;
    [SerializeField] public LayerMask spawnLayerMask;
    [SerializeField] public LayerMask pushLayerMask;

    public override void Begin(SpellCaster caster, Transform target)
    {
        base.Begin(caster, target);
    }   

    public void PunchUp(Vector3 velocity)
    {
        Vector3 spawnHoverPosition = transform.position + (caster.transform.forward + Vector3.up* 2f) * spawnRadius;

        RaycastHit hitInfo;
        if (Physics.Raycast(spawnHoverPosition, Vector3.down, out hitInfo, range, spawnLayerMask, QueryTriggerInteraction.Ignore))
        {
            Vector3 raycastHitPosition = spawnHoverPosition + Vector3.down * hitInfo.distance;
            Vector3 raycastHitNormal = hitInfo.normal;

            Quaternion raycastHitRotation = Quaternion.FromToRotation(Vector3.up, raycastHitNormal);
            if (spawnUpRockTemplate != null) {
              GameObject.Instantiate(spawnUpRockTemplate, raycastHitPosition, raycastHitRotation);
            }
        }
    }

    public void PunchOut(Vector3 velocity)
    {
        Vector3 origin = caster.transform.position;
        Vector3 direction = caster.transform.forward;
        //Collider[] colliders = Physics.OverlapSphere(origin, pushRadius, pushLayerMask, QueryTriggerInteraction.Ignore);
        RaycastHit[] hitInfos = Physics.SphereCastAll(origin, radius, direction, range, pushLayerMask, QueryTriggerInteraction.Ignore);
        foreach (RaycastHit rh in hitInfos)
        {   
            rh.rigidbody?.AddForce(pushForce * direction, ForceMode.Impulse);        
        }

    }

    public void PunchDown(Vector3 velocity)
    {
        Vector3 origin = caster.transform.position;
        Vector3 direction = caster.transform.forward;
        RaycastHit hitInfo;
        if (Physics.Raycast(origin, direction, out hitInfo, range, spawnLayerMask, QueryTriggerInteraction.Ignore))
        {
            Vector3 raycastHitPosition = origin + direction * hitInfo.distance;
            Vector3 raycastHitNormal = hitInfo.normal;

            Quaternion raycastHitRotation = Quaternion.FromToRotation(Vector3.up, raycastHitNormal);
            if (spawnDownRockTemplate != null) {
              GameObject.Instantiate(spawnDownRockTemplate, raycastHitPosition, raycastHitRotation);
            }
        }
    }

    public override void Punch(Vector3 velocity)
    {
        if (velocity.normalized.y > 0.8f)
        {
            PunchUp(velocity);
        }
        else if (velocity.y > -0.5)
        {
            PunchOut(velocity);
        }
        else
        {
            PunchDown(velocity);
        }
    }

}
