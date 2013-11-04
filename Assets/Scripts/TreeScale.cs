using UnityEngine;
using System.Collections;

public class TreeScale : MonoBehaviour {
	public Vector3 coord;
	public GameObject player;
	public SaveSceneComponent save;
	public bool bCheckGround = true;
	public bool bLoaded = false;
	public float fTime = 0.0f;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("PlayerWorld");
		save = GameObject.Find ("SaveTracker").GetComponent<SaveSceneComponent>();
	
	}
	void Update()
	{
		if(bCheckGround)
		{
			fTime += Time.deltaTime;
		}
		if(fTime > 1.0f && bCheckGround)
		{
			transform.localPosition += new Vector3(Random.Range (-130, 130), 0, Random.Range (-130,130));
			transform.localEulerAngles += new Vector3(0, Random.Range (0, 360), 0);
			transform.localScale = new Vector3(Random.Range(1.5f, 5f), Random.Range (1.5f,5f), Random.Range (1.5f, 5f));
			
			StartCoroutine(CheckGround());
			bCheckGround = false;
		}
		if(bLoaded)
		{
			StartCoroutine(CheckGround());
			bLoaded = false;
		}
	}
	IEnumerator CheckGround()
	{
		RaycastHit hit;
		if (Physics.Raycast (transform.position, -Vector3.up, out hit, 120f))
		{
			if(bCheckGround)
			{
				if(hit.point.y < -75f || hit.transform.name == "TreeP(Clone)" || hit.transform.name == "chatka(Clone)" || hit.transform.name == "TreeC" ||hit.transform.name == "chatka")
				{
					Destroy (gameObject);
				}
				else
				{
					transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
					renderer.enabled = true;
					player.GetComponent<PlayerData>().numberOfTrees++;
					if(!save.save.treesPos.Contains (hit.point))
					{
						save.save.treesPos.Add (hit.point);
						save.save.treesScale.Add(transform.localScale);
						save.save.treesRotation.Add(transform.localEulerAngles);
					}
					//save.treesName.Add(transform.name);
				}
			}
			else
			{
				if(!save.save.treesPos.Contains (hit.point))
				{
					save.save.treesPos.Add (hit.point);
					save.save.treesScale.Add(transform.localScale);
					save.save.treesRotation.Add(transform.localEulerAngles);
				}
			}
			
			//Debug.Log (hit.point.y);
		}
		else
		{
			Destroy (gameObject);	
		}
		yield return new WaitForSeconds(.3f);
	}
}