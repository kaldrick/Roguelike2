using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class PlayerData : MonoBehaviour {
	public Vector3 centerChunkPos;
	public List<ChunkData> chunks;
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
	public GameObject[] drzewka;
	//public System.Predicate<ChunkData> chunkPredicate = new System.Predicate<ChunkData>(checkChunk);
	// Use this for initialization
	void Start () {
		terrain = GameObject.Find ("TerrainGenerator").GetComponent<GenerateTerrain>();
	}
	void checkChunk(ChunkData chunk)
	{
		/*for(int i = 0; i <= uncoverArea; i++)
		{
			//N
			if(!GameObject.Find ("Voxel Mesh " + (chunk.m_pos.x+(i*8)).ToString() + " " + (chunk.m_pos.y).ToString() + " " + (chunk.m_pos.z).ToString()))
			{
				PerlinNoise m_surfacePerlin = new PerlinNoise(terrain.m_surfaceSeed);
				VoronoiNoise m_voronoi = new VoronoiNoise(terrain.m_surfaceSeed);
				Vector3 pos = new Vector3(chunk.m_pos.x+8*i+1, chunk.m_pos.y+1, chunk.m_pos.z+1);
				m_voxelChunktemp = new VoxelChunk(pos, terrain.m_voxelWidth, terrain.m_voxelHeight, terrain.m_voxelLength, terrain.m_surfaceLevel);
				m_voxelChunktemp.CreateVoxels (m_surfacePerlin, m_voronoi);
				m_voxelChunktemp.CreateMesh (terrain.m_material);
				//chunk.bOnce = true;
			}
			//NW
			if(!GameObject.Find ("Voxel Mesh " + (chunk.m_pos.x+8*i).ToString() + " " + (chunk.m_pos.y).ToString() + " " + (chunk.m_pos.z + 8*i).ToString()))
			{
				PerlinNoise m_surfacePerlin = new PerlinNoise(terrain.m_surfaceSeed);
				VoronoiNoise m_voronoi = new VoronoiNoise(terrain.m_surfaceSeed);
				Vector3 pos = new Vector3(chunk.m_pos.x+8*i+1, chunk.m_pos.y+1, chunk.m_pos.z+8*i+1);
				m_voxelChunktemp = new VoxelChunk(pos, terrain.m_voxelWidth, terrain.m_voxelHeight, terrain.m_voxelLength, terrain.m_surfaceLevel);
				m_voxelChunktemp.CreateVoxels (m_surfacePerlin, m_voronoi);
				m_voxelChunktemp.CreateMesh (terrain.m_material);
				//chunk.bOnce = true;
			}
			//NE
			if(!GameObject.Find ("Voxel Mesh " + (chunk.m_pos.x+8*i).ToString() + " " + (chunk.m_pos.y).ToString() + " " + (chunk.m_pos.z - 8*i).ToString()))
			{
				PerlinNoise m_surfacePerlin = new PerlinNoise(terrain.m_surfaceSeed);
				VoronoiNoise m_voronoi = new VoronoiNoise(terrain.m_surfaceSeed);
				Vector3 pos = new Vector3(chunk.m_pos.x+8*i+1, chunk.m_pos.y+1, chunk.m_pos.z-8*i+1);
				m_voxelChunktemp = new VoxelChunk(pos, terrain.m_voxelWidth, terrain.m_voxelHeight, terrain.m_voxelLength, terrain.m_surfaceLevel);
				m_voxelChunktemp.CreateVoxels (m_surfacePerlin, m_voronoi);
				m_voxelChunktemp.CreateMesh (terrain.m_material);
				//chunk.bOnce = true;
			}
			//S
			if(!GameObject.Find ("Voxel Mesh " + (chunk.m_pos.x-8*i).ToString() + " " + (chunk.m_pos.y).ToString() + " " + (chunk.m_pos.z).ToString()))
			{
				PerlinNoise m_surfacePerlin = new PerlinNoise(terrain.m_surfaceSeed);
				VoronoiNoise m_voronoi = new VoronoiNoise(terrain.m_surfaceSeed);
				Vector3 pos = new Vector3(chunk.m_pos.x-8*i+1, chunk.m_pos.y+1, chunk.m_pos.z+1);
				m_voxelChunktemp = new VoxelChunk(pos, terrain.m_voxelWidth, terrain.m_voxelHeight, terrain.m_voxelLength, terrain.m_surfaceLevel);
				m_voxelChunktemp.CreateVoxels (m_surfacePerlin, m_voronoi);
				m_voxelChunktemp.CreateMesh (terrain.m_material);
				//chunk.bOnce = true;
			}
			//SW
			if(!GameObject.Find ("Voxel Mesh " + (chunk.m_pos.x-8*i).ToString() + " " + (chunk.m_pos.y).ToString() + " " + (chunk.m_pos.z + 8*i).ToString()))
			{
				PerlinNoise m_surfacePerlin = new PerlinNoise(terrain.m_surfaceSeed);
				VoronoiNoise m_voronoi = new VoronoiNoise(terrain.m_surfaceSeed);
				Vector3 pos = new Vector3(chunk.m_pos.x-8*i+1, chunk.m_pos.y+1, chunk.m_pos.z+8*i+1);
				m_voxelChunktemp = new VoxelChunk(pos, terrain.m_voxelWidth, terrain.m_voxelHeight, terrain.m_voxelLength, terrain.m_surfaceLevel);
				m_voxelChunktemp.CreateVoxels (m_surfacePerlin, m_voronoi);
				m_voxelChunktemp.CreateMesh (terrain.m_material);
				//chunk.bOnce = true;
			}
			//SE
			if(!GameObject.Find ("Voxel Mesh " + (chunk.m_pos.x-8*i).ToString() + " " + (chunk.m_pos.y).ToString() + " " + (chunk.m_pos.z - 8*i).ToString()))
			{
				PerlinNoise m_surfacePerlin = new PerlinNoise(terrain.m_surfaceSeed);
				VoronoiNoise m_voronoi = new VoronoiNoise(terrain.m_surfaceSeed);
				Vector3 pos = new Vector3(chunk.m_pos.x-8*i+1, chunk.m_pos.y+1, chunk.m_pos.z-8*i+1);
				m_voxelChunktemp = new VoxelChunk(pos, terrain.m_voxelWidth, terrain.m_voxelHeight, terrain.m_voxelLength, terrain.m_surfaceLevel);
				m_voxelChunktemp.CreateVoxels (m_surfacePerlin, m_voronoi);
				m_voxelChunktemp.CreateMesh (terrain.m_material);
				//chunk.bOnce = true;
			}
			//W
			if(!GameObject.Find ("Voxel Mesh " + (chunk.m_pos.x).ToString() + " " + (chunk.m_pos.y).ToString() + " " + (chunk.m_pos.z + 8*i).ToString()))
			{
				PerlinNoise m_surfacePerlin = new PerlinNoise(terrain.m_surfaceSeed);
				VoronoiNoise m_voronoi = new VoronoiNoise(terrain.m_surfaceSeed);
				Vector3 pos = new Vector3(chunk.m_pos.x+1, chunk.m_pos.y+1, chunk.m_pos.z+8*i+1);
				m_voxelChunktemp = new VoxelChunk(pos, terrain.m_voxelWidth, terrain.m_voxelHeight, terrain.m_voxelLength, terrain.m_surfaceLevel);
				m_voxelChunktemp.CreateVoxels (m_surfacePerlin, m_voronoi);
				m_voxelChunktemp.CreateMesh (terrain.m_material);
				//chunk.bOnce = true;
			}
			//E
			if(!GameObject.Find ("Voxel Mesh " + (chunk.m_pos.x).ToString() + " " + (chunk.m_pos.y).ToString() + " " + (chunk.m_pos.z - 8*i).ToString()))
			{
				PerlinNoise m_surfacePerlin = new PerlinNoise(terrain.m_surfaceSeed);
				VoronoiNoise m_voronoi = new VoronoiNoise(terrain.m_surfaceSeed);
				Vector3 pos = new Vector3(chunk.m_pos.x+1, chunk.m_pos.y+1, chunk.m_pos.z-8*i+1);
				m_voxelChunktemp = new VoxelChunk(pos, terrain.m_voxelWidth, terrain.m_voxelHeight, terrain.m_voxelLength, terrain.m_surfaceLevel);
				m_voxelChunktemp.CreateVoxels (m_surfacePerlin, m_voronoi);
				m_voxelChunktemp.CreateMesh (terrain.m_material);
				//chunk.bOnce = true;
			}
		}
		chunk.bUsed = true;
		chunks.Remove (chunk);
		//StartCoroutine (test(chunk));*/
		StartCoroutine (test(chunk));
	}
	IEnumerator test(ChunkData chunk)
	{
		PerlinNoise m_surfacePerlin = new PerlinNoise(terrain.m_surfaceSeed);
		VoronoiNoise m_voronoi = new VoronoiNoise(terrain.m_surfaceSeed);
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
				yield return new WaitForSeconds(.1f);	
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
				yield return new WaitForSeconds(.1f);
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
				yield return new WaitForSeconds(.1f);
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
				yield return new WaitForSeconds(.1f);
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
				yield return new WaitForSeconds(.1f);
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
				yield return new WaitForSeconds(.1f);
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
				yield return new WaitForSeconds(.1f);
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
				yield return new WaitForSeconds(.1f);
				//chunk.bOnce = true;
			}
		}
		chunk.bUsed = true;
		chunks.Remove (chunk);
		yield return new WaitForSeconds(.2f);	
	}
	// Update is called once per frame
	void Update () {
		StartCoroutine (check());
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
			chunks.ForEach(checkChunk);
			//chunks.ForEach(checkChunk);	
		}
		yield return new WaitForSeconds(.2f);
	}
}
