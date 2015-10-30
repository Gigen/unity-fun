using UnityEngine;
using System.Collections;

public class Chasis : MonoBehaviour {

	[SerializeField] private float[] _Wheelbase;
	[SerializeField] private float[] _Track; //Tracks front to back


	[SerializeField] private float _Width = 1000f;



	private Vector3[] WheelPositions;
	public Track[] Tracks;
	public Suspension[] Suspensions;

	float Wheelbase(int i) {
		return _Wheelbase[i] * Helper.MmToUU;
	}

	public float Width {
		get { return _Width * Helper.MmToUU; }
	}

	private float Track(int i) {
		return _Track[i] * Helper.MmToUU; 
	}
	void Start () {
		if(_Wheelbase.Length + 1 != _Track.Length){
			Debug.LogError("WheelBase to Track ratio not correct");
			return;
		}

		CreateCollider();
		CreateTracks();
		CreateWheels();
		//CreateSuspensionsMounts();
		//CreateSuspensions();
	}

	void CreateCollider() {
		float totalWheelbaseLenght = 0;
		for(int x = 0; x < _Wheelbase.Length; x++)
			totalWheelbaseLenght += Wheelbase(x);

		BoxCollider bc = gameObject.AddComponent<BoxCollider>();
		bc.size = new Vector3(totalWheelbaseLenght,0.2f,Width);

	}
	void CreateTracks() {
		Tracks = new Track[_Track.Length];
		float totalWheelbaseLenght = 0;
		for(int x = 0; x < _Wheelbase.Length; x++)
			totalWheelbaseLenght += Wheelbase(x);
		
		float firstTrackPosition = totalWheelbaseLenght/2;

		for (int i = 0; i < _Track.Length; i++) {
			float trackPosition = firstTrackPosition;
			for (int x = 0; x < i; x++)
				trackPosition -= Wheelbase(x);
			Track t = new GameObject("Track" + i).AddComponent<Track>();
			t.transform.parent = transform;
			t.transform.localPosition = new Vector3(trackPosition,0,0);
			t.transform.localRotation = Quaternion.identity;
			t.Width = Track(i);
			t.Chasis = this;
			t.Init();
			Tracks[i] = t;
		}
	}
	void CreateWheels() {
		for(int t = 0; t < Tracks.Length; t++) {
			Wheel wL = new GameObject("Wheel").AddComponent<Wheel>();
			wL.Mirror = true;
			/*wL.transform.parent = Tracks[t].LeftSuspension.WheelJoint;
			wL.transform.localPosition = Vector3.zero;
			wL.transform.localScale = Vector3.one;
			wL.transform.localEulerAngles = new Vector3(270,0,0);*/
			wL.transform.parent = transform.parent;
			wL.transform.localPosition = Tracks[t].LeftSuspension.WheelJoint.position;
			wL.transform.localScale = Vector3.one;
			wL.transform.localEulerAngles = new Vector3(270,0,0);


			Wheel wR = new GameObject("Wheel").AddComponent<Wheel>();
			/*wR.transform.parent = Tracks[t].RightSuspension.WheelJoint;
			wR.transform.localPosition = Vector3.zero;
			wR.transform.localScale = Vector3.one;
			wR.transform.localEulerAngles = new Vector3(270,0,0);*/
			wR.transform.parent = transform.parent;
			wR.transform.localPosition = Tracks[t].RightSuspension.WheelJoint.position;
			wR.transform.localScale = Vector3.one;
			wR.transform.localEulerAngles = new Vector3(270,0,0);

		}
	}


	void DrawDebug() {
		for(int x = 0; x < Tracks.Length-1; x++)
			Debug.DrawLine(Tracks[x].transform.position,Tracks[x+1].transform.position,Color.blue);
	}

	void Update () {
		DrawDebug();
	}
}
