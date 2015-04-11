using UnityEngine;
using System.Collections;

public class Track : MonoBehaviour {

	[SerializeField] private Vector3 _UpperSuspensionMount = new Vector3(0,300,240);
	private float _Width;
	public SuspensionMount LeftSuspensionMount;
	public SuspensionMount RightSuspensionMount;
	public Suspension LeftSuspension;
	public Suspension RightSuspension;

	public Chasis Chasis;

	public float Width {
		get { return _Width * Helper.MmToUU; }
		set { _Width = value / Helper.MmToUU; }
	}

	Vector3 UpperSuspensionMount {
		get { return _UpperSuspensionMount * Helper.MmToUU; }
	}

	// Use this for initialization
	public void Init () {
		CreateSuspensionsMounts();
		CreateSuspensions();
	}

	void CreateSuspensionsMounts() {
		RightSuspensionMount = new SuspensionMount();
		RightSuspensionMount.LowerSuspensionMount = new GameObject().transform;
		RightSuspensionMount.LowerSuspensionMount.gameObject.name = "LowerSuspensionMountRight";
		RightSuspensionMount.LowerSuspensionMount.parent = transform;
		RightSuspensionMount.LowerSuspensionMount.localPosition = new Vector3(0, 0 , -Chasis.Width/2);
		RightSuspensionMount.LowerSuspensionMount.localRotation = Quaternion.identity;
		RightSuspensionMount.UpperSuspensionMount = new GameObject().transform;
		RightSuspensionMount.UpperSuspensionMount.gameObject.name = "UpperSuspensionMountRight";
		RightSuspensionMount.UpperSuspensionMount.parent = transform;
		Vector3 upperSuspensionMount = UpperSuspensionMount;
		upperSuspensionMount.z *= -1;
		RightSuspensionMount.UpperSuspensionMount.localPosition = RightSuspensionMount.LowerSuspensionMount.localPosition + upperSuspensionMount;
		RightSuspensionMount.UpperSuspensionMount.localRotation = Quaternion.identity;
		
		LeftSuspensionMount = new SuspensionMount();
		LeftSuspensionMount.LowerSuspensionMount = new GameObject().transform;
		LeftSuspensionMount.LowerSuspensionMount.gameObject.name = "LowerSuspensionMountLeft";
		LeftSuspensionMount.LowerSuspensionMount.parent = transform;
		LeftSuspensionMount.LowerSuspensionMount.localPosition = new Vector3(0, 0 , Chasis.Width/2);
		LeftSuspensionMount.LowerSuspensionMount.localRotation = Quaternion.identity;
		LeftSuspensionMount.UpperSuspensionMount = new GameObject().transform;
		LeftSuspensionMount.UpperSuspensionMount.gameObject.name = "UpperSuspensionMountLeft";
		LeftSuspensionMount.UpperSuspensionMount.parent = transform;
		LeftSuspensionMount.UpperSuspensionMount.localPosition = LeftSuspensionMount.LowerSuspensionMount.localPosition + UpperSuspensionMount;
		LeftSuspensionMount.UpperSuspensionMount.localRotation = Quaternion.identity;
	}
	
	void CreateSuspensions() {
		LeftSuspension = new GameObject("SuspensionLeft").AddComponent<Suspension>();
		LeftSuspension.transform.parent = transform;
		LeftSuspension.transform.localPosition = LeftSuspensionMount.LowerSuspensionMount.localPosition;
		LeftSuspension.transform.localRotation = Quaternion.identity;
		LeftSuspension.SuspensionMount = LeftSuspensionMount;
		LeftSuspension.Track = this;
		LeftSuspension.Init();

		RightSuspension = new GameObject("SuspensionRight").AddComponent<Suspension>();
		RightSuspension.transform.parent = transform;
		RightSuspension.transform.localPosition = RightSuspensionMount.LowerSuspensionMount.localPosition;
		RightSuspension.transform.localRotation = Quaternion.identity;
		RightSuspension.SuspensionMount = RightSuspensionMount;
		RightSuspension.Mirror = -1f;
		RightSuspension.Track = this;
		RightSuspension.Init();
	}

	public void DrawDebug() {
		Debug.DrawLine(LeftSuspensionMount.LowerSuspensionMount.position, RightSuspensionMount.LowerSuspensionMount.position,Color.gray);
		Debug.DrawLine(LeftSuspensionMount.LowerSuspensionMount.position, LeftSuspensionMount.UpperSuspensionMount.position,Color.yellow);
		Debug.DrawLine(RightSuspensionMount.LowerSuspensionMount.position, RightSuspensionMount.UpperSuspensionMount.position,Color.yellow);
	}

	private void UpdateChasis() {
		//Suspension position
		LeftSuspension.transform.localPosition = LeftSuspensionMount.LowerSuspensionMount.localPosition;
		RightSuspension.transform.localPosition = RightSuspensionMount.LowerSuspensionMount.localPosition;
		//Suspension mounts
		LeftSuspensionMount.LowerSuspensionMount.localPosition = new Vector3(0, 0 , Chasis.Width/2);
		LeftSuspensionMount.UpperSuspensionMount.localPosition = LeftSuspensionMount.LowerSuspensionMount.localPosition + UpperSuspensionMount;
		RightSuspensionMount.LowerSuspensionMount.localPosition = new Vector3(0, 0 , -Chasis.Width/2);
		Vector3 upperSuspensionMount = UpperSuspensionMount;
		upperSuspensionMount.z *= -1;
		RightSuspensionMount.UpperSuspensionMount.localPosition = RightSuspensionMount.LowerSuspensionMount.localPosition + upperSuspensionMount;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateChasis();
		DrawDebug();
	}
}
