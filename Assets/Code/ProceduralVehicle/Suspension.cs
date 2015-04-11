using UnityEngine;
using System.Collections;

[System.Serializable]
public class SuspensionMount {
	public Transform LowerSuspensionMount;
	public Transform UpperSuspensionMount;
}

public class Suspension : MonoBehaviour {

	// Use this for initialization
	[SerializeField] private float _SuspensionLength = 475f;
	[SerializeField] private float _JointToWheelDistance = 205f;
	
	public Track Track;
	public SuspensionMount SuspensionMount;
	private Transform ArmSpringJoint;
	public Transform WheelJoint;
	public float Mirror = 1;

	float JointToWheelDistance {
		get { return _JointToWheelDistance * Helper.MmToUU; }
	}
	
	float SuspensionLength {
		get { return _SuspensionLength * Helper.MmToUU; }
	}

	float ArmLength {
		get { return (Track.Width/2) - (Track.Chasis.Width/2) - JointToWheelDistance; }
	}

	public void Init () {
		CreateJoints();
	}

	private void CreateJoints() {
		ArmSpringJoint = new GameObject("ArmSpringJoint").transform;
		WheelJoint = new GameObject("WheelJoint").transform;
		WheelJoint.parent = ArmSpringJoint;
		WheelJoint.localPosition = new Vector3(0,0,JointToWheelDistance*Mirror);
		ArmSpringJoint.parent = transform;
		UpdateSuspension();
	}
	private void UpdateSuspension() {
		WheelJoint.localPosition = new Vector3(0,0,JointToWheelDistance*Mirror);
		Vector2 i1,i2;
		if (!Helper.FindCircleCircleIntersections(SuspensionMount.UpperSuspensionMount.position.y,SuspensionMount.UpperSuspensionMount.position.z,SuspensionLength,
		                                          SuspensionMount.LowerSuspensionMount.position.y,SuspensionMount.LowerSuspensionMount.position.z,ArmLength,
		                                          out i1, out i2)) {
			return;
		}
		Vector3 intersect;
		if (i1.x > i2.x) // pick lower intersection one
			intersect = new Vector3(Track.transform.position.x,i2.x,i2.y);
		else
			intersect = new Vector3(Track.transform.position.x,i1.x,i1.y);
		
		ArmSpringJoint.transform.position = intersect;
	}

	private void DrawDebug() {
		Debug.DrawLine (ArmSpringJoint.transform.position, SuspensionMount.UpperSuspensionMount.transform.position, Color.red);
		Debug.DrawLine (ArmSpringJoint.transform.position, SuspensionMount.LowerSuspensionMount.transform.position, Color.gray);
		Debug.DrawLine (ArmSpringJoint.transform.position, WheelJoint.transform.position, Color.gray);
	}
	// Update is called once per frame
	void Update () {
		UpdateSuspension();
		DrawDebug();
	}
}
