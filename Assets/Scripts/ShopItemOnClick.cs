using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopItemOnClick : MonoBehaviour {
	public Item item;
	public PlayerData player;
	public float fTime = 0.0f;
	void Start()
	{
		player = GameObject.Find ("PlayerWorld").GetComponent<PlayerData>();
	}
	void OnEnable()
	{
		fTime = 0.0f;
	}
	void OnPress (bool pressed)
	{
		if (enabled && gameObject.activeSelf && fTime > 0.2f)
		{
			item = transform.parent.GetComponent<Item>();
			if(item.name != "Bottlecaps")
			{
				if(item.houseName == null)
				{
					if(transform.root.name == "City")
					{
						StartCoroutine (addInventory2(item));

					}


				}
				else
				{
					StartCoroutine(addInventory(item));
				}
			}
			else
			{
			}
		}
	}
	void Update()
	{
		if(gameObject.activeSelf)
		{
			fTime += Time.deltaTime;
		}
	}
	IEnumerator addInventory2(Item item)
	{
		fTime = 0.0f;
		if(GameObject.Find (transform.root.GetComponent<CityVariables>().chatkaName).GetComponent<ChatkaOnClick>().cash > item.price)
		{
			if(item.quantity >= 1)
			{
				yield return new WaitForSeconds(0.1f);
				GameObject.Find ("PlayerWorld").GetComponent<PlayerData>().RemoveItem (item);
				GameObject.Find (transform.root.GetComponent<CityVariables>().chatkaName).GetComponent<ChatkaOnClick>().bInventoryChanged = true;
				yield return new WaitForSeconds(0.1f);
				GameObject.Find (transform.root.GetComponent<CityVariables>().chatkaName).GetComponent<ChatkaOnClick>().AddItem (item);
				GameObject.Find (transform.root.GetComponent<CityVariables>().chatkaName).GetComponent<ChatkaOnClick>().checkName = transform.root.GetComponent<CityVariables>().chatkaName;
				GameObject.Find (transform.root.GetComponent<CityVariables>().chatkaName).GetComponent<ChatkaOnClick>().bAddedItem = true;
				//yield return new WaitForSeconds (0.1f);
				yield return null;
			}
			else
			{
				yield return new WaitForSeconds(0.1f);
				//yield return new WaitForSeconds(0.1f);
				GameObject.Find (transform.root.GetComponent<CityVariables>().chatkaName).GetComponent<ChatkaOnClick>().AddItem (item);
				GameObject.Find (transform.root.GetComponent<CityVariables>().chatkaName).GetComponent<ChatkaOnClick>().checkName = transform.root.GetComponent<CityVariables>().chatkaName;
				GameObject.Find (transform.root.GetComponent<CityVariables>().chatkaName).GetComponent<ChatkaOnClick>().bAddedItem = true;
				yield return new WaitForSeconds (0.1f);
				GameObject.Find ("PlayerWorld").GetComponent<PlayerData>().RemoveItem (item);
				GameObject.Find (transform.root.GetComponent<CityVariables>().chatkaName).GetComponent<ChatkaOnClick>().bInventoryChanged = true;
				fTime = 0.0f;
				yield return null;
			}
		}
	}
	IEnumerator addInventory(Item item)
	{
		fTime = 0.0f;
		if(GameObject.Find ("PlayerWorld").GetComponent<PlayerData>().cash >= item.price)
		{
			if(item.quantity > 1)
			{
				yield return new WaitForSeconds(0.1f);
				GameObject.Find (item.houseName).GetComponent<ChatkaOnClick>().RemoveItem (item);
				GameObject.Find (item.houseName).GetComponent<ChatkaOnClick>().bInventoryChanged = true;
				yield return new WaitForSeconds (0.1f);
				GameObject.Find ("PlayerWorld").GetComponent<PlayerData>().AddItem (item);
				GameObject.Find ("PlayerWorld").GetComponent<PlayerData>().HouseInventoryCount = 0;
				fTime = 0.0f;
				yield return null;
			}
			else
			{
				yield return new WaitForSeconds(0.1f);
				GameObject.Find ("PlayerWorld").GetComponent<PlayerData>().AddItem (item);
				GameObject.Find ("PlayerWorld").GetComponent<PlayerData>().HouseInventoryCount = 0;
				yield return new WaitForSeconds (0.1f);
				GameObject.Find (item.houseName).GetComponent<ChatkaOnClick>().RemoveItem (item);
				GameObject.Find (item.houseName).GetComponent<ChatkaOnClick>().bInventoryChanged = true;
				fTime = 0.0f;
				yield return null;
			}
		}
	}
}
