using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChunkData : MonoBehaviour {
	public Vector3 m_pos;
	public GameObject player;
	public bool bOnce = false;
	public int i = 0;
	public bool bUsed = false;
	public bool bRiver = false;
	public bool bDodane = false;
	public List<float> test = new List<float>();
	public int randomTree;
	public float randomTreeMoveX, randomTreeMoveZ;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("PlayerWorld");
		if(!bDodane)
		{
			StartCoroutine(spawnObjects ());
		}		
	}
	IEnumerator spawnObjects()
	{
		for(int i = 1; i < 20; i++)
		{
			if(Random.Range (1,15) == 3)
			{
					GameObject lookTree = GameObject.Instantiate (player.GetComponent<PlayerData>().tree,new Vector3(transform.position.x, 30f, transform.position.z), player.GetComponent<PlayerData>().tree.transform.rotation) as GameObject;
				//	lookTree.transform.Rotate (new Vector3(0, Random.Range (0f, 360f), 0));
				//Debug.Log (GameObject.Find ("Trees"+i));
					lookTree.transform.parent = GameObject.Find ("TreeHolder").transform;
					player.GetComponent<PlayerData>().trees.Add (lookTree);	
				//	GameObject.Find ("Trees" + i).GetComponent<CombineChildren>().CallCombineOnAllChilds();
			}
		    if(Random.Range (1, 55) == 3)
			{
				GameObject lookVillage = GameObject.Instantiate (player.GetComponent<PlayerData>().village,new Vector3(transform.position.x, 0, transform.position.z), player.GetComponent<PlayerData>().village.transform.rotation) as GameObject;
				lookVillage.transform.parent = GameObject.Find ("Chatki").transform;
				player.GetComponent<PlayerData>().numberOfChatki++;
			}
			yield return new WaitForFixedUpdate();
		}
	}
	// Update is called once per frame
	void Update () {
		if(!bUsed)
		{
			if(((player.GetComponent<PlayerData>().centerChunkPos.x - m_pos.x < 9*player.GetComponent<PlayerData>().uncoverArea) && (player.GetComponent<PlayerData>().centerChunkPos.x - m_pos.x > -9*player.GetComponent<PlayerData>().uncoverArea)) && ((player.GetComponent<PlayerData>().centerChunkPos.z - m_pos.z < 9*player.GetComponent<PlayerData>().uncoverArea) && (player.GetComponent<PlayerData>().centerChunkPos.z - m_pos.z > -9*player.GetComponent<PlayerData>().uncoverArea)))
			{
				//Debug.Log ("Name: " + gameObject.name  + " vect difference " + (player.GetComponent<PlayerData>().lastPosN - m_pos));
				if(!player.GetComponent<PlayerData>().chunks.Contains(this))
				{
					player.GetComponent<PlayerData>().chunks.Add (this);
				}
			}
			else
			{
				if(player.GetComponent<PlayerData>().chunks.Contains(this))
				{
					player.GetComponent<PlayerData>().chunks.Remove (this);
				}
			}
		}
	}
}
