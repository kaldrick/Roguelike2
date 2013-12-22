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
	public GameObject TreeHolder;
	public GameObject tree, chatka;
	public PlayerData playerData;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("PlayerWorld");
		playerData = player.GetComponent<PlayerData>();
		TreeHolder = GameObject.Find (name + "/TreeHolder");
		//TreeHolder.name = TreeHolder.name + " " + m_pos.x.ToString() + " " + m_pos.y.ToString() + " " + m_pos.z.ToString();
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
					GameObject lookTree = GameObject.Instantiate (tree,new Vector3(transform.position.x, Random.Range (30f, 250f), transform.position.z), tree.transform.rotation) as GameObject;
				//	lookTree.transform.Rotate (new Vector3(0, Random.Range (0f, 360f), 0));
				//Debug.Log (GameObject.Find ("Trees"+i));
				    lookTree.transform.parent = TreeHolder.transform;
					//playerData.trees.Add (lookTree);	
				yield return new WaitForSeconds(0.001f);
				//	GameObject.Find ("Trees" + i).GetComponent<CombineChildren>().CallCombineOnAllChilds();
			}
		    if(Random.Range (1, 55) == 3)
			{
				GameObject lookVillage = GameObject.Instantiate (chatka,new Vector3(transform.position.x, 0, transform.position.z), chatka.transform.rotation) as GameObject;
				lookVillage.transform.parent = GameObject.Find ("Chatki").transform;
				playerData.numberOfChatki++;
				yield return new WaitForSeconds(0.001f);
			}
			yield return new WaitForSeconds(0.001f);
		}
	}
	// Update is called once per frame
	void Update () {
		if(!bUsed)
		{
			if(((playerData.centerChunkPos.x - m_pos.x < 9*playerData.uncoverArea) && (playerData.centerChunkPos.x - m_pos.x > -9*playerData.uncoverArea)) && ((playerData.centerChunkPos.z - m_pos.z < 9*playerData.uncoverArea) && (playerData.centerChunkPos.z - m_pos.z > -9*playerData.uncoverArea)))
			{
				//Debug.Log ("Name: " + gameObject.name  + " vect difference " + (playerData.lastPosN - m_pos));
				if(!playerData.chunks.Contains(this))
				{
					playerData.chunks.Add (this);
				}
			}
			else
			{
				if(playerData.chunks.Contains(this))
				{
					playerData.chunks.Remove (this);
				}
			}
		}
	}
}
