using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockLocomotion : MonoBehaviour
{
	[SerializeField] private Rigidbody rb;
	[SerializeField] private float initialVelocity;
	[SerializeField] private float duration;

	private float startTime;

    void Awake()
    {
    	startTime = Time.time;
        rb.AddForce(Vector3.up * initialVelocity, ForceMode.VelocityChange);
    }

    void Update()
    {
        if(Time.time - startTime >= duration){
            rb.velocity = Vector3.zero;
            Component.Destroy(this);
        }
    }
}
