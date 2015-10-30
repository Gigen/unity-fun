using UnityEngine;
using System.Collections;

public class PElement : MonoBehaviour {

	public string name = "Unknown";

	public void Awake() {
		OnInit();
	}
	public virtual void OnInit() {}
}
