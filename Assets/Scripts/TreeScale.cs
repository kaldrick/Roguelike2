using UnityEngine;
using System.Collections;

public class TreeScale : MonoBehaviour {
	public Vector3 coord;
	// Use this for initialization
	void Start () {
		transform.localPosition += new Vector3(Random.Range (-70, 70), 0, Random.Range (-70,70));
		transform.localEulerAngles += new Vector3(0, Random.Range (0, 360), 0);
		transform.localScale = new Vector3(Random.Range(1.5f, 5f), Random.Range (1.5f,5f), Random.Range (1.5f, 5f));
		CheckGround ();
	}
	void CheckGround()
	{
		RaycastHit hit;
		if (Physics.Raycast (transform.position, -Vector3.up, out hit, 120f))
		{
			if(hit.point.y < -75f || hit.transform.name == "Tree(Clone)" || hit.transform.name == "chatka(Clone)" || hit.transform.name == "Tree" ||hit.transform.name == "chatka")
			{
				Destroy (gameObject);
			}
			else
			{
				transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
				renderer.enabled = true;
			}
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