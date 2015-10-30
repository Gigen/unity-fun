using UnityEngine;
using System.Collections.Generic;

public class Crankshaft : MonoBehaviour {

	public struct CrankshaftVariation {
		public string name;
		public int numBanks;
		public float[] rotations;
		public CrankshaftVariation(string varName, int nBanks, float[] rot) {
			name = varName;
			numBanks = nBanks;
			rotations = rot;
		}
	}
	static CrankshaftVariation[] CrankshaftVariations = new CrankshaftVariation[] {
		new CrankshaftVariation("Inline",		1,	new float[]{0}),
		new CrankshaftVariation("V60",			2,	new float[]{-30,30}),
		new CrankshaftVariation("V90",			2,	new float[]{-45,45}),
		new CrankshaftVariation("V120",			2,	new float[]{-60,60}),
		new CrankshaftVariation("Flat",			2,	new float[]{-90,90}),
		new CrankshaftVariation("W120",			3,	new float[]{-60,0,60}),
		new CrankshaftVariation("X60",			4,	new float[]{-30,30,150,-150})
	};

	[SerializeField] private float Displacement;
	[SerializeField] private List<Bank> Banks;

	public void Init(float displacement, int numberOfBanks, int numberOfCylinders) {
		Displacement = displacement;

		Banks = new List<Bank>();
		for (int i = 0; i < numberOfBanks; i++) {
			Bank b = new GameObject("Bank"+i).AddComponent<Bank>();
			b.transform.parent = transform;
			b.Init (displacement/numberOfBanks, numberOfCylinders/numberOfBanks);
			Banks.Add(b);
		}
	}

	public static void Calculate(Vector3 size, float torque, float weight, float cost) {
		for (int i = 0; i < CrankshaftVariations.Length; i++) {
			//some rules:
			// - all banks are same

			Vector2[] bankVectors = new Vector2[CrankshaftVariations[i].numBanks];
			//Calculate normalized max size of engine from crankshaft
			Vector2 normalizedMinMaxX = new Vector2(-0.05f,0.05f); //crankshaft takes 10% of space
			Vector2 normalizedMinMaxY = new Vector2(-0.05f,0.05f); //crankshaft takes 10% of space
			for (int bank = 0; bank < CrankshaftVariations[i].numBanks; bank++) {
				Vector2 normalizedBankVector = new Vector2(1,0);
				float bankAngle = Mathf.Deg2Rad*CrankshaftVariations[i].rotations[bank];
				float x = Mathf.Sin(bankAngle);
				float y = Mathf.Cos(bankAngle);
				bankVectors[bank] = new Vector2(x,y);
				if (x < normalizedMinMaxX.x) normalizedMinMaxX.x = x;
				if (x > normalizedMinMaxX.y) normalizedMinMaxX.y = x;
				if (y < normalizedMinMaxY.x) normalizedMinMaxY.x = y;
				if (y > normalizedMinMaxY.y) normalizedMinMaxY.y = y;

				//fix because of precision
				if (Mathf.Abs(normalizedMinMaxX.x) < 0.001f) normalizedMinMaxX.x = 0f;
				if (Mathf.Abs(normalizedMinMaxX.y) < 0.001f) normalizedMinMaxX.y = 0f;
				if (Mathf.Abs(normalizedMinMaxY.x) < 0.001f) normalizedMinMaxY.x = 0f;
				if (Mathf.Abs(normalizedMinMaxY.y) < 0.001f) normalizedMinMaxY.y = 0f;
			}
			float spread = normalizedMinMaxX.y - normalizedMinMaxX.x;
			normalizedMinMaxX.x = normalizedMinMaxX.x/spread;
			normalizedMinMaxX.y = normalizedMinMaxX.y/spread;
			spread = normalizedMinMaxY.y - normalizedMinMaxY.x;
			normalizedMinMaxY.x = normalizedMinMaxY.x/spread;
			normalizedMinMaxY.y = normalizedMinMaxY.y/spread;

			float maxBankHeight = float.PositiveInfinity;
			Vector2 crankshaft = Vector2.zero;
			Rect engineRect = new Rect(normalizedMinMaxX.x*size.x,normalizedMinMaxY.y*size.y,size.x,size.y);
			for (int bank = 0; bank < CrankshaftVariations[i].numBanks; bank++) {
				float t1 = (engineRect.xMin-crankshaft.x)/bankVectors[bank].x;
				float t2 = (engineRect.xMax-crankshaft.x)/bankVectors[bank].x;
				float t3 = (engineRect.yMin-crankshaft.y)/bankVectors[bank].y;
				float t4 = (engineRect.yMax-crankshaft.y)/bankVectors[bank].y;
				//Debug.Log("ENGINE"+engineRect.xMin + " " + engineRect.xMax + " " + engineRect.yMin + " " + engineRect.yMax);
				//Debug.Log(t1 + " " + t2 + " " + t3 + " " + t4);
				float t = float.PositiveInfinity;
				if (t1 > 0 && t1 < t) t = t1;
				if (t2 > 0 && t2 < t) t = t2;
				if (t3 > 0 && t3 < t) t = t3;
				if (t4 > 0 && t4 < t) t = t4;

				Vector2 bankIntersect = new Vector2(crankshaft.x+t*bankVectors[bank].x,crankshaft.y+t*bankVectors[bank].y);
				float bankHeight = Vector2.Distance(crankshaft,bankIntersect);
				if (bankHeight < maxBankHeight) maxBankHeight = bankHeight;

				//Debug.LogWarning(CrankshaftVariations[i].rotations[bank] + " " + bankIntersect.x + " " + bankIntersect.y + " ");
			}
			Debug.LogError("Max bank height " + CrankshaftVariations[i].name + " "  + maxBankHeight);

			Bank.Calculate(bankVectors, maxBankHeight, engineRect, crankshaft, size.z, CrankshaftVariations[i]);
			//Debug.LogError(CrankshaftVariations[i].name + " X" + normalizedMinMaxX + " Y" + normalizedMinMaxY.x + " " + normalizedMinMaxY.y);

		}
	}

}
