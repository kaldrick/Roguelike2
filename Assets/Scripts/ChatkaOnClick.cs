using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
public class ChatkaOnClick : MonoBehaviour {
	public string myName, newName, checkName;
	public GameObject cityInv, houseTypes, itemPrefab, newItem, inventoryParent, cityGrid;
	public bool bInventoryChanged = false;
	public bool bAddedItem = false;
	public List<Item> Inventory = new List<Item>();
	public List<Item> startInv = new List<Item>();
	public CityVariables city;
	public int invCount = 0;
	public int InventoryCount = 0;
	public HouseTypes hT;
	public int quantityTest = 0;
	public int removeTest = 0;
	public bool bNormalInv = false;
	void OnMouseDown()
	{	
		city = GameObject.Find ("City").GetComponent<CityVariables>();
		if(!cityInv.activeSelf)
		{
			cityInv.SetActive (true);
			StartCoroutine (rozpieprz ());
			StartCoroutine (checkCity ());
		}
		else
		{
		}
	}
	IEnumerator rozpieprz()
	{
		foreach(Transform child in GameObject.Find ("CityGrid").transform)
		{
			Destroy (child.gameObject);
			yield return new WaitForSeconds(0.001f);
		}
	}
	IEnumerator checkCity()
	{
		yield return new WaitForSeconds (0.01f);
		if(city.houseInventories.ContainsKey(city.parentNumber.ToString () + "/" + myName))
		{
			//Inventory.Clear ();
			Inventory = city.houseInventories[city.parentNumber.ToString () + "/" + myName];
			//Debug.Log ("MAM!" + city.parentNumber.ToString () + "/" + myName + " ## " + Inventory.Count +"/" +  city.houseInventories[city.parentNumber.ToString () + "/" + myName].Count);
			bInventoryChanged = true;
		}
		else
		{
			
		}
		yield return new WaitForSeconds(0.05f);

			if(!bInventoryChanged)
			{
				StartCoroutine (startChatka ());
				
			}
			else
			{
				StartCoroutine (destroyChildren());
			}		
	}
	IEnumerator destroyChildren()
	{

		transform.root.GetComponent<CityVariables>().chatkaName = transform.name;
		GameObject.Find ("ChatkaName").GetComponent<UILabel>().text = myName;
		city.currentHouseName = city.parentNumber.ToString () + "/" + myName;
		foreach(Transform child in GameObject.Find ("CityGrid").transform)
		{
			Destroy (child.gameObject);
			yield return new WaitForSeconds(0.001f);
		}
		yield return new WaitForSeconds(0.1f);
		InventoryCount = 0;
		bNormalInv = true;
	}
	public void AddItem(Item item)
	{
		item.gameObject.name = item.gameObject.name.Remove (item.gameObject.name.Length - 1) + "h";
		if(Inventory.Find (i => i.name == item.name))
		{
			quantityTest++;
			Debug.Log ("Znalazłem: " + item.name + " na miejscu: " + Inventory.IndexOf (item) + "w ChatkaOnClick AddItem" + quantityTest);
			if(quantityTest>1)
			{
				Inventory.Find (i => i.name == item.name).quantity++;
				Inventory.Find (i => i.name == item.name).bChecked = false;
			}
		}
		else
		{
		//	//Debug.Log ("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" + item);
			quantityTest = 0;
			newItem = Instantiate (itemPrefab, itemPrefab.transform.position, itemPrefab.transform.rotation) as GameObject;
			newItem.AddComponent<Item>();
			CopyTo (item, newItem.GetComponent<Item>());
			if(inventoryParent == null)
			{
				inventoryParent = GameObject.Find ("CityGrid");
			}
			newItem.transform.parent = inventoryParent.transform;

			newItem.transform.name = newItem.GetComponent<Item>().name + "h";
			newItem.transform.localPosition += new Vector3(48,0,0);
			newItem.transform.localScale = new Vector3(1,1,1);
			newItem.GetComponent<Item>().houseName = gameObject.name;
			Inventory.Add (newItem.GetComponent<Item>());
			////Debug.Log ("????" + Inventory.Find (i => i.name == item.name));
		}
		StartCoroutine (RepositionGrid());
	}
	IEnumerator normalInv()
	{
		//Debug.Log ("DzialaM!!!!!!##########!!!!!!!!!!!" + InventoryCount + "/" + Inventory.Count);
		if(InventoryCount < Inventory.Count && GameObject.Find ("CityUI Root (3D)"))
		{
			if(!GameObject.Find ("CityGrid/"+Inventory.ElementAt (InventoryCount).name + "h"))
			{
				////Debug.Log ("Grid/"+Inventory.ElementAt (InventoryCount).name);
				//prevItem = GameObject.Find ("Item" + (InventoryCount-1).ToString ());
				newItem = Instantiate (itemPrefab, itemPrefab.transform.position, itemPrefab.transform.rotation) as GameObject;
				//newItem.AddComponent<Item>();
				newItem.AddComponent<Item>();
				CopyTo (Inventory.ElementAt (InventoryCount), newItem.GetComponent<Item>());
				if(inventoryParent == null)
				{
					inventoryParent = GameObject.Find ("CityGrid");
				}
				newItem.transform.parent = inventoryParent.transform;
				newItem.transform.name = newItem.GetComponent<Item>().name + "h";

				newItem.transform.localScale = new Vector3(1,1,1);
				newItem.GetComponent<Item>().houseName = gameObject.name;
				inventoryParent.GetComponent<UIGrid>().Reposition ();
				newItem.transform.localPosition += new Vector3(48,0,0);
				//	newItem.transform.position = prevItem.transform.position - new Vector3(0,0.12f,0);
				/*foreach(Transform child in newItem.transform)
						{
							if(child.name == "NAME")
							{
								child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).name;
							}
							else if(child.name == "QLT")
							{
								child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).quality.ToString ();
							}
							else if(child.name == "WEIGHT")
							{
								child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).weight.ToString () + " KG";
							}
				//		}*/
				InventoryCount++;
				yield return new WaitForSeconds(0.1f);
			}
			else
			{
				if(bInventoryChanged)
				{
					Debug.Log ("Znalazłem: " + Inventory.ElementAt (InventoryCount).name + " na miejscu: " + InventoryCount  + "w ChatkaOnClick normalInv");
					Inventory.ElementAt (InventoryCount).quantity++;
					GameObject.Find ("CityGrid/"+Inventory.ElementAt (InventoryCount).name + "h").GetComponent<Item>().bChecked = false;
					if(InventoryCount < Inventory.Count)
					{
						InventoryCount++;
					}
					else
					{
						InventoryCount = Inventory.Count;
					}
				}
				yield return new WaitForSeconds(0.1f);
			}
		}
	}
	void Update()
	{
		if(InventoryCount < Inventory.Count && GameObject.Find ("CityUI Root (3D)") && bNormalInv)
		{
		//	InventoryCount = 0;
			//Debug.Log ("HULAM" + InventoryCount + "/" + Inventory.Count);
			StartCoroutine (normalInv ());
		}
		if(InventoryCount >= Inventory.Count)
		{
			//InventoryCount = 0;
			bNormalInv = false;
		}
	}
	void CopyTo(Item oldItem, Item newItem)
	{
		newItem.name = oldItem.name;
		newItem.quality = oldItem.quality;
		newItem.quantity = oldItem.quantity;
		newItem.weight = oldItem.weight;
		newItem.price = oldItem.price;
	}


