using UnityEngine;
using System.Collections;

public class Hide : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if(renderer.isVisible)
		{
			renderer.enabled = true;
		}
		else
		{
			renderer.enabled = false;
		}
	}
}
