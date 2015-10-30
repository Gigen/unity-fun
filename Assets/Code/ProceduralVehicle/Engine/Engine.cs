using UnityEngine;
using System.Collections.Generic;

public class Engine : MonoBehaviour {
	public float Displacement = 2.0f; //in litres
	public int NumberOfBanks = 2;
	public int NumberOfCylinders = 3;

	public Crankshaft Crankshaft;

	public void Awake() {
		Init (Displacement,NumberOfBanks,NumberOfCylinders);

		Crankshaft.Calculate(new Vector3(1,1,1),1,1,1);
	}

	public void Init(float displacement, int numberOfBanks, int numberOfCylinders) {
		Displacement = displacement;
		
		Crankshaft = new GameObject("Crankshaft").AddComponent<Crankshaft>();
		Crankshaft.transform.parent = transform;
		Crankshaft.Init(displacement,numberOfBanks,numberOfCylinders);
	}
}
