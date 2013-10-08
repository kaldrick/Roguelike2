using UnityEngine;
using System.Collections;

public class ChunkData : MonoBehaviour {
	public Vector3 m_pos;
	public GameObject player;
	public bool bOnce = false;
	public int i = 0;
	public bool bUsed = false;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("PlayerWorld");
		for(int i = 0; i < 5; i++)
		{
			GameObject lookTree = GameObject.Instantiate (player.GetComponent<PlayerData>().tree,transform.position + new Vector3(Random.Range(-120, 120), 40, Random.Range (-120, 120)), player.GetComponent<PlayerData>().tree.transform.rotation) as GameObject;
			lookTree.transform.Rotate (new Vector3(0, Random.Range (0f, 360f), 0));
			Debug.Log (GameObject.Find ("Trees"+i));
			if(GameObject.Find ("Trees"+i))
			{
				lookTree.transform.parent = GameObject.Find ("Trees"+i).transform;
			}
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
