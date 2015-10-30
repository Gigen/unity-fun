using UnityEngine;
using System.Collections;

public class CarSuspensionJoint : MonoBehaviour {
	public Rigidbody chasisRigidbody;
	public Transform hingeTransform;
	public Transform suspensionTransform;
	private Rigidbody rigidbody;

	public float springStength = 10000f;
	public float springDamping = 5000f;
	// Use this for initialization
	private float hingeLenght;
	private float suspensionLength;

	void Start () {
		rigidbody=GetComponent<Rigidbody>();
		hingeLenght = Vector3.Distance(hingeTransform.position, transform.position);
		suspensionLength = Vector3.Distance(suspensionTransform.position, transform.position);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float currentSuspensionLength = Vector3.Distance(suspensionTransform.position, transform.position);
		Vector3 suspensionDirection = Vector3.Normalize( suspensionTransform.position - transform.position);
		float centerDisplacement = currentSuspensionLength - suspensionLength;
		Vector3 relativeSpeed = rigidbody.velocity - chasisRigidbody.velocity;
		Vector3 springForce = springStength * centerDisplacement * suspensionDirection;
		Vector3 dampingForce = springDamping * relativeSpeed.magnitude * suspensionDirection;
		rigidbody.AddForce ((springForce - dampingForce) * Time.deltaTime);
	}
}
