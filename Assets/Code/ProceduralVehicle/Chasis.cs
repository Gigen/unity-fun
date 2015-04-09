using UnityEngine;
using System.Collections;

public class Chasis : MonoBehaviour {

	[SerializeField] private float[] _Wheelbase;
	[SerializeField] private float[] _Track; //Tracks front to back
	private Vector3[] WheelPositions; //Tracks front to back

	float Wheelbase(int i) {
		return _Wheelbase[i] * Helper.MmToUU;
	
	}float Track(int i) {
		return _Track[i] * Helper.MmToUU;
	}

	void Start () {
		CalculateWheelPositions();

		for (int i = 0; i < WheelPositions.Length; i++) {
			GameObject wheel = new GameObject("Wheel");
			wheel.transform.position = WheelPositions[i];
			wheel.transform.localEulerAngles = new Vector3(270,0,0);
			wheel.transform.parent = transform;
			Wheel w = wheel.AddComponent<Wheel>();
			if (i%2 == 1)
				w.Mirror = true;
		}
	}
	
	// Update is called once per frame
	bool CalculateWheelPositions() {
		if(_Wheelbase.Length + 1 != _Track.Length){
			Debug.LogError("WheelBase to Track ratio not correct");
			return false;
		}
		WheelPositions = new Vector3[(_Wheelbase.Length+1)*2];

		float totalWheelbaseLenght = 0;
		for(int x = 0; x < _Wheelbase.Length; x++)
			totalWheelbaseLenght += Wheelbase(x);
		
		float firstTrackPosition = totalWheelbaseLenght/2;

		for (int t = 0; t < _Track.Length; t++) {
			float trackPosition = firstTrackPosition;
			for (int i = 0; i < t; i++)
				trackPosition -= Wheelbase(i);

			WheelPositions[t*2 + 0] = new Vector3(trackPosition, 0 , -Track(t)/2);
			WheelPositions[t*2 + 1] = new Vector3(trackPosition, 0 , Track(t)/2);
		}
		return true;
	}

	void DrawDebug() {
		float totalWheelbaseLenght = 0;
		for(int x = 0; x < _Wheelbase.Length; x++)
			totalWheelbaseLenght += Wheelbase(x);
		Debug.DrawLine(new Vector3(totalWheelbaseLenght/2,0,0),new Vector3(-totalWheelbaseLenght/2,0,0),Color.blue);

		for(int i = 0; i < WheelPositions.Length; i++) {
			Debug.DrawLine(WheelPositions[i], new Vector3(WheelPositions[i].x,0,0));
		}
	}

	void Update () {
		if (CalculateWheelPositions())
			DrawDebug();
	}
}
