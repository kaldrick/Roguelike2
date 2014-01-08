using UnityEngine;
using System.Linq;
using System.Collections;
public enum houseType
{
	Farm, Sawmill, Hunter, 
	Chief, Shaman, MechanicalWorkshop, 
	ArmsWorkshop, Medic, Warehouse, 
	Caravan, LootBrotherhood, Distillery
};
public class TooltipData
{
	public Vector3 chatkaPos;
	public float rand = 0;
	public int children = 0;
	public int parent = 0;
	public int house1Availability = 0;
	public int house2Availability = 0;
	public int house3Availability = 0;
	public int house4Availability = 0;
	public int house5Availability = 0;
	public int house6Availability = 0;
	public houseType house1, house2, house3, house4, house5, house6;
	public float House1ratio = 0f;
	public float House2ratio = 0f;
	public float House3ratio = 0f;
	public float House4ratio = 0f;
	public float House5ratio = 0f;
	public float House6ratio = 0f;
	public bool Farm = false;
	public bool Sawmill = false;
	public bool Hunter = false;
	public bool Chief = false;
	public bool Shaman = false;
	public bool MechanicalWorkshop = false;
	public bool ArmsWorkshop = false;
	public bool Medic = false;
	public bool Warehouse = false;
	public bool Caravan = false;
	public bool LootBrotherhood = false;
	public bool Distillery = false;
	public int resources = 0;
	public string name;
}
public class Tooltip : MonoBehaviour {
	public bool bShow = false;
	public bool bLoaded = false;	
	public Camera cam;
	public TooltipData tooltip = new TooltipData();
	public PlayerControllerWorld player;
	public PlayerData playerData;
	public SaveSceneComponent save;
	public int i = 0;
	public int j = 0;
	public float r = 0f;
	public float z = 0f;
	public int children = 0;
	// Use this for initialization
	void Start () {
		cam = GameObject.Find ("CameraG").GetComponent<Camera>();
		save = GameObject.Find ("SaveTracker").GetComponent<SaveSceneComponent>();
		if(!bLoaded)
		{
			tooltip.rand = Random.Range (0, 65000);

			if(!transform.parent.GetComponent<Tooltip>())
			{
				//Debug.Log ("Farm: " + tooltip.house1Availability);
				GameObject.Find ("PlayerWorld").GetComponent<PlayerData>().numberOfChatkiParent++;
				transform.name = "Chatka parent" + GameObject.Find ("PlayerWorld").GetComponent<PlayerData>().numberOfChatkiParent;
				tooltip.name = transform.name;
				tooltip.parent = GameObject.Find ("PlayerWorld").GetComponent<PlayerData>().numberOfChatkiParent;
				StartCoroutine (Test ());

			}
			StartCoroutine (CheckTooltip ());
		}
		else
		{
			transform.name = tooltip.name;	
		}

		player = GameObject.Find ("PlayerWorld").GetComponent<PlayerControllerWorld>();
		playerData = GameObject.Find ("PlayerWorld").GetComponent<PlayerData>();
	}
	IEnumerator Test()
	{
		yield return new WaitForSeconds(3f);
		tooltip.House1ratio = Random.Range (10f, 20f);
		tooltip.House2ratio = Random.Range (10f, 20f);
		tooltip.House3ratio = Random.Range (30f, 50f);
		tooltip.House4ratio = Random.Range (30f, 50f);
		tooltip.House5ratio = Random.Range (80f, 120f);
		tooltip.House6ratio = Random.Range (80f, 120f);

		////Debug.Log ("Ma tatusia? : " + transform.parent.GetComponents<Tooltip>().Length + " Ma dzieci i numer: " + tooltip.children*2 + transform.name);
		while(i<tooltip.children*2)
		{
			j = Random.Range (0,6);
			if(j == 0 && tooltip.house1Availability == 0)
			{
				tooltip.house1Availability = 1;
				if(!tooltip.Chief)
				{
					////Debug.Log ("Działa?");
					tooltip.house1 = houseType.Chief; tooltip.Chief = true;  
				}
				else
				{
					r = Random.Range (0f, 2.66f);
					////Debug.Log ("Działam?");
					if(r > 2.15f && !tooltip.Farm)
					{
						tooltip.house1 =  houseType.Farm; tooltip.Farm = true;
					}
					else if(r > 1.65f && r < 2.15f && !tooltip.Sawmill)
					{
						tooltip.house1 =  houseType.Sawmill; tooltip.Sawmill = true;  
					}
					else if(r > 1.15f && r < 1.65f && !tooltip.Hunter)
					{
						tooltip.house1 =  houseType.Hunter; tooltip.Hunter = true;  
					}
					else if(r > 0.85f && r < 1.15f && !tooltip.Shaman)
					{
						tooltip.house1 =  houseType.Shaman; tooltip.Shaman = true;  
					}
					else if(r > 0.75f && r < 0.85f && !tooltip.ArmsWorkshop && !tooltip.MechanicalWorkshop)
					{
						float z = Random.Range (0f, 1f);
						if(z < 0.5f)
						{
							tooltip.house1 =  houseType.ArmsWorkshop; tooltip.ArmsWorkshop = true;  
						}
						else
						{
							tooltip.house1 =  houseType.MechanicalWorkshop; tooltip.MechanicalWorkshop = true;  
						}
					}
					else if(r > 0.70f && r < 0.75f && !tooltip.Medic)
					{
						tooltip.house1 =  houseType.Medic; tooltip.Medic = true;  
					}
					else if(r > 0.60f && r < 0.70f && !tooltip.Warehouse)
					{
						tooltip.house1 =  houseType.Warehouse; tooltip.Warehouse = true;  
					}
					else if(r > 0.40f && r < 0.60f && !tooltip.Caravan)
					{
						tooltip.house1 =  houseType.Caravan; tooltip.Caravan = true;  
					}
					else if(r > 0.30f && r < 0.40f && !tooltip.LootBrotherhood)
					{
						tooltip.house1 =  houseType.LootBrotherhood;
					}
					else if(r < 0.3f && !tooltip.Distillery)
					{
						tooltip.house1 =  houseType.Distillery; tooltip.Distillery = true;  
					}
				}
				i++;
			}
			else if(j == 1 && tooltip.house2Availability == 0)
			{
				tooltip.house2Availability = 1;
				if(!tooltip.Chief)
				{
					tooltip.house2 = houseType.Chief; tooltip.Chief = true;  
				}
				else
				{
					r = Random.Range (0f, 2.66f);
					if(r > 2.15f && !tooltip.Farm)
					{
						tooltip.house2 =  houseType.Farm; tooltip.Farm = true;  
					}
					else if(r > 1.65f && r < 2.15f && !tooltip.Sawmill)
					{
						tooltip.house2 =  houseType.Sawmill; tooltip.Sawmill = true;  
					}
					else if(r > 1.15f && r < 1.65f && !tooltip.Hunter)
					{
						tooltip.house2 =  houseType.Hunter; tooltip.Hunter = true;  
					}
					else if(r > 0.85f && r < 1.15f && !tooltip.Shaman)
					{
						tooltip.house2 =  houseType.Shaman; tooltip.Shaman = true;  
					}
					else if(r > 0.75f && r < 0.85f && !tooltip.ArmsWorkshop && !tooltip.MechanicalWorkshop)
					{
						float z = Random.Range (0f, 1f);
						if(z < 0.5f)
						{
							tooltip.house2 =  houseType.ArmsWorkshop; tooltip.ArmsWorkshop = true;  
						}
						else
						{
							tooltip.house2 =  houseType.MechanicalWorkshop; tooltip.MechanicalWorkshop = true;  
						}
					}
					else if(r > 0.70f && r < 0.75f && !tooltip.Medic)
					{
						tooltip.house2 =  houseType.Medic; tooltip.Medic = true;  
					}
					else if(r > 0.60f && r < 0.70f && !tooltip.Warehouse)
					{
						tooltip.house2 =  houseType.Warehouse; tooltip.Warehouse = true;  
					}
					else if(r > 0.40f && r < 0.60f && !tooltip.Caravan)
					{
						tooltip.house2 =  houseType.Caravan; tooltip.Caravan = true;  
					}
					else if(r > 0.30f && r < 0.40f && !tooltip.LootBrotherhood)
					{
						tooltip.house2 =  houseType.LootBrotherhood;
					}
					else if(r < 0.3f && !tooltip.Distillery)
					{
						tooltip.house2 =  houseType.Distillery; tooltip.Distillery = true;  
					}
				}
				i++;
			}
			else if(j == 2 && tooltip.house3Availability == 0)
			{
				tooltip.house3Availability = 1;
				if(!tooltip.Chief)
				{
					tooltip.house3 = houseType.Chief; tooltip.Chief = true;  
				}
				else
				{
					r = Random.Range (0f, 2.66f);
					if(r > 2.15f && !tooltip.Farm)
					{
						tooltip.house3 =  houseType.Farm; tooltip.Farm = true;  
					}
					else if(r > 1.65f && r < 2.15f && !tooltip.Sawmill)
					{
						tooltip.house3 =  houseType.Sawmill; tooltip.Sawmill = true;  
					}
					else if(r > 1.15f && r < 1.65f && !tooltip.Hunter)
					{
						tooltip.house3 =  houseType.Hunter; tooltip.Hunter = true;  
					}
					else if(r > 0.85f && r < 1.15f && !tooltip.Shaman)
					{
						tooltip.house3 =  houseType.Shaman; tooltip.Shaman = true;  
					}
					else if(r > 0.75f && r < 0.85f && !tooltip.ArmsWorkshop && !tooltip.MechanicalWorkshop)
					{
						float z = Random.Range (0f, 1f);
						if(z < 0.5f)
						{
							tooltip.house3 =  houseType.ArmsWorkshop; tooltip.ArmsWorkshop = true;  
						}
						else
						{
							tooltip.house3 =  houseType.MechanicalWorkshop; tooltip.MechanicalWorkshop = true;  
						}
					}
					else if(r > 0.70f && r < 0.75f && !tooltip.Medic)
					{
						tooltip.house3 =  houseType.Medic; tooltip.Medic = true;  
					}
					else if(r > 0.60f && r < 0.70f && !tooltip.Warehouse)
					{
						tooltip.house3 =  houseType.Warehouse; tooltip.Warehouse = true;  
					}
					else if(r > 0.40f && r < 0.60f && !tooltip.Caravan)
					{
						tooltip.house3 =  houseType.Caravan; tooltip.Caravan = true;  
					}
					else if(r > 0.30f && r < 0.40f && !tooltip.LootBrotherhood)
					{
						tooltip.house3 =  houseType.LootBrotherhood;
					}
					else if(r < 0.3f && !tooltip.Distillery)
					{
						tooltip.house3 =  houseType.Distillery; tooltip.Distillery = true;  
					}
				}
				i++;
			}
			else if(j == 3 && tooltip.house4Availability == 0)
			{
				tooltip.house4Availability = 1;
				if(!tooltip.Chief)
				{
					tooltip.house4 = houseType.Chief; tooltip.Chief = true;  
				}
				else
				{
					r = Random.Range (0f, 2.66f);
					if(r > 2.15f && !tooltip.Farm)
					{
						tooltip.house4 =  houseType.Farm; tooltip.Farm = true;  
					}
					else if(r > 1.65f && r < 2.15f && !tooltip.Sawmill)
					{
						tooltip.house4 =  houseType.Sawmill; tooltip.Sawmill = true;  
					}
					else if(r > 1.15f && r < 1.65f && !tooltip.Hunter)
					{
						tooltip.house4 =  houseType.Hunter; tooltip.Hunter = true;  
					}
					else if(r > 0.85f && r < 1.15f && !tooltip.Shaman)
					{
						tooltip.house4 =  houseType.Shaman; tooltip.Shaman = true;  
					}
					else if(r > 0.75f && r < 0.85f && !tooltip.ArmsWorkshop && !tooltip.MechanicalWorkshop)
					{
						float z = Random.Range (0f, 1f);
						if(z < 0.5f)
						{
							tooltip.house4 =  houseType.ArmsWorkshop; tooltip.ArmsWorkshop = true;  
						}
						else
						{
							tooltip.house4 =  houseType.MechanicalWorkshop; tooltip.MechanicalWorkshop = true;  
						}
					}
					else if(r > 0.70f && r < 0.75f && !tooltip.Medic)
					{
						tooltip.house4 =  houseType.Medic; tooltip.Medic = true;  
					}
					else if(r > 0.60f && r < 0.70f && !tooltip.Warehouse)
					{
						tooltip.house4 =  houseType.Warehouse; tooltip.Warehouse = true;  
					}
					else if(r > 0.40f && r < 0.60f && !tooltip.Caravan)
					{
						tooltip.house4 =  houseType.Caravan; tooltip.Caravan = true;  
					}
					else if(r > 0.30f && r < 0.40f && !tooltip.LootBrotherhood)
					{
						tooltip.house4 =  houseType.LootBrotherhood;
					}
					else if(r < 0.3f && !tooltip.Distillery)
					{
						tooltip.house4 =  houseType.Distillery; tooltip.Distillery = true;  
					}
				}
				i++;
			}
			else if(j == 4 && tooltip.house5Availability == 0)
			{
				tooltip.house5Availability = 1;
				if(!tooltip.Chief)
				{
					tooltip.house5 = houseType.Chief; tooltip.Chief = true;  
				}
				else
				{
					r = Random.Range (0f, 2.66f);
					if(r > 2.15f && !tooltip.Farm)
					{
						tooltip.house5 =  houseType.Farm; tooltip.Farm = true;  
					}
					else if(r > 1.65f && r < 2.15f && !tooltip.Sawmill)
					{
						tooltip.house5 =  houseType.Sawmill; tooltip.Sawmill = true;  
					}
					else if(r > 1.15f && r < 1.65f && !tooltip.Hunter)
					{
						tooltip.house5 =  houseType.Hunter; tooltip.Hunter = true;  
					}
					else if(r > 0.85f && r < 1.15f && !tooltip.Shaman)
					{
						tooltip.house5 =  houseType.Shaman; tooltip.Shaman = true;  
					}
					else if(r > 0.75f && r < 0.85f && !tooltip.ArmsWorkshop && !tooltip.MechanicalWorkshop)
					{
						float z = Random.Range (0f, 1f);
						if(z < 0.5f)
						{
							tooltip.house5 =  houseType.ArmsWorkshop; tooltip.ArmsWorkshop = true;  
						}
						else
						{
							tooltip.house5 =  houseType.MechanicalWorkshop; tooltip.MechanicalWorkshop = true;  
						}
					}
					else if(r > 0.70f && r < 0.75f && !tooltip.Medic)
					{
						tooltip.house5 =  houseType.Medic; tooltip.Medic = true;  
					}
					else if(r > 0.60f && r < 0.70f && !tooltip.Warehouse)
					{
						tooltip.house5 =  houseType.Warehouse; tooltip.Warehouse = true;  
					}
					else if(r > 0.40f && r < 0.60f && !tooltip.Caravan)
					{
						tooltip.house5 =  houseType.Caravan; tooltip.Caravan = true;  
					}
					else if(r > 0.30f && r < 0.40f && !tooltip.LootBrotherhood)
					{
						tooltip.house5 =  houseType.LootBrotherhood;
					}
					else if(r < 0.3f && !tooltip.Distillery)
					{
						tooltip.house5 =  houseType.Distillery; tooltip.Distillery = true;  
					}
				}
				i++;
			}
			else if(j == 5 && tooltip.house6Availability == 0)
			{
				tooltip.house6Availability = 1;
				if(!tooltip.Chief)
				{
					tooltip.house6 = houseType.Chief; tooltip.Chief = true;  
				}
				else
				{
					r = Random.Range (0f, 2.66f);
					if(r > 2.15f && !tooltip.Farm)
					{
						tooltip.house6 =  houseType.Farm; tooltip.Farm = true;  
					}
					else if(r > 1.65f && r < 2.15f && !tooltip.Sawmill)
					{
						tooltip.house6 =  houseType.Sawmill; tooltip.Sawmill = true;  
					}
					else if(r > 1.15f && r < 1.65f && !tooltip.Hunter)
					{
						tooltip.house6 =  houseType.Hunter; tooltip.Hunter = true;  
					}
					else if(r > 0.85f && r < 1.15f && !tooltip.Shaman)
					{
						tooltip.house6 =  houseType.Shaman; tooltip.Shaman = true;  
					}
					else if(r > 0.75f && r < 0.85f && !tooltip.ArmsWorkshop && !tooltip.MechanicalWorkshop)
					{
						float z = Random.Range (0f, 1f);
						if(z < 0.5f)
						{
							tooltip.house6 =  houseType.ArmsWorkshop; tooltip.ArmsWorkshop = true;  
						}
						else
						{
							tooltip.house6 =  houseType.MechanicalWorkshop; tooltip.MechanicalWorkshop = true;  
						}
					}
					else if(r > 0.70f && r < 0.75f && !tooltip.Medic)
					{
						tooltip.house6 =  houseType.Medic; tooltip.Medic = true;  
					}
					else if(r > 0.60f && r < 0.70f && !tooltip.Warehouse)
					{
						tooltip.house6 =  houseType.Warehouse; tooltip.Warehouse = true;  
					}
					else if(r > 0.40f && r < 0.60f && !tooltip.Caravan)
					{
						tooltip.house6 =  houseType.Caravan; tooltip.Caravan = true;  
					}
					else if(r > 0.30f && r < 0.40f && !tooltip.LootBrotherhood)
					{
						tooltip.house6 =  houseType.LootBrotherhood;
					}
					else if(r < 0.3f && !tooltip.Distillery)
					{
						tooltip.house6 =  houseType.Distillery; tooltip.Distillery = true;  
					}
				}
				i++;
			}
			//Debug.Log ("IIIIII: " + j);
		}
	}
	IEnumerator CheckTooltip()
	{
		yield return new  WaitForSeconds(3f);
		if(transform.parent.GetComponent<Tooltip>())
		{
			tooltip.children = transform.parent.GetComponent<Tooltip>().tooltip.children;	
			tooltip.parent = transform.parent.GetComponent<Tooltip>().tooltip.parent;
			
			tooltip.house1 = transform.parent.GetComponent<Tooltip>().tooltip.house1;
			tooltip.House1ratio = transform.parent.GetComponent<Tooltip>().tooltip.House1ratio;
			tooltip.house1Availability = transform.parent.GetComponent<Tooltip>().tooltip.house1Availability;
			
			tooltip.house2 = transform.parent.GetComponent<Tooltip>().tooltip.house2;
			tooltip.House2ratio = transform.parent.GetComponent<Tooltip>().tooltip.House2ratio;
			tooltip.house2Availability = transform.parent.GetComponent<Tooltip>().tooltip.house2Availability;
			
			tooltip.house3 = transform.parent.GetComponent<Tooltip>().tooltip.house3;
			tooltip.House3ratio = transform.parent.GetComponent<Tooltip>().tooltip.House3ratio;
			tooltip.house3Availability = transform.parent.GetComponent<Tooltip>().tooltip.house3Availability;
			
			tooltip.house4 = transform.parent.GetComponent<Tooltip>().tooltip.house4;
			tooltip.House4ratio = transform.parent.GetComponent<Tooltip>().tooltip.House4ratio;
			tooltip.house4Availability = transform.parent.GetComponent<Tooltip>().tooltip.house4Availability;
			
			tooltip.house5 = transform.parent.GetComponent<Tooltip>().tooltip.house5;
			tooltip.House5ratio = transform.parent.GetComponent<Tooltip>().tooltip.House5ratio;
			tooltip.house5Availability = transform.parent.GetComponent<Tooltip>().tooltip.house5Availability;
			
			tooltip.house6 = transform.parent.GetComponent<Tooltip>().tooltip.house6;
			tooltip.House6ratio = transform.parent.GetComponent<Tooltip>().tooltip.House6ratio;
			tooltip.house6Availability = transform.parent.GetComponent<Tooltip>().tooltip.house6Availability;

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
			tooltip.resources = (int)(tooltip.House1ratio * tooltip.house1Availability * playerData.fTimePassed + tooltip.House2ratio * tooltip.house2Availability * playerData.fTimePassed + 
			                          tooltip.House3ratio * tooltip.house3Availability * playerData.fTimePassed + tooltip.House4ratio * tooltip.house4Availability * playerData.fTimePassed + 
			                          tooltip.House5ratio * tooltip.house5Availability * playerData.fTimePassed + tooltip.House6ratio * tooltip.house6Availability * playerData.fTimePassed);
		}
	//	//Debug.Log ("Dzialam");
		
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
			//Debug.Log (GameObject.Find ("Text1").GetComponent<TextMesh>());
			GameObject.Find ("Text1").GetComponent<TextMesh>().text = tooltip.house1.ToString () + " " + tooltip.House1ratio.ToString ("f1") + "/s";
			GameObject.Find ("Text2").GetComponent<TextMesh>().text = tooltip.house2.ToString () + " " + tooltip.House2ratio.ToString ("f1") + "/s";
			GameObject.Find ("Text3").GetComponent<TextMesh>().text = tooltip.house3.ToString () + " " + tooltip.House3ratio.ToString ("f1") + "/s";
			GameObject.Find ("Text4").GetComponent<TextMesh>().text = tooltip.house4.ToString () + " " + tooltip.House4ratio.ToString ("f1") + "/s";
			GameObject.Find ("Text5").GetComponent<TextMesh>().text = tooltip.house5.ToString () + " " + tooltip.House5ratio.ToString ("f1") + "/s";
			GameObject.Find ("Text6").GetComponent<TextMesh>().text = tooltip.house6.ToString () + " " + tooltip.House6ratio.ToString ("f1") + "/s";

			GameObject.Find ("House1").GetComponent<ChatkaOnClick>().myName = tooltip.house1.ToString ();
			GameObject.Find ("House2").GetComponent<ChatkaOnClick>().myName = tooltip.house2.ToString ();
			GameObject.Find ("House3").GetComponent<ChatkaOnClick>().myName = tooltip.house3.ToString ();
			GameObject.Find ("House4").GetComponent<ChatkaOnClick>().myName = tooltip.house4.ToString ();
			GameObject.Find ("House5").GetComponent<ChatkaOnClick>().myName = tooltip.house5.ToString ();
			GameObject.Find ("House6").GetComponent<ChatkaOnClick>().myName = tooltip.house6.ToString ();
			GameObject.Find ("House1").GetComponent<ChatkaOnClick>().bInventoryChanged = false;
			GameObject.Find ("House2").GetComponent<ChatkaOnClick>().bInventoryChanged = false;
			GameObject.Find ("House3").GetComponent<ChatkaOnClick>().bInventoryChanged = false;
			GameObject.Find ("House4").GetComponent<ChatkaOnClick>().bInventoryChanged = false;
			GameObject.Find ("House5").GetComponent<ChatkaOnClick>().bInventoryChanged = false;
			GameObject.Find ("House6").GetComponent<ChatkaOnClick>().bInventoryChanged = false;
			GameObject.Find ("Resources").GetComponent<TextMesh>().text = "Resources: " + tooltip.resources.ToString ("f1");
			GameObject.Find ("City").GetComponent<CityVariables>().parentNumber = tooltip.parent;
			player.normalCam.enabled = false;
			player.cityCam.enabled = true;
			if(tooltip.house1Availability == 1)
			{
				GameObject.Find ("House1").renderer.enabled = true;
				GameObject.Find ("House1/Text1").renderer.enabled = true;
			}
			else
			{
				GameObject.Find ("House1").renderer.enabled = false;
				GameObject.Find ("Text1").renderer.enabled = false;
			}
			if(tooltip.house2Availability == 1)
			{
				GameObject.Find ("House2").renderer.enabled = true;
				GameObject.Find ("Text2").renderer.enabled = true;
			}
			else
			{
				GameObject.Find ("House2").renderer.enabled = false;
				GameObject.Find ("Text2").renderer.enabled = false;
			}
			if(tooltip.house3Availability == 1)
			{
				GameObject.Find ("House3").renderer.enabled = true;
				GameObject.Find ("Text3").renderer.enabled = true;
			}
			else
			{
				GameObject.Find ("House3").renderer.enabled = false;
				GameObject.Find ("Text3").renderer.enabled = false;
			}
			if(tooltip.house4Availability == 1)
			{
				GameObject.Find ("House4").renderer.enabled = true;
				GameObject.Find ("Text4").renderer.enabled = true;
			}
			else
			{
				GameObject.Find ("House4").renderer.enabled = false;
				GameObject.Find ("Text4").renderer.enabled = false;
			}
			if(tooltip.house5Availability == 1)
			{
				GameObject.Find ("House5").renderer.enabled = true;
				GameObject.Find ("Text5").renderer.enabled = true;
			}
			else
			{
				GameObject.Find ("House5").renderer.enabled = false;
				GameObject.Find ("Text5").renderer.enabled = false;
			}
			if(tooltip.house6Availability == 1)
			{
				GameObject.Find ("House6").renderer.enabled = true;
				GameObject.Find ("Text6").renderer.enabled = true;
			}
			else
			{
				GameObject.Find ("House6").renderer.enabled = false;
				GameObject.Find ("Text6").renderer.enabled = false;
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
