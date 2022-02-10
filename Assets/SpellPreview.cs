using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPreview : MonoBehaviour
{
    public float range;
    public float angle;
    public float minimumDistance;
    public LayerMask layerMask;
    public InteractableType interacableType;
    
    public enum InteractableType
    {
        Levitatable,
        Flammable,
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    public void FixedUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            SpellInteractable si = hit.transform.gameObject.GetComponent<SpellInteractable>();
            if (CanInteract(si)) {
                Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }

    public bool CanInteract(SpellInteractable si)
    {
        if (si == null)
            return false;
        if (interacableType == InteractableType.Levitatable && si.isLevitatable)       
            return true;
        if (interacableType == InteractableType.Flammable && si.isFlammable)
            return true;
        return false;
    }
}