using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class HouseTypes : MonoBehaviour {
	public enum houseTypes {Chief, Farm, Sawmill, Hunter, Shaman, MechanicalWorkshop, ArmsWorkshop, Medic, Storehouse, Caravan, LootBrotherhood, Distillery};
	public houseTypes typeOfHouse;
	public List<string> nameOfGoodsChief = new List<string>();
	public List<int> basicPriceForGoodsChief = new List<int>();

	public List<string> nameOfGoodsFarm = new List<string>();
	public List<int> basicPriceForGoodsFarm = new List<int>();

	public List<string> nameOfGoodsSawmill = new List<string>();
	public List<int> basicPriceForGoodsSawmill = new List<int>();

	public List<string> nameOfGoodsHunter = new List<string>();
	public List<int> basicPriceForGoodsHunter = new List<int>();

	public List<string> nameOfGoodsShaman = new List<string>();
	public List<int> basicPriceForGoodsShaman = new List<int>();

	public List<string> nameOfGoodsMechanicalWorkshop = new List<string>();
	public List<int> basicPriceForGoodsMechanicalWorkshop = new List<int>();

	public List<string> nameOfGoodsArmsWorkshop = new List<string>();
	public List<int> basicPriceForGoodsArmsWorkshop = new List<int>();

	public List<string> nameOfGoodsMedic = new List<string>();
	public List<int> basicPriceForGoodsMedic = new List<int>();

	public List<string> nameOfGoodsStorehouse = new List<string>();
	public List<int> basicPriceForGoodsStorehouse = new List<int>();

	public List<string> nameOfGoodsCaravan = new List<string>();
	public List<int> basicPriceForGoodsCaravan = new List<int>();

	public List<string> nameOfGoodsLootBrotherhood = new List<string>();
	public List<int> basicPriceForGoodsLootBrotherhood = new List<int>();

	public List<string> nameOfGoodsDistillery = new List<string>();
	public List<int> basicPriceForGoodsDistillery = new List<int>();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
