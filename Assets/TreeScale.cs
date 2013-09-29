using UnityEngine;
using System.Collections;

public class TreeScale : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3(Random.Range(1.5f, 5f), Random.Range (1.5f,5f), Random.Range (1.5f, 5f));
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}