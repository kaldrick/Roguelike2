using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class HouseTypes : MonoBehaviour {
	public enum houseTypes {Chief, Farm, Sawmill, Hunter, Shaman, MechanicalWorkshop, ArmsWorkshop, Medic, Storehouse, Caravan, LootBrotherhood, Distillery};
	public houseTypes typeOfHouse;
	public Dictionary<string, List<Item>> houses = new Dictionary<string, List<Item>>();
	public List<Item> nameOfGoodsChief = new List<Item>();
	public List<int> basicPriceForGoodsChief = new List<int>(); 

	public List<Item> nameOfGoodsFarm = new List<Item>();
	public List<int> basicPriceForGoodsFarm = new List<int>();

	public List<Item> nameOfGoodsSawmill = new List<Item>();
	public List<int> basicPriceForGoodsSawmill = new List<int>();

	public List<Item> nameOfGoodsHunter = new List<Item>();
	public List<int> basicPriceForGoodsHunter = new List<int>();

	public List<Item> nameOfGoodsShaman = new List<Item>();
	public List<int> basicPriceForGoodsShaman = new List<int>();

	public List<Item> nameOfGoodsMechanicalWorkshop = new List<Item>();
	public List<int> basicPriceForGoodsMechanicalWorkshop = new List<int>();

	public List<Item> nameOfGoodsArmsWorkshop = new List<Item>();
	public List<int> basicPriceForGoodsArmsWorkshop = new List<int>();

	public List<Item> nameOfGoodsMedic = new List<Item>();
	public List<int> basicPriceForGoodsMedic = new List<int>();

	public List<Item> nameOfGoodsStorehouse = new List<Item>();
	public List<int> basicPriceForGoodsStorehouse = new List<int>();

	public List<Item> nameOfGoodsCaravan = new List<Item>();
	public List<int> basicPriceForGoodsCaravan = new List<int>();

	public List<Item> nameOfGoodsLootBrotherhood = new List<Item>();
	public List<int> basicPriceForGoodsLootBrotherhood = new List<int>();

	public List<Item> nameOfGoodsDistillery = new List<Item>();
	public List<int> basicPriceForGoodsDistillery = new List<int>();

	// Use this for initialization
	void Start () {
		houses.Add ("nameOfGoodsChief", nameOfGoodsChief);
		houses.Add ("nameOfGoodsFarm", nameOfGoodsFarm);
		houses.Add ("nameOfGoodsSawmill", nameOfGoodsSawmill);
		houses.Add ("nameOfGoodsHunter", nameOfGoodsHunter);
		houses.Add ("nameOfGoodsShaman", nameOfGoodsShaman);
		houses.Add ("nameOfGoodsMechanicalWorkshop", nameOfGoodsMechanicalWorkshop);
		houses.Add ("nameOfGoodsArmsWorkshop", nameOfGoodsArmsWorkshop);
		houses.Add ("nameOfGoodsMedic", nameOfGoodsMedic);
		houses.Add ("nameOfGoodsStorehouse", nameOfGoodsStorehouse);
		houses.Add ("nameOfGoodsCaravan", nameOfGoodsCaravan);
		houses.Add ("nameOfGoodsLootBrotherhood", nameOfGoodsLootBrotherhood);
		houses.Add ("nameOfGoodsDistillery", nameOfGoodsDistillery);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
