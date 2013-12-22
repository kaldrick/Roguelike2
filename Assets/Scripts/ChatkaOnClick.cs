using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ChatkaOnClick : MonoBehaviour {
	public string myName, newName;
	public GameObject cityInv, houseTypes, item, newItem, inventoryParent, cityGrid;
	public int invCount = 0;
	void OnMouseDown()
	{
		StartCoroutine (startChatka ());
	}
	IEnumerator startChatka()
	{
		switch(myName)
		{
		case "bFarm":
			newName = "Farm";
			cityInv.SetActive (true);
			GameObject.Find ("ChatkaName").GetComponent<UILabel>().text = newName;
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
			//yield return new WaitForSeconds(0.4f);
			for(int i = 0; i < houseTypes.GetComponent<HouseTypes>().nameOfGoodsFarm.Count; i++)
			{
				newItem = Instantiate (item, item.transform.position, item.transform.rotation) as GameObject;
				if(inventoryParent == null)
				{
					inventoryParent = GameObject.Find ("CityGrid");
				}
				newItem.transform.parent = inventoryParent.transform;
				newItem.transform.localScale = new Vector3(1,1,1);
				inventoryParent.GetComponent<UIGrid>().Reposition ();
				foreach(Transform child in newItem.transform)
				{
					if(child.name == "NAME")
					{
						
						child.GetComponent<UILabel>().text = houseTypes.GetComponent<HouseTypes>().nameOfGoodsFarm[i];
					}
					yield return new WaitForSeconds(0.001f);
					/*else if(child.name == "QLT")
						{
							child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).quality.ToString ();
						}
						else if(child.name == "WEIGHT")
						{
							child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).weight.ToString () + " KG";
						}*/
				}
				yield return new WaitForSeconds(0.001f);
			}
			break;
		case "bTartak":
			newName = "Sawmill";
			cityInv.SetActive (true);
			GameObject.Find ("ChatkaName").GetComponent<UILabel>().text = newName;
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
			//yield return new WaitForSeconds(0.4f);
			for(int i = 0; i < houseTypes.GetComponent<HouseTypes>().nameOfGoodsSawmill.Count; i++)
			{
				newItem = Instantiate (item, item.transform.position, item.transform.rotation) as GameObject;
				if(inventoryParent == null)
				{
					inventoryParent = GameObject.Find ("CityGrid");
				}
				newItem.transform.parent = inventoryParent.transform;
				newItem.transform.localScale = new Vector3(1,1,1);
				inventoryParent.GetComponent<UIGrid>().Reposition ();
				foreach(Transform child in newItem.transform)
				{
					if(child.name == "NAME")
					{
						
						child.GetComponent<UILabel>().text = houseTypes.GetComponent<HouseTypes>().nameOfGoodsSawmill[i];
					}
					/*else if(child.name == "QLT")
							{
								child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).quality.ToString ();
							}
							else if(child.name == "WEIGHT")
							{
								child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).weight.ToString () + " KG";
							}*/
					yield return new WaitForSeconds(0.001f);
				}
				yield return new WaitForSeconds(0.001f);
			}
			break;
		case "bHunter":
			newName = "Hunter";
			cityInv.SetActive (true);
			GameObject.Find ("ChatkaName").GetComponent<UILabel>().text = newName;
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
			//yield return new WaitForSeconds(0.4f);
			for(int i = 0; i < houseTypes.GetComponent<HouseTypes>().nameOfGoodsHunter.Count; i++)
			{
				newItem = Instantiate (item, item.transform.position, item.transform.rotation) as GameObject;
				if(inventoryParent == null)
				{
					inventoryParent = GameObject.Find ("CityGrid");
				}
				newItem.transform.parent = inventoryParent.transform;
				newItem.transform.localScale = new Vector3(1,1,1);
				inventoryParent.GetComponent<UIGrid>().Reposition ();
				foreach(Transform child in newItem.transform)
				{
					if(child.name == "NAME")
					{
						
						child.GetComponent<UILabel>().text = houseTypes.GetComponent<HouseTypes>().nameOfGoodsHunter[i];
					}
					/*else if(child.name == "QLT")
								{
									child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).quality.ToString ();
								}
								else if(child.name == "WEIGHT")
								{
									child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).weight.ToString () + " KG";
								}*/
					yield return new WaitForSeconds(0.001f);
				}
				yield return new WaitForSeconds(0.001f);
			}
			break;
		case "bChief":
			newName = "Chief";
			cityInv.SetActive (true);
			GameObject.Find ("ChatkaName").GetComponent<UILabel>().text = newName;
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
			//yield return new WaitForSeconds(0.4f);
			for(int i = 0; i < houseTypes.GetComponent<HouseTypes>().nameOfGoodsChief.Count; i++)
			{
				newItem = Instantiate (item, item.transform.position, item.transform.rotation) as GameObject;
				if(inventoryParent == null)
				{
					inventoryParent = GameObject.Find ("CityGrid");
				}
				newItem.transform.parent = inventoryParent.transform;
				newItem.transform.localScale = new Vector3(1,1,1);
				inventoryParent.GetComponent<UIGrid>().Reposition ();
				foreach(Transform child in newItem.transform)
				{
					if(child.name == "NAME")
					{
						
						child.GetComponent<UILabel>().text = houseTypes.GetComponent<HouseTypes>().nameOfGoodsChief[i];
					}
					/*else if(child.name == "QLT")
								{
									child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).quality.ToString ();
								}
								else if(child.name == "WEIGHT")
								{
									child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).weight.ToString () + " KG";
								}*/
					yield return new WaitForSeconds(0.001f);
				}
				yield return new WaitForSeconds(0.001f);
			}
			break;
		case "bShaman":
			newName = "Shaman";
			cityInv.SetActive (true);
			GameObject.Find ("ChatkaName").GetComponent<UILabel>().text = newName;
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
			//yield return new WaitForSeconds(0.4f);
			for(int i = 0; i < houseTypes.GetComponent<HouseTypes>().nameOfGoodsShaman.Count; i++)
			{
				newItem = Instantiate (item, item.transform.position, item.transform.rotation) as GameObject;
				if(inventoryParent == null)
				{
					inventoryParent = GameObject.Find ("CityGrid");
				}
				newItem.transform.parent = inventoryParent.transform;
				newItem.transform.localScale = new Vector3(1,1,1);
				inventoryParent.GetComponent<UIGrid>().Reposition ();
				foreach(Transform child in newItem.transform)
				{
					if(child.name == "NAME")
					{
						
						child.GetComponent<UILabel>().text = houseTypes.GetComponent<HouseTypes>().nameOfGoodsShaman[i];
					}
					/*else if(child.name == "QLT")
								{
									child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).quality.ToString ();
								}
								else if(child.name == "WEIGHT")
								{
									child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).weight.ToString () + " KG";
								}*/
					yield return new WaitForSeconds(0.001f);
				}
				yield return new WaitForSeconds(0.001f);
			}
			break;
		case "bWorkshopMech":
			newName = "Mechanical Workshop";
			cityInv.SetActive (true);
			GameObject.Find ("ChatkaName").GetComponent<UILabel>().text = newName;
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
			//yield return new WaitForSeconds(0.4f);
			for(int i = 0; i < houseTypes.GetComponent<HouseTypes>().nameOfGoodsMechanicalWorkshop.Count; i++)
			{
				newItem = Instantiate (item, item.transform.position, item.transform.rotation) as GameObject;
				if(inventoryParent == null)
				{
					inventoryParent = GameObject.Find ("CityGrid");
				}
				newItem.transform.parent = inventoryParent.transform;
				newItem.transform.localScale = new Vector3(1,1,1);
				inventoryParent.GetComponent<UIGrid>().Reposition ();
				foreach(Transform child in newItem.transform)
				{
					if(child.name == "NAME")
					{
						
						child.GetComponent<UILabel>().text = houseTypes.GetComponent<HouseTypes>().nameOfGoodsMechanicalWorkshop[i];
					}
					/*else if(child.name == "QLT")
								{
									child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).quality.ToString ();
								}
								else if(child.name == "WEIGHT")
								{
									child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).weight.ToString () + " KG";
								}*/
					yield return new WaitForSeconds(0.001f);
				}
				yield return new WaitForSeconds(0.001f);
			}
			break;
		case "bWorkshopArm":
			newName = "Arms Workshop";
			cityInv.SetActive (true);
			GameObject.Find ("ChatkaName").GetComponent<UILabel>().text = newName;
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
			//yield return new WaitForSeconds(0.4f);
			for(int i = 0; i < houseTypes.GetComponent<HouseTypes>().nameOfGoodsArmsWorkshop.Count; i++)
			{
				newItem = Instantiate (item, item.transform.position, item.transform.rotation) as GameObject;
				if(inventoryParent == null)
				{
					inventoryParent = GameObject.Find ("CityGrid");
				}
				newItem.transform.parent = inventoryParent.transform;
				newItem.transform.localScale = new Vector3(1,1,1);
				inventoryParent.GetComponent<UIGrid>().Reposition ();
				foreach(Transform child in newItem.transform)
				{
					if(child.name == "NAME")
					{
						
						child.GetComponent<UILabel>().text = houseTypes.GetComponent<HouseTypes>().nameOfGoodsArmsWorkshop[i];
					}
					/*else if(child.name == "QLT")
								{
									child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).quality.ToString ();
								}
								else if(child.name == "WEIGHT")
								{
									child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).weight.ToString () + " KG";
								}*/
					yield return new WaitForSeconds(0.001f);
				}
				yield return new WaitForSeconds(0.001f);
			}
			break;
		case "bMedic":
			newName = "Medic";
			cityInv.SetActive (true);
			GameObject.Find ("ChatkaName").GetComponent<UILabel>().text = newName;
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
			//yield return new WaitForSeconds(0.4f);
			for(int i = 0; i < houseTypes.GetComponent<HouseTypes>().nameOfGoodsMedic.Count; i++)
			{
				newItem = Instantiate (item, item.transform.position, item.transform.rotation) as GameObject;
				if(inventoryParent == null)
				{
					inventoryParent = GameObject.Find ("CityGrid");
				}
				newItem.transform.parent = inventoryParent.transform;
				newItem.transform.localScale = new Vector3(1,1,1);
				inventoryParent.GetComponent<UIGrid>().Reposition ();
				foreach(Transform child in newItem.transform)
				{
					if(child.name == "NAME")
					{
						
						child.GetComponent<UILabel>().text = houseTypes.GetComponent<HouseTypes>().nameOfGoodsMedic[i];
					}
					/*else if(child.name == "QLT")
								{
									child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).quality.ToString ();
								}
								else if(child.name == "WEIGHT")
								{
									child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).weight.ToString () + " KG";
								}*/
					yield return new WaitForSeconds(0.001f);
				}
				yield return new WaitForSeconds(0.001f);
			}
			break;
		case "bSklad":
			newName = "Warehouse";
			cityInv.SetActive (true);
			GameObject.Find ("ChatkaName").GetComponent<UILabel>().text = newName;
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
			//yield return new WaitForSeconds(0.4f);
			for(int i = 0; i < houseTypes.GetComponent<HouseTypes>().nameOfGoodsStorehouse.Count; i++)
			{
				newItem = Instantiate (item, item.transform.position, item.transform.rotation) as GameObject;
				if(inventoryParent == null)
				{
					inventoryParent = GameObject.Find ("CityGrid");
				}
				newItem.transform.parent = inventoryParent.transform;
				newItem.transform.localScale = new Vector3(1,1,1);
				inventoryParent.GetComponent<UIGrid>().Reposition ();
				foreach(Transform child in newItem.transform)
				{
					if(child.name == "NAME")
					{
						
						child.GetComponent<UILabel>().text = houseTypes.GetComponent<HouseTypes>().nameOfGoodsStorehouse[i];
					}
					/*else if(child.name == "QLT")
								{
									child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).quality.ToString ();
								}
								else if(child.name == "WEIGHT")
								{
									child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).weight.ToString () + " KG";
								}*/
					yield return new WaitForSeconds(0.001f);
				}
				yield return new WaitForSeconds(0.001f);
			}
			break;
		case "bKarawana":
			newName = "Caravan";
			cityInv.SetActive (true);
			GameObject.Find ("ChatkaName").GetComponent<UILabel>().text = newName;
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
			//yield return new WaitForSeconds(0.4f);
			for(int i = 0; i < houseTypes.GetComponent<HouseTypes>().nameOfGoodsCaravan.Count; i++)
			{
				newItem = Instantiate (item, item.transform.position, item.transform.rotation) as GameObject;
				if(inventoryParent == null)
				{
					inventoryParent = GameObject.Find ("CityGrid");
				}
				newItem.transform.parent = inventoryParent.transform;
				newItem.transform.localScale = new Vector3(1,1,1);
				inventoryParent.GetComponent<UIGrid>().Reposition ();
				foreach(Transform child in newItem.transform)
				{
					if(child.name == "NAME")
					{
						
						child.GetComponent<UILabel>().text = houseTypes.GetComponent<HouseTypes>().nameOfGoodsCaravan[i];
					}
					/*else if(child.name == "QLT")
								{
									child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).quality.ToString ();
								}
								else if(child.name == "WEIGHT")
								{
									child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).weight.ToString () + " KG";
								}*/
					yield return new WaitForSeconds(0.001f);
				}
				yield return new WaitForSeconds(0.001f);
			}
			break;
		case "bSzabry":
			newName = "Loot brotherhood";
			cityInv.SetActive (true);
			GameObject.Find ("ChatkaName").GetComponent<UILabel>().text = newName;
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
			//yield return new WaitForSeconds(0.4f);
			for(int i = 0; i < houseTypes.GetComponent<HouseTypes>().nameOfGoodsLootBrotherhood.Count; i++)
			{
				newItem = Instantiate (item, item.transform.position, item.transform.rotation) as GameObject;
				if(inventoryParent == null)
				{
					inventoryParent = GameObject.Find ("CityGrid");
				}
				newItem.transform.parent = inventoryParent.transform;
				newItem.transform.localScale = new Vector3(1,1,1);
				inventoryParent.GetComponent<UIGrid>().Reposition ();
				foreach(Transform child in newItem.transform)
				{
					if(child.name == "NAME")
					{
						
						child.GetComponent<UILabel>().text = houseTypes.GetComponent<HouseTypes>().nameOfGoodsLootBrotherhood[i];
					}
					/*else if(child.name == "QLT")
								{
									child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).quality.ToString ();
								}
								else if(child.name == "WEIGHT")
								{
									child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).weight.ToString () + " KG";
								}*/
					yield return new WaitForSeconds(0.001f);
				}
				yield return new WaitForSeconds(0.001f);
			}
			break;
		case "bGorzelnia":
			newName = "Distillery";
			cityInv.SetActive (true);
			GameObject.Find ("ChatkaName").GetComponent<UILabel>().text = newName;
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
			//yield return new WaitForSeconds(0.4f);
			for(int i = 0; i < houseTypes.GetComponent<HouseTypes>().nameOfGoodsDistillery.Count; i++)
			{
				newItem = Instantiate (item, item.transform.position, item.transform.rotation) as GameObject;
				if(inventoryParent == null)
				{
					inventoryParent = GameObject.Find ("CityGrid");
				}
				newItem.transform.parent = inventoryParent.transform;
				newItem.transform.localScale = new Vector3(1,1,1);
				inventoryParent.GetComponent<UIGrid>().Reposition ();
				foreach(Transform child in newItem.transform)
				{
					if(child.name == "NAME")
					{
						
						child.GetComponent<UILabel>().text = houseTypes.GetComponent<HouseTypes>().nameOfGoodsDistillery[i];
					}
					/*else if(child.name == "QLT")
								{
									child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).quality.ToString ();
								}
								else if(child.name == "WEIGHT")
								{
									child.GetComponent<UILabel>().text = Inventory.ElementAt (InventoryCount).weight.ToString () + " KG";
								}*/
					yield return new WaitForSeconds(0.001f);
				}
				yield return new WaitForSeconds(0.001f);
			}
			break;
		}
	}
}
