using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockLocomotion : MonoBehaviour
{
	[SerializeField] private Rigidbody rigidbody;
	[SerializeField] private float initialVelocity;
	[SerializeField] private float duration;

	private float startTime;

    void Start()
    {
    	startTime = Time.time;
    	rigidbody.AddForce(Vector3.up * initialVelocity, ForceMode.VelocityChange);
    }

    void Update()
    {
        if(Time.time - startTime >= duration){
        	rigidbody.velocity = Vector3.zero;
        }
    }
}
