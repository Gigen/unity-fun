using UnityEngine;
using System.Collections.Generic;

public class Bank : MonoBehaviour {
	private struct BankVariation {
		public string name;
		public int lines;
		public BankVariation(string varName, int line) {
			name = varName;
			lines = line;
		}
	}
	BankVariation[] BankVariations = new BankVariation[] {
		new BankVariation("Straight", 1),
		new BankVariation("VR", 2)
	};
	
	[SerializeField] private float Displacement;
	[SerializeField] private List<Cylinder> Cylinders;

	public void Init(float displacement, int numberOfCylinders) {
		Displacement = displacement;
		
		Cylinders = new List<Cylinder>();
		for (int i = 0; i < numberOfCylinders; i++) {
			Cylinder c = new GameObject("Cylinder"+i).AddComponent<Cylinder>();
			c.transform.parent = transform;
			c.Init(displacement/numberOfCylinders);
			Cylinders.Add(c);
		}
	}

	public static void Calculate(Vector2[] bankVectors, float maxBankHeight, Rect engineRect, Vector2 crankshaft, float engineDepth, Crankshaft.CrankshaftVariation crankshaftVariation) {
		// target is to find optimal bank size
		// rules:
		// - cylinder block to cylinder head ratio is constant 4:3

		float StrokeToHeadRatio = 1f;
		float OptimalRodToStrokeRatio = 1.75f;

		float OptimalRodToStrokePlusHeadRatio = OptimalRodToStrokeRatio / (StrokeToHeadRatio*2f);

		//find most problematic bank
		int bankIndex = -1;
		float smallestAngleDifference = float.PositiveInfinity;
		if (crankshaftVariation.numBanks < 3) {
			bankIndex = 0;
			if (crankshaftVariation.numBanks == 2)
				smallestAngleDifference = Mathf.Abs(crankshaftVariation.rotations[0] - crankshaftVariation.rotations[1]);
		} else {
			smallestAngleDifference = float.PositiveInfinity;
			for (int bank = 0; bank < crankshaftVariation.numBanks; bank++) {
				float bankRotation = crankshaftVariation.rotations[bank];
				Debug.Log (bank);
				float leftBankDifference = Mathf.Abs(bankRotation - crankshaftVariation.rotations[(bank-1)%crankshaftVariation.numBanks]);
				float rightBankDifference = Mathf.Abs(bankRotation - crankshaftVariation.rotations[(bank+1)%crankshaftVariation.numBanks]);
				if (leftBankDifference < smallestAngleDifference){
					bankIndex = bank;
					smallestAngleDifference = leftBankDifference;
				}
				if (rightBankDifference < smallestAngleDifference) {
					bankIndex = bank;
					smallestAngleDifference = rightBankDifference;
				}
			}
		}
		// get width at optimal rod:stroke+head ratio
		float rodUnit = 1f;
		float strokePlusHeadUnit = rodUnit * OptimalRodToStrokePlusHeadRatio;

		float rodHeight = rodUnit / (strokePlusHeadUnit + rodUnit) * maxBankHeight;
		float strokePlusHeadHeight = maxBankHeight - rodHeight;
		float stroke = (1/(1 + 1*StrokeToHeadRatio)) * strokePlusHeadHeight;
		float headHeight = strokePlusHeadHeight-stroke;

		float bankWidth = Mathf.Tan(Mathf.Deg2Rad*(smallestAngleDifference/2)) * rodHeight * 2;
		if (float.IsNaN(bankWidth) || bankWidth > engineRect.width)
			bankWidth = engineRect.width;

		// cylinderDiameter = (bore*1.05f)
		// straight
		// walls re 2/3 of 1 cylinder
		// depth = (numCylinders + (cylinderDiameter*0.667f)) * cylinderDiameter;
		// width = (1 + (cylinderDiameter*0.667f)) * cylinderDiameter
		// width = 0.667*cylinderDiameter*cylinderDiameter + cylinderDiameter

		//Debug.LogWarning("Bank: " + engineDepth + " " + bankWidth);  
		//Debug.LogError(crankshaftVariation.name + " optimal cylinders straigth: " + numCylinders);  

	}
}
