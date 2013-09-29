using UnityEngine;
using System.Collections;

public class TreeScale : MonoBehaviour {
	public float fTime = 0.0f;
	// Use this for initialization
	void Start () {
		
		transform.localScale = new Vector3(Random.Range(1.5f, 5f), Random.Range (1.5f,5f), Random.Range (1.5f, 5f));
		
		CheckGround ();
	}
	void CheckGround()
	{
		RaycastHit hit;
		if (Physics.Raycast (transform.position, -Vector3.up, out hit, 120f))
		{
			transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
			//Debug.Log (hit.point.y);
		}
	}
	// Update is called once per frame
	void Update () {
	}
}