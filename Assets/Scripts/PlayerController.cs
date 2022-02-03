using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public Vector2 mouseSens = new Vector2(2.0f, 2.0f);

	private Vector3 camRotation = new Vector3(0.0f, 0.0f, 0.0f);

	private bool paused = false;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void Update()
	{
		if(!paused){
			camRotation.x += mouseSens.x * Input.GetAxis("Mouse X");
			camRotation.y += mouseSens.y * Input.GetAxis("Mouse Y");

			transform.eulerAngles = new Vector3(-camRotation.y, camRotation.x, 0.0f);
		}

		if (Input.GetKey(KeyCode.Escape)){
			paused = true;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else if(paused && Input.GetButtonDown("Fire1")){
			paused = false;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
}
