using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class PlayerData : MonoBehaviour {
	public Vector3 centerChunkPos;
	public List<ChunkData> chunks;
//	public List<ChunkData> chunks2;
	public List<GameObject> trees;
	public GenerateTerrain terrain;
	public GameObject tree, village;
	public enum Dir { North, NorthEast, NorthWest, West, East, South, SouthEast, SouthWest };
	public int uncoverArea = 1;
	public Dir currentDir;
	VoxelChunk m_voxelChunktemp; 
	public UnityThreading.ActionThread myThread;	
	public int numberOfTrees = 0;
	public int numberOfChatki = 0;
	public int numberOfChatkiParent = 0;
//	public CombineChildren[] combiner;
	public float fTimePassed = 0f;
	public GameObject[] drzewka;
	public List<Item> Inventory = new List<Item>();
	public int InventoryCount = 0;
	public int HouseInventoryCount = 0;
	public GameObject prevItem;
	public GameObject InventoryParent;
	public GameObject HouseInventoryParent;
	public GameObject newItem;
	public GameObject itemPrefab;
	public int quantityTest = 0;
	public int removeTest = 0;
	public int cash = 1000;
	public Item cashObject;
	//public System.Predicate<ChunkData> chunkPredicate = new System.Predicate<ChunkData>(checkChunk);
	// Use this for initialization
	void Start () {
		terrain = GameObject.Find ("TerrainGenerator").GetComponent<GenerateTerrain>();
		InventoryParent = GameObject.Find ("Grid");
	/*	Inventory.Add (new Item("Iron", 15, 120));
		Inventory.Add (new Item("Steel", 10, 140));
		Inventory.Add (new Item("Steel", 10, 140));
		Inventory.Add (new Item("Steel", 10, 140));
		Inventory.Add (new Item("Steel", 10, 140));
		Inventory.Add (new Item("Steel", 10, 140));
		Inventory.Add (new Item("Steel", 10, 140));
		Inventory.Add (new Item("Steel", 10, 140));
		Inventory.Add (new Item("Steel", 10, 140));
		Inventory.Add (new Item("Steel", 10, 140));
		Inventory.Add (new Item("Steel", 10, 140));
		Inventory.Add (new Item("Steel", 10, 140));
		Inventory.Add (new Item("Steel", 10, 140));
		Inventory.Add (new Item("Steel", 10, 140));
		Inventory.Add (new Item("Steel", 10, 140));
		Inventory.Add (new Item("Steel", 10, 140));
		Inventory.Add (new Item("Steel", 10, 140));
		Inventory.Add (new Item("Steel", 10, 140));
		Inventory.Add (new Item("Steel", 10, 140));*/
	}
	void CopyTo(Item oldItem, Item newItem)
	{
		newItem.name = oldItem.name;
		newItem.quality = oldItem.quality;
		newItem.quantity = 1;
		newItem.weight = oldItem.weight;
		newItem.price = oldItem.price;
	}
	public void AddItem(Item item)
	{
		if(cash >= item.price)
		{
			StartCoroutine (corAdd (item));
		}
	}
	IEnumerator corAdd(Item item)
	{
		//item.gameObject.name = item.gameObject.name.Remove (item.gameObject.name.Length - 1) + "i";
		yield return new WaitForSeconds(0.1f);
		cash -= item.price;
		if(Inventory.Find (i => i.name == item.name))
		{
			quantityTest++;
			//Debug.Log ("ZNALAZLEM: " + item.name);
			//Debug.Log ("ZNALAZLEM: " + item.name);
			Debug.Log ("Znalazłem: " + item.name + " na miejscu: " + Inventory.IndexOf (item) + "w PlayerData" + quantityTest);
			//if(quantityTest > 1)
			//{
			Inventory.Find (i => i.name == item.name).quantity++;
			Inventory.Find (i => i.name == item.name).bChecked = false;
			yield return new WaitForSeconds(0.1f);
			//}
		}
		else
		{
			quantityTest = 0;
			newItem = Instantiate (itemPrefab, itemPrefab.transform.position, itemPrefab.transform.rotation) as GameObject;
			newItem.AddComponent<Item>();
			CopyTo (item, newItem.GetComponent<Item>());
			if(HouseInventoryParent == null)
			{
				HouseInventoryParent = GameObject.Find ("HouseGrid");
			}
			newItem.transform.parent = HouseInventoryParent.transform;
			newItem.transform.name = newItem.GetComponent<Item>().name + "i";
			//newItem.transform.localPosition += new Vector3(-32,0,0);
			newItem.transform.localScale = new Vector3(1,1,1);
			//newItem.GetComponent<Item>().houseName = gameObject.name;
			Inventory.Add (newItem.GetComponent<Item>());
			//	//Debug.Log ("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" + HouseInventoryCount + "/" + Inventory.Count);
		}
		StartCoroutine (RepositionGrid());
	}
	public void RemoveItem(Item item)
	{
		StartCoroutine (corRem (item));
	}
	IEnumerator corRem(Item item)
	{
		yield return new WaitForSeconds(0.1f);
		cash += item.price;
		if(item.quantity > 1)
		{
			removeTest++;
			Debug.Log ("Znalazłem: " + item.name + " na miejscu: " + item.quantity + "w PlayerData" + removeTest);
		//	if(removeTest > 1)
		//	{
				item.quantity--;
				item.bChecked =  false;
			yield return new WaitForSeconds(0.1f);
		//	}
		}
		else
		{
			////Debug.Log ("????" + Inventory.Find (i => i.name == item.name));
			Inventory.Remove ( Inventory.Find (i => i.name == item.name));
			//GameObject.Find ("HouseGrid/"+item.name + "h").SetActive (false);
			GameObject.Destroy (GameObject.Find ("HouseGrid/"+item.name + "i"));
			
			StartCoroutine (RepositionGrid());
		}
	}
	IEnumerator RepositionGrid()
	{
		yield return new WaitForSeconds(.1f);
		if(GameObject.Find ("Grid"))
		{
			GameObject.Find ("Grid").GetComponent<UIGrid>().Reposition ();
		}
		if(GameObject.Find ("HouseGrid"))
		{
			GameObject.Find ("HouseGrid").GetComponent<UIGrid>().Reposition ();
		}
		quantityTest = 0;
		removeTest = 0;
	}
	IEnumerator normalInv()
	{
		if(InventoryCount < Inventory.Count && GameObject.Find ("InventoryUI Root (3D)"))
		{
			if(!GameObject.Find ("Grid/"+Inventory.ElementAt (InventoryCount).name + "i"))
			{
				////Debug.Log ("Grid/"+Inventory.ElementAt (InventoryCount).name);
				//prevItem = GameObject.Find ("Item" + (InventoryCount-1).ToString ());
				newItem = Instantiate (itemPrefab, itemPrefab.transform.position, itemPrefab.transform.rotation) as GameObject;
				//newItem.AddComponent<Item>();
				newItem.AddComponent<Item>();
				CopyTo (Inventory.ElementAt (InventoryCount), newItem.GetComponent<Item>());
				if(InventoryParent == null)
				{
					InventoryParent = GameObject.Find ("Grid");
				}
				newItem.transform.parent = InventoryParent.transform;
				newItem.transform.name = newItem.GetComponent<Item>().name + "i";
				newItem.transform.localScale = new Vector3(1,1,1);
				InventoryParent.GetComponent<UIGrid>().Reposition ();
				//	newItem.transform.position = prevItem.transform.position - new Vector3(0,0.12f,0);
				/*foreach(Transform child in newItem.transform)
						{
							if(child.name == "NAME")
							{
								child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).name;
							}
							else if(child.name == "QLT")
							{
								child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).quality.ToString ();
							}
							else if(child.name == "WEIGHT")
							{
								child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).weight.ToString () + " KG";
							}
						}*/
				InventoryCount++;
				yield return new WaitForSeconds(0.1f);
			}
			/*else
			{
				Inventory.ElementAt (InventoryCount).quantity++;
				GameObject.Find ("Grid/"+Inventory.ElementAt (InventoryCount).name.Replace (" ", string.Empty) + "i").GetComponent<Item>().bChecked = false;
				if(InventoryCount < Inventory.Count)
				{
					InventoryCount++;
				}
				else
				{
					InventoryCount = Inventory.Count;
				}
				yield return new WaitForSeconds(0.1f);
			}*/
		}
	}
	IEnumerator houseInv()
	{
	//	//Debug.Log ("Ja sie w ogole odpalam?");
		if(HouseInventoryCount < Inventory.Count)
		{
			if(!GameObject.Find ("HouseGrid/"+Inventory.ElementAt (HouseInventoryCount).name + "i"))
			{
				newItem = Instantiate (itemPrefab, itemPrefab.transform.position, itemPrefab.transform.rotation) as GameObject;
				newItem.AddComponent<Item>();
				CopyTo (Inventory.ElementAt (HouseInventoryCount), newItem.GetComponent<Item>());
				//newItem.AddComponent<Item>();
				//newItem.GetComponent<Item>() = Inventory.ElementAt (HouseInventoryCount);
				if(HouseInventoryParent == null)
				{
					HouseInventoryParent = GameObject.Find ("HouseGrid");
				}
				newItem.transform.parent = HouseInventoryParent.transform;
				newItem.transform.name = newItem.GetComponent<Item>().name + "i";
				newItem.GetComponent<Item>().houseName = null;
				newItem.transform.localScale = new Vector3(1,1,1);
				HouseInventoryParent.GetComponent<UIGrid>().Reposition ();
				//newItem.transform.localPosition += new Vector3(-32,0,0);
				//	newItem.transform.position = prevItem.transform.position - new Vector3(0,0.12f,0);
				/*foreach(Transform child in newItem.transform)
						{
							if(child.name == "NAME")
							{
								child.GetComponent<UILabel>().text = Inventory.ElementAt (HouseInventoryCount).name;
							}
							else if(child.name == "QLT")
							{
								child.GetComponent<UILabel>().text = Inventory.ElementAt (HouseInventoryCount).quality.ToString ();
							}
							else if(child.name == "WEIGHT")
							{
								child.GetComponent<UILabel>().text = Inventory.ElementAt (HouseInventoryCount).weight.ToString () + " KG";
							}
						}*/
				HouseInventoryCount++;
				yield return new WaitForSeconds(0.1f);
			}
			/*else
			{
				//Inventory.ElementAt (HouseInventoryCount).quantity++;
			//	Inventory.ElementAt (HouseInventoryCount).bChecked = false;
				////Debug.Log ("HouseGrid/"+Inventory.ElementAt (HouseInventoryCount).name + "i");
				CopyTo (Inventory.ElementAt (HouseInventoryCount), GameObject.Find ("HouseGrid/"+Inventory.ElementAt (HouseInventoryCount).name + "i").GetComponent<Item>());
				GameObject.Find ("HouseGrid/"+Inventory.ElementAt (HouseInventoryCount).name + "i").GetComponent<Item>().bChecked = false;
				HouseInventoryCount++;
				yield return new WaitForSeconds(0.1f);
			}*/
		}
	}
	// Update is called once per frame
	void Update () {
		if(cashObject == null)
		{	
			if(Inventory.Find (i => i.name == "Bottlecaps"))
			{
				cashObject = Inventory.Find (i => i.name == "Bottlecaps");
			}
		}
		else
		{
			if(cashObject.quantity != cash )
			{
				cashObject.quantity = cash;
				cashObject.bChecked =false;
			}
			if(GameObject.Find("Bottlecapsi"))
			{
				if(GameObject.Find("Bottlecapsi").GetComponent<Item>().quantity != cash)
				{
					GameObject.Find("Bottlecapsi").GetComponent<Item>().quantity = cash;
					GameObject.Find("Bottlecapsi").GetComponent<Item>().bChecked = false;
				}
			}
		}
		if(InventoryCount != Inventory.Count && GameObject.Find ("InventoryUI Root (3D)"))
		{
			StartCoroutine(normalInv ());
		}
		if(HouseInventoryCount != Inventory.Count && GameObject.Find ("CityUI Root (3D)"))
		{
			StartCoroutine(houseInv ());
		}
		if(InventoryCount > Inventory.Count)
		{
			//InventoryCount = 0;
		}
		if(HouseInventoryCount > Inventory.Count)
		{
			//HouseInventoryCount = 0;
		}
		StartCoroutine (check());
		fTimePassed += Time.fixedDeltaTime;
	}
	IEnumerator check()
	{
		RaycastHit hit;
		Physics.Raycast(transform.position, -Vector3.up, out hit);
		if(centerChunkPos != hit.transform.GetComponent<ChunkData>().m_pos)
		{
			centerChunkPos = hit.transform.GetComponent<ChunkData>().m_pos;
			//Debug.Log ("ZMIENIAM" + centerChunkPos);
		}
//		//Debug.Log (chunks.Count);
		if(chunks.Count < 18 * (uncoverArea+0.5f) && chunks.Count != 0)
		{
		//	chunks.ForEach(checkChunk);
			StartCoroutine (test2());
			//chunks.ForEach(checkChunk);	
		}
		yield return new WaitForSeconds(.2f);
	}
	IEnumerator test2()
	{
		PerlinNoise m_surfacePerlin = new PerlinNoise(terrain.m_surfaceSeed);
		VoronoiNoise m_voronoi = new VoronoiNoise(terrain.m_surfaceSeed);
		//List<ChunkData> chunks2 = new List<ChunkData>(); 
		foreach(ChunkData chunk in chunks.ToArray ())
		{	
			for(int i = 0; i <= uncoverArea; i++)
			{
				//N
				if(!GameObject.Find ("Voxel Mesh " + (chunk.m_pos.x+(i*8)).ToString() + " " + (chunk.m_pos.y).ToString() + " " + (chunk.m_pos.z).ToString()))
				{
			//		PerlinNoise m_surfacePerlin = new PerlinNoise(terrain.m_surfaceSeed);
			//		VoronoiNoise m_voronoi = new VoronoiNoise(terrain.m_surfaceSeed);
					Vector3 pos = new Vector3(chunk.m_pos.x+8*i+1, chunk.m_pos.y+1, chunk.m_pos.z+1);
					m_voxelChunktemp = new VoxelChunk(pos, terrain.m_voxelWidth, terrain.m_voxelHeight, terrain.m_voxelLength, terrain.m_surfaceLevel);
					m_voxelChunktemp.CreateVoxels (m_surfacePerlin, m_voronoi);
					m_voxelChunktemp.CreateMesh (terrain.m_material);
				//	yield return new WaitForSeconds(.1f);	
					//chunk.bOnce = true;
				}
				//NW
				if(!GameObject.Find ("Voxel Mesh " + (chunk.m_pos.x+8*i).ToString() + " " + (chunk.m_pos.y).ToString() + " " + (chunk.m_pos.z + 8*i).ToString()))
				{
			//		PerlinNoise m_surfacePerlin = new PerlinNoise(terrain.m_surfaceSeed);
			//		VoronoiNoise m_voronoi = new VoronoiNoise(terrain.m_surfaceSeed);
					Vector3 pos = new Vector3(chunk.m_pos.x+8*i+1, chunk.m_pos.y+1, chunk.m_pos.z+8*i+1);
					m_voxelChunktemp = new VoxelChunk(pos, terrain.m_voxelWidth, terrain.m_voxelHeight, terrain.m_voxelLength, terrain.m_surfaceLevel);
					m_voxelChunktemp.CreateVoxels (m_surfacePerlin, m_voronoi);
					m_voxelChunktemp.CreateMesh (terrain.m_material);
				//	yield return new WaitForSeconds(.1f);
					//chunk.bOnce = true;
				}
				//NE
				if(!GameObject.Find ("Voxel Mesh " + (chunk.m_pos.x+8*i).ToString() + " " + (chunk.m_pos.y).ToString() + " " + (chunk.m_pos.z - 8*i).ToString()))
				{
			//		PerlinNoise m_surfacePerlin = new PerlinNoise(terrain.m_surfaceSeed);
			//		VoronoiNoise m_voronoi = new VoronoiNoise(terrain.m_surfaceSeed);
					Vector3 pos = new Vector3(chunk.m_pos.x+8*i+1, chunk.m_pos.y+1, chunk.m_pos.z-8*i+1);
					m_voxelChunktemp = new VoxelChunk(pos, terrain.m_voxelWidth, terrain.m_voxelHeight, terrain.m_voxelLength, terrain.m_surfaceLevel);
					m_voxelChunktemp.CreateVoxels (m_surfacePerlin, m_voronoi);
					m_voxelChunktemp.CreateMesh (terrain.m_material);
				//	yield return new WaitForSeconds(.1f);
					//chunk.bOnce = true;
				}
				//S
				if(!GameObject.Find ("Voxel Mesh " + (chunk.m_pos.x-8*i).ToString() + " " + (chunk.m_pos.y).ToString() + " " + (chunk.m_pos.z).ToString()))
				{
			//		PerlinNoise m_surfacePerlin = new PerlinNoise(terrain.m_surfaceSeed);
			//		VoronoiNoise m_voronoi = new VoronoiNoise(terrain.m_surfaceSeed);
					Vector3 pos = new Vector3(chunk.m_pos.x-8*i+1, chunk.m_pos.y+1, chunk.m_pos.z+1);
					m_voxelChunktemp = new VoxelChunk(pos, terrain.m_voxelWidth, terrain.m_voxelHeight, terrain.m_voxelLength, terrain.m_surfaceLevel);
					m_voxelChunktemp.CreateVoxels (m_surfacePerlin, m_voronoi);
					m_voxelChunktemp.CreateMesh (terrain.m_material);
				//	yield return new WaitForSeconds(.1f);
					//chunk.bOnce = true;
				}
				//SW
				if(!GameObject.Find ("Voxel Mesh " + (chunk.m_pos.x-8*i).ToString() + " " + (chunk.m_pos.y).ToString() + " " + (chunk.m_pos.z + 8*i).ToString()))
				{
			//		PerlinNoise m_surfacePerlin = new PerlinNoise(terrain.m_surfaceSeed);
			//		VoronoiNoise m_voronoi = new VoronoiNoise(terrain.m_surfaceSeed);
					Vector3 pos = new Vector3(chunk.m_pos.x-8*i+1, chunk.m_pos.y+1, chunk.m_pos.z+8*i+1);
					m_voxelChunktemp = new VoxelChunk(pos, terrain.m_voxelWidth, terrain.m_voxelHeight, terrain.m_voxelLength, terrain.m_surfaceLevel);
					m_voxelChunktemp.CreateVoxels (m_surfacePerlin, m_voronoi);
					m_voxelChunktemp.CreateMesh (terrain.m_material);
				//	yield return new WaitForSeconds(.1f);
					//chunk.bOnce = true;
				}
				//SE
				if(!GameObject.Find ("Voxel Mesh " + (chunk.m_pos.x-8*i).ToString() + " " + (chunk.m_pos.y).ToString() + " " + (chunk.m_pos.z - 8*i).ToString()))
				{
			//		PerlinNoise m_surfacePerlin = new PerlinNoise(terrain.m_surfaceSeed);
			//		VoronoiNoise m_voronoi = new VoronoiNoise(terrain.m_surfaceSeed);
					Vector3 pos = new Vector3(chunk.m_pos.x-8*i+1, chunk.m_pos.y+1, chunk.m_pos.z-8*i+1);
					m_voxelChunktemp = new VoxelChunk(pos, terrain.m_voxelWidth, terrain.m_voxelHeight, terrain.m_voxelLength, terrain.m_surfaceLevel);
					m_voxelChunktemp.CreateVoxels (m_surfacePerlin, m_voronoi);
					m_voxelChunktemp.CreateMesh (terrain.m_material);
				//	yield return new WaitForSeconds(.1f);
					//chunk.bOnce = true;
				}
				//W
				if(!GameObject.Find ("Voxel Mesh " + (chunk.m_pos.x).ToString() + " " + (chunk.m_pos.y).ToString() + " " + (chunk.m_pos.z + 8*i).ToString()))
				{
			//		PerlinNoise m_surfacePerlin = new PerlinNoise(terrain.m_surfaceSeed);
			//		VoronoiNoise m_voronoi = new VoronoiNoise(terrain.m_surfaceSeed);
					Vector3 pos = new Vector3(chunk.m_pos.x+1, chunk.m_pos.y+1, chunk.m_pos.z+8*i+1);
					m_voxelChunktemp = new VoxelChunk(pos, terrain.m_voxelWidth, terrain.m_voxelHeight, terrain.m_voxelLength, terrain.m_surfaceLevel);
					m_voxelChunktemp.CreateVoxels (m_surfacePerlin, m_voronoi);
					m_voxelChunktemp.CreateMesh (terrain.m_material);
				//	yield return new WaitForSeconds(.1f);
					//chunk.bOnce = true;
				}
				//E
				if(!GameObject.Find ("Voxel Mesh " + (chunk.m_pos.x).ToString() + " " + (chunk.m_pos.y).ToString() + " " + (chunk.m_pos.z - 8*i).ToString()))
				{
			//		PerlinNoise m_surfacePerlin = new PerlinNoise(terrain.m_surfaceSeed);
			//		VoronoiNoise m_voronoi = new VoronoiNoise(terrain.m_surfaceSeed);
					Vector3 pos = new Vector3(chunk.m_pos.x+1, chunk.m_pos.y+1, chunk.m_pos.z-8*i+1);
					m_voxelChunktemp = new VoxelChunk(pos, terrain.m_voxelWidth, terrain.m_voxelHeight, terrain.m_voxelLength, terrain.m_surfaceLevel);
					m_voxelChunktemp.CreateVoxels (m_surfacePerlin, m_voronoi);
					m_voxelChunktemp.CreateMesh (terrain.m_material);
				//	yield return new WaitForSeconds(.1f);
					//chunk.bOnce = true;
				}
				yield return new WaitForSeconds(0.001f);
			}
			chunk.bUsed = true;
			//chunks2.Add (chunk); 
			chunks.Remove (chunk);
			yield return new WaitForSeconds(0.001f);
		}
		//chunks = chunks.Except (chunks2).ToList ();
		//chunks2.Clear ();
		//yield return new WaitForSeconds(.2f);	
	}
}