	public void RemoveItem(Item item)
	{
		if(item.quantity > 1)
		{
			removeTest++;

			if(removeTest > 1)
			{
				item.quantity--;
				item.bChecked = false;
				Debug.Log ("Znalazłem: " + item.name + " na miejscu: " + item.quantity + "w ChatkaOnClick" + removeTest);
			}
		}
		else
		{
			//Debug.Log ("????" + Inventory.Find (i => i.name == item.name));
			Inventory.Remove ( Inventory.Find (i => i.name == item.name));
			//GameObject.Find ("CityGrid/" + item.name + "i").SetActive(false);
			GameObject.Destroy (GameObject.Find ("CityGrid/" + item.name + "i"));
			StartCoroutine (RepositionGrid());
		}
	}
	IEnumerator RepositionGrid()
	{
		yield return new WaitForSeconds(.1f);
		GameObject.Find ("CityGrid").GetComponent<UIGrid>().Reposition ();
		city.houseInventories[city.parentNumber.ToString () + "/" + myName] = Inventory;
		Debug.Log ("###########" + city.houseInventories[city.parentNumber.ToString () + "/" + myName]);
		quantityTest = 0;
		removeTest = 0;
		foreach(Transform child in GameObject.Find ("CityGrid").transform)
		{
			child.localPosition += new Vector3(48,0,0);
			//yield return new WaitForFixedUpdate();
		}
	}
	/*IEnumerator emptyChatka()
	{
		switch(myName)
		{
		case "bFarm":
			newName = "Farm";
			cityInv.SetActive (true);
			transform.root.GetComponent<CityVariables>().chatkaName = transform.name;
			GameObject.Find ("ChatkaName").GetComponent<UILabel>().text = newName;
			yield return new WaitForSeconds(0.001f);
			break;
		case "bTartak":
			newName = "Sawmill";
			cityInv.SetActive (true);
			transform.root.GetComponent<CityVariables>().chatkaName = transform.name;
			GameObject.Find ("ChatkaName").GetComponent<UILabel>().text = newName;
			yield return new WaitForSeconds(0.001f);
			break;
		case "bHunter":
			newName = "Hunter";
			cityInv.SetActive (true);
			transform.root.GetComponent<CityVariables>().chatkaName = transform.name;
			GameObject.Find ("ChatkaName").GetComponent<UILabel>().text = newName;
			yield return new WaitForSeconds(0.001f);
			break;
		case "bChief":
			newName = "Chief";
			cityInv.SetActive (true);
			transform.root.GetComponent<CityVariables>().chatkaName = transform.name;
			GameObject.Find ("ChatkaName").GetComponent<UILabel>().text = newName;
			yield return new WaitForSeconds(0.001f);
			break;
		case "bShaman":
			newName = "Shaman";
			cityInv.SetActive (true);
			transform.root.GetComponent<CityVariables>().chatkaName = transform.name;
			GameObject.Find ("ChatkaName").GetComponent<UILabel>().text = newName;
			yield return new WaitForSeconds(0.001f);
			break;
		case "bWorkshopMech":
			newName = "Mechanical Workshop";
			cityInv.SetActive (true);
			transform.root.GetComponent<CityVariables>().chatkaName = transform.name;
			GameObject.Find ("ChatkaName").GetComponent<UILabel>().text = newName;
			yield return new WaitForSeconds(0.001f);
			break;
		case "bGorzelnia":
			newName = "Distillery";
			cityInv.SetActive (true);
			transform.root.GetComponent<CityVariables>().chatkaName = transform.name;
			GameObject.Find ("ChatkaName").GetComponent<UILabel>().text = newName;
			yield return new WaitForSeconds(0.001f);
			break;
		case "bWorkshopArm":
			newName = "Arms Workshop";
			cityInv.SetActive (true);
			transform.root.GetComponent<CityVariables>().chatkaName = transform.name;
			GameObject.Find ("ChatkaName").GetComponent<UILabel>().text = newName;
			yield return new WaitForSeconds(0.001f);
			break;
		case "bMedic":
			newName = "Medic";
			cityInv.SetActive (true);
			transform.root.GetComponent<CityVariables>().chatkaName = transform.name;
			GameObject.Find ("ChatkaName").GetComponent<UILabel>().text = newName;
			yield return new WaitForSeconds(0.001f);
			break;
		case "bSklad":
			newName = "Warehouse";
			cityInv.SetActive (true);
			transform.root.GetComponent<CityVariables>().chatkaName = transform.name;
			GameObject.Find ("ChatkaName").GetComponent<UILabel>().text = newName;
			yield return new WaitForSeconds(0.001f);
			break;
		case "bKarawana":
			newName = "Caravan";
			cityInv.SetActive (true);
			transform.root.GetComponent<CityVariables>().chatkaName = transform.name;
			GameObject.Find ("ChatkaName").GetComponent<UILabel>().text = newName;
			yield return new WaitForSeconds(0.001f);
			break;
		case "bSzabry":
			newName = "Loot Brotherhood";
			cityInv.SetActive (true);
			transform.root.GetComponent<CityVariables>().chatkaName = transform.name;
			GameObject.Find ("ChatkaName").GetComponent<UILabel>().text = newName;
			yield return new WaitForSeconds(0.001f);
			break;
		}

	}*/
	IEnumerator startChatka()
	{
		Inventory.Clear ();
		transform.root.GetComponent<CityVariables>().chatkaName = transform.name;
		GameObject.Find ("ChatkaName").GetComponent<UILabel>().text = myName;
		city.currentHouseName = city.parentNumber.ToString () + "/" + myName;
		if(cityGrid == null)
		{
			cityGrid = GameObject.Find ("CityGrid");
		}
		foreach(Transform child in cityGrid.transform)
		{
			Destroy (child.gameObject);
			//cityGrid.GetComponent<UIGrid>().Reposition ();
			yield return new WaitForSeconds(0.001f);
		}
		yield return new WaitForSeconds(0.2f);
		HouseTypes value = houseTypes.GetComponent<HouseTypes>();
		startInv = value.houses["nameOfGoods" + myName];
		for(int i = 0; i < startInv.Count; i++)
		{
			newItem = Instantiate (itemPrefab, itemPrefab.transform.position, itemPrefab.transform.rotation) as GameObject;
			newItem.AddComponent<Item>();
			CopyTo (startInv[i], newItem.GetComponent<Item>());
			
			//newItem.AddComponent<houseTypes.GetComponent<HouseTypes>().nameOfGoodsFarm[i]>();
			//	//newItem.GetComponent<Item>() = houseTypes.GetComponent<HouseTypes>().nameOfGoodsFarm[i];
			if(inventoryParent == null)
			{
				inventoryParent = GameObject.Find ("CityGrid");
			}
			newItem.transform.parent = inventoryParent.transform;
			newItem.transform.name = newItem.GetComponent<Item>().name + "h";

			Inventory.Add (newItem.GetComponent<Item>());
			newItem.GetComponent<Item>().houseName = gameObject.name;
			newItem.transform.localScale = new Vector3(1,1,1);
			inventoryParent.GetComponent<UIGrid>().Reposition ();
			newItem.transform.localPosition += new Vector3(48,0,0);
			//foreach(Transform child in newItem.transform)
			//{
			//	if(child.name == "NAME")
			//	{
			
			//child.GetComponent<UILabel>().text = houseTypes.GetComponent<HouseTypes>().nameOfGoodsFarm[i].name;
			//}
			//yield return new WaitForSeconds(0.001f);
			/*else if(child.name == "QLT")
						{
							//child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).quality.ToString ();
						}
						else if(child.name == "WEIGHT")
						{
							//child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).weight.ToString () + " KG";
						}*/
			//}
			yield return new WaitForSeconds(0.001f);
		}
		city.houseInventories.Add (city.parentNumber.ToString () + "/" + myName, Inventory);
	}
}
