using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBallSpell : MonoBehaviour
{
	public Transform controller;
	Rigidbody rb;
	int phase = 0;
	
	float startTime = 0;

	public float minSize = 0;
	public float maxSize = 2;
	public float chargeTime = 2.0f;
	float scale = 0;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		float currTime = Time.time;
		if(Input.GetButton("Fire1")){
			if(phase == 0){
				phase = 1;
				gameObject.transform.position = Vector3.zero;
				rb.velocity = Vector3.zero;
				rb.angularVelocity = Vector3.zero;
				startTime = currTime;
				Debug.Log("begin");
			}
			else if(phase == 1){
				//charge
				scale = Mathf.Lerp(minSize, maxSize, (currTime - startTime) / chargeTime);
				gameObject.transform.localScale = new Vector3(scale, scale, scale);
				Debug.Log("charge");
			}
			else if(phase == 2){
				phase = 0;
			}
		}
		else{
			if(phase == 1){
				//release
				phase = 2;
				Debug.Log("release");
			}
			if(phase == 2){
				rb.AddForce(controller.forward * 2.0f);
			}
		}
	}
}
