using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
public class SaveMenu : MonoBehaviour {
	/*public SaveSceneComponent save;
	void Start()
	{ 	
		//save = GameObject.Find ("SaveTracker").GetComponent<SaveSceneComponent>();
	}
 void OnGUI()
    {
        if (GUI.Button(new Rect(20, 100, 100, 30), "Save"))
        {
			save.m_voxels.ForEach (checkChunk);
			save.m_voxelsName.ForEach (checkString);
			var serializer = new XmlSerializer(typeof(SaveSceneComponent));
			var stream = new FileStream("", FileMode.Create);
			serializer.Serialize (stream, save);
			stream.Close();
        }

        if (GUI.Button(new Rect(20, 140, 100, 30), "Load"))
        {
			deleteChunk();
			save.savedVoxels.ForEach (loadChunk);
        }
		
        if (GUI.Button(new Rect(20, 180, 100, 30), "Restart Scene"))
        {
            Application.LoadLevel(Application.loadedLevelName);
        }
    }
	void checkChunk(VoxelChunk m_voxel)
	{
		save.savedVoxels.Add (m_voxel);
		m_voxel.bDodane = true;
	}
	void checkString(string m_voxelName)
	{
		save.savedVoxelsNames.Add (m_voxelName);	
	}
	void deleteChunk()
	{
		//Destroy(GameObject.Find (m_voxelName));
		foreach(Transform test in GameObject.Find ("Terrain").transform)
		{
			Destroy(test.gameObject);
		}
	}
	void loadChunk(VoxelChunk m_voxel)
	{
		m_voxel.CreateMesh (GameObject.Find ("TerrainGenerator").GetComponent<GenerateTerrain>().m_material);
		save.m_voxelsName = save.savedVoxelsNames;
	}*/
}
