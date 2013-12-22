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
	public CombineChildren[] combiner;
	public float fTimePassed = 0f;
	public GameObject[] drzewka;
	public List<Item> Inventory = new List<Item>();
	public int InventoryCount = 0;
	public GameObject prevItem;
	public GameObject InventoryParent;
	public GameObject newItem;
	//public System.Predicate<ChunkData> chunkPredicate = new System.Predicate<ChunkData>(checkChunk);
	// Use this for initialization
	void Start () {
		terrain = GameObject.Find ("TerrainGenerator").GetComponent<GenerateTerrain>();
		InventoryParent = GameObject.Find ("Grid");
		Inventory.Add (new Item("Iron", 15, 120));
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
		Inventory.Add (new Item("Steel", 10, 140));
	}
	// Update is called once per frame
	void Update () {
		if(InventoryCount != Inventory.Count && GameObject.Find ("InventoryUI Root (3D)"))
		{
			if(GameObject.Find ("Item" + (InventoryCount).ToString ()) == null)
			{
				prevItem = GameObject.Find ("Item" + (InventoryCount-1).ToString ());
				newItem = Instantiate (prevItem, prevItem.transform.position, prevItem.transform.rotation) as GameObject;
				if(InventoryParent == null)
				{
					InventoryParent = GameObject.Find ("Grid");
				}
				newItem.transform.parent = InventoryParent.transform;
				newItem.transform.name = "Item" + (InventoryCount).ToString ();
				newItem.transform.localScale = new Vector3(1,1,1);
				InventoryParent.GetComponent<UIGrid>().Reposition ();
			//	newItem.transform.position = prevItem.transform.position - new Vector3(0,0.12f,0);
				foreach(Transform child in newItem.transform)
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
				}
			}
			else
			{
				if(InventoryCount == 0)
				{
					prevItem = GameObject.Find ("Item0");
					foreach(Transform child in prevItem.transform)
					{
						if(child.name == "NAME")
						{
							child.GetComponent<UILabel>().text = Inventory.ElementAt (0).name;
						}
						else if(child.name == "QLT")
						{
							child.GetComponent<UILabel>().text = Inventory.ElementAt (0).quality.ToString ();
						}
						else if(child.name == "WEIGHT")
						{
							child.GetComponent<UILabel>().text = Inventory.ElementAt (0).weight.ToString () + " KG";
						}
					}
				}
				InventoryCount++;
				return;
			}
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
			Debug.Log ("ZMIENIAM" + centerChunkPos);
		}
//		Debug.Log (chunks.Count);
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
