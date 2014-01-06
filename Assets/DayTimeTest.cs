using UnityEngine;
using System.Collections;

public class DayTimeTest : MonoBehaviour {
	private bool bTest = true;
//	private float fTime = 0.0f;
	public Color noonCol, eveningCol, goldenHourCol, nightCol;
 	Light lightz;
	// Use this for initialization
	void Start () {
//		lightz = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		if(bTest)
		{
			transform.Rotate (new Vector3(0, 0.50f, 0));
		}
		if(Input.GetKeyDown(KeyCode.Space))
		{
			bTest = !bTest;	
		}
	}
}