using UnityEngine;
using System.Collections;
public class TooltipData
{
	public Vector3 chatkaPos;
	public int rand = 0;
	public int children = 0;
	public int parent = 0;
	public string name;
}
public class Tooltip : MonoBehaviour {
	public bool bShow = false;
	public bool bLoaded = false;	
	public Camera cam;
	public TooltipData tooltip = new TooltipData();
	public PlayerControllerWorld player;
	public SaveSceneComponent save;
	// Use this for initialization
	void Start () {
		cam = GameObject.Find ("CameraG").GetComponent<Camera>();
		save = GameObject.Find ("SaveTracker").GetComponent<SaveSceneComponent>();
		if(!bLoaded)
		{
			tooltip.rand = Random.Range (0, 65000);
			if(!transform.parent.GetComponent<Tooltip>())
			{
				GameObject.Find ("PlayerWorld").GetComponent<PlayerData>().numberOfChatkiParent++;
				transform.name = "Chatka parent" + GameObject.Find ("PlayerWorld").GetComponent<PlayerData>().numberOfChatkiParent;
				tooltip.name = transform.name;
				tooltip.parent = GameObject.Find ("PlayerWorld").GetComponent<PlayerData>().numberOfChatkiParent;
			}
			StartCoroutine (CheckTooltip ());
		}
		else
		{
			transform.name = tooltip.name;	
		}
		
		player = GameObject.Find ("PlayerWorld").GetComponent<PlayerControllerWorld>();
	}
	IEnumerator CheckTooltip()
	{
		yield return new  WaitForSeconds(3f);
		if(transform.parent.GetComponent<Tooltip>())
		{
			tooltip.children = transform.parent.GetComponent<Tooltip>().tooltip.children;	
			tooltip.parent = transform.parent.GetComponent<Tooltip>().tooltip.parent;
			save.save.tooltipData.Add (tooltip);
		}
		//yield return new WaitForFixedUpdate();
	}
	// Update is called once per frame
	void Update () {

	}
	void OnMouseOver(){
		if(renderer.enabled && (Vector3.Distance (transform.position, player.transform.position) < 100))
		{
			bShow = true;
		}
	//	Debug.Log ("Dzialam");
		
	}
	
	void OnMouseExit(){
		if(bShow)
		{
			bShow = false;	
		}
	}
	void OnMouseDown()
	{
		if((Vector3.Distance (transform.position, player.transform.position) < 100))
		{
			player.normalCam.enabled = false;
			player.cityCam.enabled = true;
			if(tooltip.children == 1) 
			{
				GameObject.Find ("House3").renderer.enabled = false;	
				GameObject.Find ("House4").renderer.enabled = false;	
				GameObject.Find ("House5").renderer.enabled = false;	
				GameObject.Find ("House6").renderer.enabled = false;	
			}
			if(tooltip.children == 2)
			{
				GameObject.Find ("House3").renderer.enabled = true;	
				GameObject.Find ("House4").renderer.enabled = true;	
				GameObject.Find ("House5").renderer.enabled = false;	
				GameObject.Find ("House6").renderer.enabled = false;	
			}
			if(tooltip.children == 3)
			{
				GameObject.Find ("House3").renderer.enabled = true;	
				GameObject.Find ("House4").renderer.enabled = true;	
				GameObject.Find ("House5").renderer.enabled = true;	
				GameObject.Find ("House6").renderer.enabled = true;	
			}
		}
	}
	void OnGUI()
	{
		if(bShow)
		{
			Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
			GUI.Box(new Rect(screenPos.x, cam.pixelHeight - screenPos.y , 200, 25), "Academy (size: " + tooltip.children.ToString () + " parent: " + tooltip.parent + ")");
		}
	}
}
