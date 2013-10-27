using UnityEngine;
using System.Collections;

public class ChatkaScale : MonoBehaviour {
	public SaveSceneComponent save;
	public bool bCheckGround = true;
	public bool bLoaded = false;
	public float fTime = 0.0f;
	// Use this for initialization
	void Start () {
		//transform.localPosition += new Vector3(Random.Range (-30, 30), 0, Random.Range (-30,30));
		
		save = GameObject.Find ("SaveTracker").GetComponent<SaveSceneComponent>();
		//transform.localScale = new Vector3(Random.Range(1.5f, 5f), Random.Range (1.5f,5f), Random.Range (1.5f, 5f));
	}
	void Update()
	{
		if(bCheckGround)
		{
			fTime += Time.deltaTime;
		}
		if(fTime > 3.0f && bCheckGround)
		{
		//	transform.localPosition += new Vector3(Random.Range (-70, 70), 0, Random.Range (-70,70));
		//	transform.localEulerAngles += new Vector3(0, Random.Range (0, 360), 0);
			//transform.localEulerAngles += new Vector3(0, 0, Random.Range (0, 360));
		//	transform.localScale = new Vector3(Random.Range(1.5f, 5f), Random.Range (1.5f,5f), Random.Range (1.5f, 5f));
			CheckGround ();
			bCheckGround = false;
		}
		if(bLoaded)
		{
			CheckGround ();
			bLoaded = false;
		}
	}
	void CheckGround()
	{
		RaycastHit hit;
		if (Physics.Raycast (transform.position, -Vector3.up, out hit, 60f))
		{
			if(hit.point.y < -75f || hit.transform.name == "Tree(Clone)" || hit.transform.name == "chatka(Clone)")
			{
				Destroy (gameObject);
			}
			else
			{
				transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
				renderer.enabled = true;
				if(!save.chatkaPos.Contains (hit.point))
				{
					save.chatkaPos.Add (hit.point);
					//save.treesScale.Add(transform.localScale);
					save.chatkaRotation.Add(transform.localEulerAngles);
				}
			}
		}
		else
		{
			Destroy (gameObject);	
		}
	}
}
