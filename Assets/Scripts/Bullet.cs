using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float speed = 512f;
	public Vector3 forward;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.activeSelf != false)
		{
			rigidbody.AddForce(forward * speed * Time.deltaTime*3); 
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		if(collision.collider.tag == "Enemy")
		{
			Debug.Log ("TRAFILEM WROGA");
			collision.collider.gameObject.GetComponent<AIController>().bHurt = true;
			collision.collider.gameObject.GetComponent<AIController>().HP -= 20;
		}
		gameObject.SetActive (false);
	}
}
