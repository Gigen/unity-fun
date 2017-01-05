using UnityEngine;
using System.Collections;

public class Chasis : MonoBehaviour {

	[SerializeField] private float[] _Wheelbase;
	[SerializeField] private float[] _Track; //Tracks front to back
    
	[SerializeField] private float _Width = 1000f;

    public Rigidbody Rigidbody;
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


	void DrawDebug() {
		for(int x = 0; x < Tracks.Length-1; x++)
			Debug.DrawLine(Tracks[x].transform.position,Tracks[x+1].transform.position,Color.blue);
	}

	void Update () {
		DrawDebug();
	}
}
