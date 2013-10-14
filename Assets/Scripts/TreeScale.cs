using UnityEngine;
using System.Collections;

public class TreeScale : MonoBehaviour {
	// Use this for initialization
	void Start () {
		transform.localPosition += new Vector3(Random.Range (-30, 30), 0, Random.Range (-30,30));
		transform.localEulerAngles += new Vector3(0, Random.Range (0, 360), 0);
		transform.localScale = new Vector3(Random.Range(1.5f, 5f), Random.Range (1.5f,5f), Random.Range (1.5f, 5f));
		CheckGround ();
	}
	void CheckGround()
	{
		RaycastHit hit;
		if (Physics.Raycast (transform.position, -Vector3.up, out hit, 120f))
		{
			transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
			renderer.enabled = true;
			//Debug.Log (hit.point.y);
		}
		else
		{
			Destroy (gameObject);	
		}
	}
	// Update is called once per frame
	void Update () {
	}
}