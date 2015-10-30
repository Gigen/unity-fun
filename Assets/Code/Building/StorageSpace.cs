using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Resource {
	WATER = 0,
	GOLD = 1
}

public class ResourceData {
	public static Color GetResourceColor(Resource r) {
		switch(r) {
		case Resource.GOLD:
			return Color.yellow;
			break;
		case Resource.WATER:
			return Color.blue;
			break;
		}
		return Color.black;
	}
}

public class StorageSpace : MonoBehaviour {
	public Resource Resource;
	public float Capacity = 10f;
	public float Stored = 0f;
	public float maxInputs = 1;
	public float maxOutputs = 1;
	public float inputSpeed = 1;
	public float outputSpeed = 1;
	public List<Building> BuildingInput = new List<Building>();
	public List<Building> BuildingOutput = new List<Building>();
}
