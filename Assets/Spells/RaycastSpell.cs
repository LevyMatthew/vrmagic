using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSpell : Spell
{

    public float range = 10f;
    [SerializeField] public GameObject spawnPrefabTemplate;
    [SerializeField] public LayerMask layerMask;
    [SerializeField] public QueryTriggerInteraction triggerInteraction; 

    public override void Begin(SpellCaster caster, Transform target)
    {
        base.Begin(caster, target);
        bool hit;
        Vector3 direction = caster.transform.forward;
        Vector3 origin = caster.transform.position;
        RaycastHit hitInfo;
        Debug.Log("From " + origin + " direction " + direction);
        hit = Physics.Raycast(origin, direction, out hitInfo, range, layerMask, triggerInteraction);
        if (hit)
        {
            Debug.Log("Hit!");
            Vector3 raycastHitPosition = origin + direction * hitInfo.distance;
            Vector3 raycastHitNormal = hitInfo.normal;

            Quaternion raycastHitRotation = Quaternion.FromToRotation(Vector3.up, raycastHitNormal);
            if (spawnPrefabTemplate != null) {
              GameObject.Instantiate(spawnPrefabTemplate, raycastHitPosition, raycastHitRotation);
            }
            transform.position = raycastHitPosition;
            transform.rotation = raycastHitRotation;           
        }
        else
        {
            Object.Destroy(gameObject);
        }
        
    }

}
