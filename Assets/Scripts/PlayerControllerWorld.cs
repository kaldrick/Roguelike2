﻿using UnityEngine;
using System.Collections;

public class PlayerControllerWorld : MonoBehaviour {
	private Vector3 newPos;
	private bool bChangeCamera;
	public Camera normalCam, overTopCam;
	public float fZoom = 0.0f;
	public GameObject cube2;
	public float posY, posZ, rotX, prevPosY;
	public Vector3 camPos, camRot, startPos, startRot;
	// Use this for initialization
	void Start () {
		rigidbody.freezeRotation = true;
		Physics.gravity = new Vector3(0f, -1024.0f, 0f);
		normalCam.enabled = true; 
		overTopCam.enabled = false;
		camPos = new Vector3(0, 32, -8);
		camRot = new Vector3(60, 0, 0);
		posY = 32f;
		posZ = -8f;
		rotX = 60f;
	}
	
	// Update is called once per frame
	void Update () {
		
		
		/*if(normalCam.enabled && fZoom != 0f)
		{
			normalCam.transform.localPosition = new Vector3(normalCam.transform.localPosition.x, Mathf.Clamp (normalCam.transform.localPosition.y - fZoom * 6f, 14f, 32f), Mathf.Clamp (normalCam.transform.localPosition.z - fZoom*2f, -18f, -8f));
			normalCam.transform.localEulerAngles = new Vector3 (Mathf.Clamp (normalCam.transform.localEulerAngles.x - fZoom * 12f, 24f, 60f), normalCam.transform.localEulerAngles.y, normalCam.transform.localEulerAngles.z);
		}*/
		if (Input.GetKeyDown(KeyCode.F2)) 
		{
			if(normalCam.enabled)
			{
				normalCam.enabled = false;
				overTopCam.enabled = true;
				//prevPosY = transform.localPosition.y;
				//transform.localScale = new Vector3(64, 1024, 64);
				cube2.renderer.enabled = true;
				RenderSettings.fog = false;
			}
			else
			{
				normalCam.enabled = true;
				overTopCam.enabled = false;
				transform.localScale = new Vector3(1, 1, 1);
				cube2.renderer.enabled = false;
				//transform.localPosition = new Vector3(transform.localPosition.x, prevPosY + 5f, transform.localPosition.z);
				RenderSettings.fog = true;
			}
		}
	}
	void FixedUpdate()
	{
		float rotationInput = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * 128;
		float moveInput = Input.GetAxis ("Vertical") * Time.fixedDeltaTime * 4096;
		float scrollInput = -Input.GetAxis ("Mouse ScrollWheel");
		if(scrollInput != 0f)
		{
			fZoom = Mathf.Clamp (fZoom + scrollInput * 10f, -10f, 10f);
			posY = Mathf.Clamp (camPos.y - fZoom * 6f, 14f, 32f);
			posZ = Mathf.Clamp (camPos.z - fZoom*2f, -18f, -8f);
			rotX = Mathf.Clamp (normalCam.transform.localEulerAngles.x - fZoom * 12f, 24f, 60f);	
			/*if(normalCam.enabled && fZoom != 0f)
			{
				normalCam.transform.localPosition = new Vector3(normalCam.transform.localPosition.x, Mathf.Clamp (normalCam.transform.localPosition.y - fZoom * 6f, 8f, 32f), Mathf.Clamp (normalCam.transform.localPosition.z - fZoom*2f, -24f, -8f));
				normalCam.transform.localEulerAngles = new Vector3 (Mathf.Clamp (normalCam.transform.localEulerAngles.x - fZoom * 12f, 0f, 60f), normalCam.transform.localEulerAngles.y, normalCam.transform.localEulerAngles.z);
			}*/
		}
		else
		{
			fZoom = 0f;
		}
		camPos.y = Mathf.Lerp (normalCam.transform.localPosition.y, posY, Time.deltaTime * 4.5f);
		camPos.z = Mathf.Lerp (normalCam.transform.localPosition.z, posZ, Time.deltaTime * 4.5f);
		camRot.x = Mathf.Lerp (normalCam.transform.localEulerAngles.x, rotX, Time.deltaTime * 4.5f);
		normalCam.transform.localPosition = camPos;
		normalCam.transform.localEulerAngles = camRot;	
		rigidbody.velocity = transform.forward * moveInput;
		transform.Rotate(new Vector3(0, rotationInput, 0));
	}
	void OnCollisionEnter(Collision other)
	{
		Debug.Log ("BUM!" + other.collider.name);
	}
}
