using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RoomXYContainer : MonoBehaviour {
	public DungeonGenerator Dungeon;
	public List<Room> rooms;
	public List<float> X;
	public List<float> Y;
	public int i = 0;
	public void Awake()
	{
	}
}
