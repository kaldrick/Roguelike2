using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Linq;
using Boomlagoon.JSON;
using System.Text;

public class SaveSceneVars
{
	public List<Vector3> savedPos = new List<Vector3>();
}
public class SaveSceneComponent : MonoBehaviour
{
	public List<Vector3> pos = new List<Vector3>();
	public List<Vector3> savedPos = new List<Vector3>();
	public List<Vector3> treesPos = new List<Vector3>();
	public List<Vector3> savedTreesPos = new List<Vector3>();
	public List<Vector3> treesScale = new List<Vector3>();
	public List<Vector3> savedTreesScale = new List<Vector3>();
	public List<Vector3> treesRotation = new List<Vector3>();
	public List<Vector3> savedTreesRotation = new List<Vector3>();
	public List<Vector3> chatkaPos = new List<Vector3>();
	public List<Vector3> savedChatkaPos = new List<Vector3>();
	public List<Vector3> chatkaRotation = new List<Vector3>();
	public List<Vector3> savedChatkaRotation = new List<Vector3>();
	public int m_Width = 8;
	public int m_Height = 38;
	public int m_Length = 8;
	public float surfaceLevel = 0;
	public VoxelChunk m_voxelChunk;
	public GameObject player;
	public PerlinNoise m_surfacePerlin;
	public VoronoiNoise m_voronoi;
	public int m_surfaceSeed;
	public Vector3 playerPos, playerVel, playerRot;
	public GameObject tree, parent, chatka;
	public int index = 0;
	public int indexC = 0;
	public string stringTest;
	public StringBuilder builder = new StringBuilder();
	public JSONObject obj = new JSONObject();
	public int indexJson = 0;
 	void OnGUI()
    {
        if (GUI.Button(new Rect(20, 100, 100, 30), "Save"))
        {
			savedChatkaRotation.Clear ();
			savedTreesRotation.Clear ();
			savedTreesScale.Clear ();
//			m_voxels.ForEach (checkChunk);
			pos.ForEach (savePos);
			treesPos.ForEach (saveTreePos);
			treesScale.ForEach (saveTreeScale);
			treesRotation.ForEach (saveTreeRotation);
			chatkaPos.ForEach (saveChatkaPos);
			chatkaRotation.ForEach (saveChatkaRotation);
			
			PlayerPrefsX.SetVector3Array("TreesScale", savedTreesScale.ToArray());
			PlayerPrefsX.SetVector3Array("TreesRotation", savedTreesRotation.ToArray());
			PlayerPrefsX.SetVector3Array("Testyy", savedPos.ToArray ());
			PlayerPrefsX.SetVector3Array ("TreesPos", savedTreesPos.ToArray ());
			PlayerPrefsX.SetVector3Array("ChatkaRotation", savedChatkaRotation.ToArray());
			PlayerPrefsX.SetVector3Array ("ChatkaPos", savedChatkaPos.ToArray ());
			PlayerPrefs.SetInt ("SurfaceSeed", m_surfaceSeed);
			player = GameObject.Find ("PlayerWorld");
			playerPos = player.transform.localPosition;
			playerVel = player.rigidbody.velocity;
			playerRot = player.transform.localEulerAngles;
			PlayerPrefsX.SetVector3 ("PlayerPos", playerPos);
			PlayerPrefsX.SetVector3 ("PlayerVel", playerVel);
			PlayerPrefsX.SetVector3 ("PlayerRot", playerRot);
			stringTest = builder.ToString ();
			indexJson = 0;
			//PlayerPrefsArray.SetVector3Array("Test", testerer1);
        }

        if (GUI.Button(new Rect(20, 140, 100, 30), "Load"))
        {
		//	index = 0;
			m_surfaceSeed = PlayerPrefs.GetInt ("SurfaceSeed");
			deleteChunk();
			savedTreesPos.Clear ();
			savedPos.Clear ();
			savedTreesPos = PlayerPrefsX.GetVector3Array("TreesPos").ToList ();
			savedPos = PlayerPrefsX.GetVector3Array("Testyy").ToList ();
			savedTreesRotation.Clear ();
			savedTreesScale.Clear ();
			savedTreesRotation = PlayerPrefsX.GetVector3Array ("TreesRotation").ToList ();
			savedTreesScale = PlayerPrefsX.GetVector3Array ("TreesScale").ToList();
			savedChatkaRotation.Clear ();
			savedChatkaPos.Clear ();
			savedChatkaRotation = PlayerPrefsX.GetVector3Array ("ChatkaRotation").ToList ();
			savedChatkaPos = PlayerPrefsX.GetVector3Array ("ChatkaPos").ToList();
			//savedPos.Add (testerer);
			savedPos.ForEach(loadVoxels);
			//loadTrees ();
			player = GameObject.Find ("PlayerWorld");
			player.transform.localPosition = PlayerPrefsX.GetVector3 ("PlayerPos");
			player.rigidbody.velocity = PlayerPrefsX.GetVector3 ("PlayerVel");
			player.transform.localEulerAngles = PlayerPrefsX.GetVector3 ("PlayerRot");
			GameObject.Find ("TerrainGenerator").GetComponent<GenerateTerrain>().m_surfaceSeed = m_surfaceSeed;
			savedTreesPos.ForEach (loadTrees);
			savedChatkaPos.ForEach (loadChatki);
			index = 0;
			indexC = 0;
        }
		
        if (GUI.Button(new Rect(20, 180, 100, 30), "Restart Scene"))
        {
            Application.LoadLevel(Application.loadedLevelName);
        }
    }
	
	
	void savePos(Vector3 pos)
	{
		if(!savedPos.Contains (pos))
		{
			savedPos.Add (pos);	
			builder.Append (pos.x).Append (" ").Append (pos.y).Append (" ").Append (pos.z).Append (" | ");
			obj = new JSONObject{
				{"VectorX" + indexJson, pos.x}	
			};
		}
	}
	void saveTreePos(Vector3 pos)
	{
		if(!savedTreesPos.Contains (pos))
		{
			savedTreesPos.Add (pos);	
		}
	}
	void saveTreeScale(Vector3 pos)
	{
		savedTreesScale.Add (pos);	
	}
	void saveTreeRotation(Vector3 pos)
	{
		savedTreesRotation.Add (pos);	
	}
	void saveChatkaPos(Vector3 pos)
	{
		if(!savedChatkaPos.Contains (pos))
		{
			savedChatkaPos.Add (pos);	
		}
	}
	void saveChatkaRotation(Vector3 pos)
	{
		savedChatkaRotation.Add (pos);	
	}
	void deleteChunk()
	{
		//Destroy(GameObject.Find (m_voxelName));
		foreach(Transform test in GameObject.Find ("Terrain").transform)
		{ 
			Destroy(test.gameObject);
		}
		foreach (Transform testT in GameObject.Find ("TreeHolder").transform)
		{
			Destroy (testT.gameObject);
		}
		foreach (Transform testC in GameObject.Find ("Chatki").transform)
		{
			Destroy (testC.gameObject);
		}
	}
	void loadVoxels(Vector3 pos)
	{
		pos.x++;
		pos.y++;
		pos.z++;
				//Set some varibles for the marching cubes plugin
		MarchingCubes.SetTarget(0.0f);
		MarchingCubes.SetWindingOrder(2, 1, 0);
		MarchingCubes.SetModeToCubes();
		m_surfacePerlin = new PerlinNoise(m_surfaceSeed);
		m_voronoi = new VoronoiNoise(m_surfaceSeed);
		m_voxelChunk = new VoxelChunk(pos, m_Width, m_Height, m_Length, surfaceLevel);
		m_voxelChunk.bDodane = true;
		
		m_voxelChunk.CreateVoxels(m_surfacePerlin, m_voronoi);//, m_cavePerlin);
		m_voxelChunk.CreateMesh (GameObject.Find ("TerrainGenerator").GetComponent<GenerateTerrain>().m_material);
	}
	void loadTrees(Vector3 pos)
	{
		GameObject lTree = GameObject.Instantiate (tree, pos, tree.transform.rotation) as GameObject;
		lTree.transform.localEulerAngles = savedTreesRotation.ElementAt(index);
		lTree.transform.localScale = savedTreesScale.ElementAt (index);
		lTree.transform.parent = GameObject.Find ("TreeHolder").transform;
		lTree.GetComponent<TreeScale>().bCheckGround = false;
	//	lTree.GetComponent<TreeScale>().bLoaded = true;
		lTree.renderer.enabled = true;
		lTree.name += index;
		MakeParents();
		CheckIndex();
		index++;
	}
	void loadChatki(Vector3 pos)
	{
		GameObject lChatka = GameObject.Instantiate (chatka, pos, chatka.transform.rotation) as GameObject;
		//lChatka.transform.localEulerAngles = savedChatkaRotation.ElementAt(indexC);
		//lChatka.transform.localEulerAngles = new Vector3(lChatka.transform.localEulerAngles.z, lChatka.transform.localEulerAngles.y, lChatka.transform.localEulerAngles.x);
	//	lTree.transform.localScale = savedTreesScale.ElementAt (index);
		lChatka.transform.parent = GameObject.Find ("Chatki").transform;
		lChatka.GetComponent<ChatkaScale>().bCheckGround = false;
	//	lTree.GetComponent<TreeScale>().bLoaded = true;
		lChatka.renderer.enabled = true;
		lChatka.name += index;
		//MakeParents();
		//CheckIndex();
		indexC++;
	}
	void MakeParents()
	{
		if(index%8 == 0 && index != 0)
		{
			GameObject lParent = GameObject.Instantiate (parent) as GameObject;
			lParent.name += index;
			lParent.transform.parent = GameObject.Find ("TreeHolder").transform;
		}
	}
	void CheckIndex()
	{
		if(index%8 == 0 && index != 0)
		{
			for(int i = index-8; i<= index; i++)
			{
				Transform test = GameObject.Find ("Tree 1(Clone)"+i).transform;
				Debug.Log ("Ma juz parenta? " + test.parent);
				test.parent = null;
				test.parent = GameObject.Find ("Parent(Clone)" + index).transform;
			//	test.transform.SetParent (GameObject.Find ("Parent(Clone)" + index));
				test.renderer.enabled = true;
			//	Debug.Log ("Hulam " + test + " / " + test.transform.parent + " / " + index);
				//test.transform.parent = GameObject.Find ("Parent(Clone)" + index).transform;
			}
		}	
	}
}
