using UnityEngine;
using System.Collections;

public class GateGlowController : MonoBehaviour {

	public GameObject GlowGO;
	public AnimationCurve GlowScaleAC;
	public float animationSpeed;

	private float animationTime;
	private float lastMouseEnterTime;
	private bool mouseEntered;

	void Start () {
		animationTime = 1.0f;
		mouseEntered = false;
	}

	void Update () {
		if (Time.time - lastMouseEnterTime > 0.05f) {
			mouseEntered = false;
			GlowGO.transform.localScale = Vector3.one;
		}
		if (animationTime < 1.0f) {
			float newScale = GlowScaleAC.Evaluate(animationTime);
			GlowGO.transform.localScale = new Vector3(newScale, newScale, newScale);
			animationTime += Time.deltaTime * animationSpeed;
		}
	}

	void OnMouseOver() {
		lastMouseEnterTime = Time.time;
		if (!mouseEntered)
			animationTime = 0;
		mouseEntered = true;
	}
}
