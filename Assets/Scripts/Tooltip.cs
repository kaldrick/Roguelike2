using UnityEngine;
using System.Collections;

public class Tooltip : MonoBehaviour {
	public bool bShow = false;
	public bool bLoaded = false;	
	public Camera cam;
	public Vector3 chatkaPos;
	public int rand = 0;
	// Use this for initialization
	void Start () {
		cam = GameObject.Find ("CameraG").GetComponent<Camera>();
		if(!bLoaded)
		{
			rand = Random.Range (0, 65000);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseOver(){
		if(renderer.enabled)
		{
			bShow = true;
		}
		Debug.Log ("Dzialam");
		
	}
	
	void OnMouseExit(){
		if(bShow)
		{
			bShow = false;	
		}
	}
	
	void OnGUI()
	{
		if(bShow)
		{
			Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
			GUI.Box(new Rect(screenPos.x, cam.pixelHeight - screenPos.y , 150, 25), "Academy" + rand.ToString ());
		}
	}
}
