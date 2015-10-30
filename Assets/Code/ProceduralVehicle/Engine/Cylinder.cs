using UnityEngine;
using System.Collections.Generic;

public class Cylinder : MonoBehaviour {

	[SerializeField] private float Displacement;

	public void Init(float displacement) {
		Displacement = displacement;
	}


	public static void Calculate(Vector3 size, float torque, float weight, float cost) {
		
	}
}
