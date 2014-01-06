using UnityEngine;
using System.Collections;

public class ColliderIgnore : MonoBehaviour {
	// Use this for initialization
	void Start () {
		Physics.IgnoreLayerCollision (11, 0);
		Physics.IgnoreLayerCollision (11, 1);
		Physics.IgnoreLayerCollision (11, 2);
		Physics.IgnoreLayerCollision (11, 3);
		Physics.IgnoreLayerCollision (11, 4);
		Physics.IgnoreLayerCollision (11, 5);
		Physics.IgnoreLayerCollision (11, 6);
		Physics.IgnoreLayerCollision (11, 7);
		Physics.IgnoreLayerCollision (11, 8);
		Physics.IgnoreLayerCollision (11, 9);
	}
	void OnTriggerEnter(Collider other)
	{
		ActiveDeactive activated = other.GetComponent<ActiveDeactive>();
		if(!other.gameObject.activeSelf)
		{
			activated.SetActivated ();
		}
	}

	void OnTriggerExit(Collider other)
	{
		ActiveDeactive activated = other.GetComponent<ActiveDeactive>();
		if(other.gameObject.activeSelf)
		{
			activated.SetActivated ();
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
