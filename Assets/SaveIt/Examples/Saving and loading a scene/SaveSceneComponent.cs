using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Linq;
using System.Text;
using WyrmTale;
using SaveIt;
public class SaveSceneVars
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
	public List<Vector3> testSerialization = new List<Vector3>();
	public List<TooltipData> tooltipData = new List<TooltipData>();
	public int m_surfaceSeed;
	public Vector3 playerPos, playerVel, playerRot;
	public float fTimePassed;
}
public class SaveSceneComponent : MonoBehaviour
{
	public int m_Width = 8;
	public int m_Height = 38;
	public int m_Length = 8;
	public float surfaceLevel = 0;
	public VoxelChunk m_voxelChunk;
	public GameObject player;
	public PerlinNoise m_surfacePerlin;
	public VoronoiNoise m_voronoi;
	public GameObject tree, parent, chatka;
	public int index = 0;
	public int cindex = 0;
	public SaveSceneVars save = new SaveSceneVars();
	public SaveSceneVars load = new SaveSceneVars();
	public string path = @"C:\";
	public string saveName = "Test.dat";
	public JSON js = new JSON();
	public SaveContext context;
	public LoadContext loadCon;
 	void OnGUI()
    {
        if (GUI.Button(new Rect(20, 100, 100, 30), "Save"))
        {
			save.savedChatkaRotation.Clear ();
			save.savedTreesRotation.Clear ();
			save.savedTreesScale.Clear ();
			if( Application.platform == RuntimePlatform.OSXWebPlayer || Application.platform == RuntimePlatform.WindowsWebPlayer )
			{
				context = SaveContext.ToPlayerPrefs (saveName);
			}
			else
			{
				context = SaveContext.ToFile (path+saveName);
			}
//			m_voxels.ForEach (checkChunk);
			save.pos.ForEach (savePos);
			save.treesPos.ForEach (saveTreePos);
			save.treesScale.ForEach (saveTreeScale);
			save.treesRotation.ForEach (saveTreeRotation);
			save.chatkaPos.ForEach (saveChatkaPos);
			save.chatkaRotation.ForEach (saveChatkaRotation);
			//PlayerPrefsX.SetVector3Array("TreesScale", savedTreesScale.ToArray());
			//PlayerPrefsX.SetVector3Array("TreesRotation", savedTreesRotation.ToArray());
			//PlayerPrefsX.SetVector3Array("Testyy", savedPos.ToArray ());
			//PlayerPrefsX.SetVector3Array ("TreesPos", savedTreesPos.ToArray ());
			//PlayerPrefsX.SetVector3Array("ChatkaRotation", savedChatkaRotation.ToArray());
			//PlayerPrefsX.SetVector3Array ("ChatkaPos", savedChatkaPos.ToArray ());
			//PlayerPrefs.SetInt ("SurfaceSeed", m_surfaceSeed);
			player = GameObject.Find ("PlayerWorld");
			save.playerPos = player.transform.localPosition;
			save.playerVel = player.rigidbody.velocity;
			save.playerRot = player.transform.localEulerAngles;
			save.fTimePassed = player.GetComponent<PlayerData>().fTimePassed;
			//PlayerPrefsX.SetVector3 ("PlayerPos", playerPos);
			//PlayerPrefsX.SetVector3 ("PlayerVel", playerVel);
			//PlayerPrefsX.SetVector3 ("PlayerRot", playerRot);
			//stringTest = builder.ToString ();
			//indexJson = 0;
			//save.savedPos = savedPos;	
			context.Save (save.savedTreesScale, "savedTreesScale");
			context.Save (save.savedTreesRotation, "savedTreesRotation");
			context.Save (save.savedPos, "savedPos");
			context.Save (save.savedTreesPos, "savedTreesPos");
			context.Save (save.savedChatkaRotation, "savedChatkaRotation");
			context.Save (save.savedChatkaPos, "savedChatkaPos");
			context.Save (save.playerPos, "playerPos");
			context.Save (save.playerVel, "playerVel");
			context.Save (save.playerRot, "playerRot");
			context.Save (save.m_surfaceSeed, "m_surfaceSeed");
			context.Save (save.fTimePassed, "fTimePassed");
			context.Flush();
			//PlayerPrefsArray.SetVector3Array("Test", testerer1);
        }

        if (GUI.Button(new Rect(20, 140, 100, 30), "Load"))
        {
			save.savedTreesPos.Clear ();
			save.savedPos.Clear ();
			//savedTreesPos = PlayerPrefsX.GetVector3Array("TreesPos").ToList ();
			//savedPos = PlayerPrefsX.GetVector3Array("Testyy").ToList ();
			save.savedTreesRotation.Clear ();
			save.savedTreesScale.Clear ();
			//savedTreesRotation = PlayerPrefsX.GetVector3Array ("TreesRotation").ToList ();
			//savedTreesScale = PlayerPrefsX.GetVector3Array ("TreesScale").ToList();
			save.savedChatkaRotation.Clear ();
			save.savedChatkaPos.Clear ();
			//LoadContext loadCon = LoadContext.FromFile("Test.dat");
			if( Application.platform == RuntimePlatform.OSXWebPlayer || Application.platform == RuntimePlatform.WindowsWebPlayer )
			{
				loadCon = LoadContext.FromPlayerPrefs (saveName);
			}
			else
			{
				loadCon = LoadContext.FromFile (path+saveName);
			}
			save.testSerialization = loadCon.Load<List<Vector3>>("savedPos");
			save.savedTreesScale = loadCon.Load<List<Vector3>>("savedTreesScale");
			save.savedTreesRotation = loadCon.Load<List<Vector3>>("savedTreesRotation");
			save.savedPos = loadCon.Load<List<Vector3>>("savedPos");
			save.savedTreesPos = loadCon.Load<List<Vector3>>("savedTreesPos");
			save.savedChatkaRotation = loadCon.Load<List<Vector3>>("savedChatkaRotation");
			save.savedChatkaPos = loadCon.Load<List<Vector3>>("savedChatkaPos");
			save.playerPos = loadCon.Load<Vector3>("playerPos");
			save.playerVel = loadCon.Load<Vector3>("playerVel");
			save.playerRot = loadCon.Load<Vector3>("playerRot");
			save.m_surfaceSeed = loadCon.Load<int>("m_surfaceSeed");
		//	index = 0;
			//save.m_surfaceSeed = PlayerPrefs.GetInt ("SurfaceSeed");
			StartCoroutine(deleteChunk());
			//savedChatkaRotation = PlayerPrefsX.GetVector3Array ("ChatkaRotation").ToList ();
			//savedChatkaPos = PlayerPrefsX.GetVector3Array ("ChatkaPos").ToList();
			//savedPos.Add (testerer);
			save.savedPos.ForEach(loadVoxels);
			//loadTrees ();
			player = GameObject.Find ("PlayerWorld");
			player.transform.localPosition = save.playerPos;
			player.rigidbody.velocity = save.playerVel;
			player.transform.localEulerAngles = save.playerRot;
			player.GetComponent<PlayerData>().fTimePassed = save.fTimePassed;
			GameObject.Find ("TerrainGenerator").GetComponent<GenerateTerrain>().m_surfaceSeed = save.m_surfaceSeed;
			//save.savedTreesPos.ForEach (loadTrees2);
			
			StartCoroutine (loadTrees2 ());
			save.savedChatkaPos.ForEach (loadChatki);
			index = 0;
			cindex = 0;
			
        }
		
        if (GUI.Button(new Rect(20, 180, 100, 30), "Restart Scene"))
        {
            Application.LoadLevel(Application.loadedLevelName);
        }
    }
	
	
	void savePos(Vector3 pos)
	{
		if(!save.savedPos.Contains (pos))
		{
			save.savedPos.Add (pos);	
		}
	}
	void saveTreePos(Vector3 pos)
	{
		if(!save.savedTreesPos.Contains (pos))
		{
			save.savedTreesPos.Add (pos);	
		}
	}
	void saveTreeScale(Vector3 pos)
	{
		save.savedTreesScale.Add (pos);	
	}
	void saveTreeRotation(Vector3 pos)
	{
		save.savedTreesRotation.Add (pos);	
	}
	void saveChatkaPos(Vector3 pos)
	{
		if(!save.savedChatkaPos.Contains (pos))
		{
			save.savedChatkaPos.Add (pos);	
		}
	}
	void saveChatkaRotation(Vector3 pos)
	{
		save.savedChatkaRotation.Add (pos);	
	}
	IEnumerator deleteChunk()
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
		yield return new WaitForFixedUpdate();
	}
	void loadVoxels(Vector3 pos)
	{
		pos.x++;
		pos.y++;
		pos.z++;
				//Set some varibles for the marching cubes plugin
		StartCoroutine (lV (pos));
	}
	IEnumerator lV(Vector3 pos)
	{
		MarchingCubes.SetTarget(0.0f);
		MarchingCubes.SetWindingOrder(2, 1, 0);
		MarchingCubes.SetModeToCubes();
		m_surfacePerlin = new PerlinNoise(save.m_surfaceSeed);
		m_voronoi = new VoronoiNoise(save.m_surfaceSeed);
		m_voxelChunk = new VoxelChunk(pos, m_Width, m_Height, m_Length, surfaceLevel);
		m_voxelChunk.bDodane = true;
		
		m_voxelChunk.CreateVoxels(m_surfacePerlin, m_voronoi);//, m_cavePerlin);
		m_voxelChunk.CreateMesh (GameObject.Find ("TerrainGenerator").GetComponent<GenerateTerrain>().m_material);
		yield return new WaitForFixedUpdate();
	}
	void loadTrees(Vector3 pos)
	{
		GameObject lTree = GameObject.Instantiate (tree, pos, tree.transform.rotation) as GameObject;
		lTree.transform.localEulerAngles = save.savedTreesRotation.ElementAt(index);
		lTree.transform.localScale = save.savedTreesScale.ElementAt (index);
		lTree.transform.parent = GameObject.Find ("TreeHolder").transform;
		//lTree.GetComponent<TreeScale>().bCheckGround = false;
	//	lTree.GetComponent<TreeScale>().bLoaded = true;
		lTree.renderer.enabled = true;
		lTree.name += index;
		MakeParents();
		CheckIndex();
		index++;
	}
	IEnumerator loadTrees2()
	{
		foreach(Vector3 pos in save.savedTreesPos.AsEnumerable().Reverse ())
		{
			GameObject lTree = GameObject.Instantiate (tree, pos, tree.transform.rotation) as GameObject;
			lTree.transform.localEulerAngles = save.savedTreesRotation.ElementAt(index);
			lTree.transform.localScale = save.savedTreesScale.ElementAt (index);
			lTree.transform.parent = GameObject.Find ("TreeHolder").transform;
			//lTree.GetComponent<TreeScale>().bCheckGround = false;
		//	lTree.GetComponent<TreeScale>().bLoaded = true;
			lTree.renderer.enabled = true;
			lTree.name += index;
			MakeParents();
			CheckIndex();
			index++;
			yield return new WaitForFixedUpdate();
		}
	}
	void loadChatki(Vector3 pos)
	{
		GameObject lChatka = GameObject.Instantiate (chatka, pos, chatka.transform.rotation) as GameObject;
		//lChatka.transform.localEulerAngles = savedChatkaRotation.ElementAt(cindex);
		//lChatka.transform.localEulerAngles = new Vector3(lChatka.transform.localEulerAngles.z, lChatka.transform.localEulerAngles.y, lChatka.transform.localEulerAngles.x);
	//	lTree.transform.localScale = savedTreesScale.ElementAt (index);
		lChatka.transform.parent = GameObject.Find ("Chatki").transform;
		//lChatka.GetComponent<ChatkaScale>().bCheckGround = false;
	//	lTree.GetComponent<TreeScale>().bLoaded = true;
		lChatka.renderer.enabled = true;
		lChatka.GetComponent<Tooltip>().bLoaded = true;
		lChatka.GetComponent<Tooltip>().tooltip = save.tooltipData.ElementAt (cindex);
		lChatka.name += cindex;
		//MakeParents();
		//CheckIndex();
		cindex++;
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
			//	Debug.Log ("Ma juz parenta? " + test.parent);
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
