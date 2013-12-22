using UnityEngine;
using System.Collections;

public class Item{
	public string name;
	public int quality;
	public float weight;
	public Item(string dName, int dQuality, float dWeight)
	{
		name = dName;
		quality = dQuality;
		weight = dWeight;
	}
}