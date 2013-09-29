using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float rotationInput = Input.GetAxis("Horizontal") * Time.deltaTime * 128;
		float moveInput = Input.GetAxis ("Vertical") * Time.deltaTime * 18192;
		transform.Translate (transform.forward * moveInput);
		transform.Rotate(new Vector3(0, rotationInput, 0));
		/*if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			this.transform.Translate (transform.forward * Time.deltaTime * 18192);
		}
		if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
		{
			this.transform.Translate (-transform.forward * Time.deltaTime * 18192);
		}
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			this.transform.Translate (Vector3.left * Time.deltaTime * 8192);
		}
		if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			this.transform.Translate (Vector3.right * Time.deltaTime * 8192);
		}*/
		if(Input.GetAxis ("Mouse ScrollWheel") != 0f)
		{
			this.transform.Translate (-transform.up * Input.GetAxis ("Mouse ScrollWheel") * 8192);
		}
	}
}
