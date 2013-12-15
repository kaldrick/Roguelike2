using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private Vector3 newPos;
	private bool bChangeCamera;
	public GameObject bulletPrefab;
	public GameObject[] bullets;
	// Use this for initialization
	void Start () {
		rigidbody.freezeRotation = true;
		bullets = new GameObject[100];
		for(int i=0; i<100; i++)
		{
			bullets[i] = Instantiate (bulletPrefab, GameObject.Find("GunPoint").transform.position, GameObject.Find("GunPoint").transform.rotation) as GameObject;
			bullets[i].GetComponent<Bullet>().forward = transform.forward;
			Physics.IgnoreCollision (bullets[i].collider, collider);
			bullets[i].transform.parent = GameObject.Find ("Bullets").transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		float rotationInput = Input.GetAxis("Horizontal") * Time.deltaTime * 128;
		float moveInput = Input.GetAxis ("Vertical") * Time.deltaTime * 384;
		//transform.Translate(moveVector);
		rigidbody.velocity = transform.forward * moveInput;
		newPos = transform.position;
		newPos.y = 1.5f;
		transform.position = newPos;
		transform.Rotate(new Vector3(0, rotationInput, 0));
		if(Input.GetKeyDown (KeyCode.Tab))
		{
			bChangeCamera = !bChangeCamera;
		}
		if(!bChangeCamera)
		{
			Camera.main.depth = -1;
			Camera.main.farClipPlane = 0.02f;
		}
		else
		{
			Camera.main.depth = 1;
			Camera.main.farClipPlane = 1000;
		}
		if(Input.GetButtonDown("Fire1"))
		{
			FireBullet();
		}
	}
	void FireBullet()
	{
		int bulletNum = -1;
		for(int i=0; i<100;i++)
		{
			if(bullets[i].activeSelf == false) 
		    {
				bulletNum=i;
				break; 
			}
		}
		// found one (if we couldn't find unused orc, no spawn):
		if(bulletNum>=0) 
		{ 
			bullets[bulletNum].SetActive(true);
			bullets[bulletNum].GetComponent<Bullet>().forward = transform.forward;
			bullets[bulletNum].transform.position = GameObject.Find("GunPoint").transform.position;
			bullets[bulletNum].transform.rotation = GameObject.Find("GunPoint").transform.rotation;
			bullets[bulletNum].rigidbody.velocity = new Vector3(0,0,0);
			Physics.IgnoreCollision (bullets[bulletNum].collider, collider);
			// activate this orc, move it to spawn point:
			
			
		}
	}
	void OnCollisionEnter(Collision other)
	{
		Debug.Log ("BUM!" + other.collider.name);
	}
}
