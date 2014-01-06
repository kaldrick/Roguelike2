using UnityEngine;
using System.Collections;

public class ActiveDeactive : MonoBehaviour {
	public GameObject trees;
	public Vector3 m_pos;
	void Start()
	{
		trees = GameObject.Find (gameObject.name + "/TreeHolder");
	}
	// Use this for initialization
	public void SetActivated()
	{
		trees.SetActive (!gameObject.activeSelf);
	}
}
