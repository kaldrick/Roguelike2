using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CityVariables : MonoBehaviour {
	public string chatkaName;
	public int parentNumber;
	public Dictionary<string, List<Item>> houseInventories = new Dictionary<string, List<Item>>();
	public string currentHouseName;
}
