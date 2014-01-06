using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour{
	public string name;
	public int quality;
	public float weight;
	public int quantity;
	public int price;
	public bool bChecked = false;
	public string houseName;
	public Item(string dName, int dQuality, int dQuantity, float dWeight, int dPrice)
	{
		name = dName;
		quality = dQuality;
		quantity = dQuantity;
		weight = dWeight;
		price = dPrice;
	}
	void Awake()
	{
		bChecked = false;
	}
	void Update()
	{
		if(gameObject.activeSelf && !bChecked)
		{
			foreach(Transform child in gameObject.transform)
			{
				if(child.name == "NAME")
				{
					child.GetComponent<UILabel>().text = name;
				}
				else if(child.name == "QLT")
				{
					child.GetComponent<UILabel>().text = quality.ToString ();
				}
				else if(child.name == "QT")
				{
					child.GetComponent<UILabel>().text = quantity.ToString ();
				}
				else if(child.name == "WEIGHT")
				{
					child.GetComponent<UILabel>().text = weight.ToString () + " KG";
				}
				else if(child.name == "PRICE")
				{
					child.GetComponent<UILabel>().text = price.ToString () + " $";
				}
			}
			//Debug.Log ("Czekuje!" + transform.name);
			bChecked = true;
		}
	}
}