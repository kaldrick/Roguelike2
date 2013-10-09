using UnityEngine;
using System.Collections;

public class TreeCheck : MonoBehaviour {
	public GameObject player;
	public float fTime = 0.0f;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("PlayerWorld");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		fTime += Time.fixedDeltaTime;
		if(fTime > 3.0f)
		{
			if(!renderer.IsVisibleFrom (player.GetComponent<PlayerControllerWorld>().normalCam))
			{
				renderer.enabled = false;
			}
			else
			{
				renderer.enabled = true;	
			}
			fTime = 0.0f;
		}
	}
}
