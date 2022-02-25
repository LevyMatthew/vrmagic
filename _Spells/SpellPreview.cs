using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPreview : MonoBehaviour
{
    public float range;
    public float angle;
    public float minimumDistance;
    public LayerMask layerMask;
    public InteractableType interactableType;
    public GameObject indicator;
    
    public enum InteractableType
    {
        Levitatable,
        Flammable,
    }

    private Transform target;

    public void FixedUpdate()
    {
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            SpellInteractable si = hit.transform.gameObject.GetComponent<SpellInteractable>();
            if (CanInteract(si))
            {
                Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green);
                target = hit.transform;
                indicator.transform.position = transform.forward * hit.distance + transform.position;                
            }
            else
            {
                Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
                target = null;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 1000, Color.white);
            target = null;
        }
        indicator.SetActive(target != null);
    }

    public bool CanInteract(SpellInteractable si)
    {
        if (si == null)
            return false;
        if (interactableType == InteractableType.Levitatable && si.isLevitatable)       
            return true;
        if (interactableType == InteractableType.Flammable && si.isFlammable)
            return true;
        return false;
    }

    public Transform GetTarget()
    {
        return target;
    }
}