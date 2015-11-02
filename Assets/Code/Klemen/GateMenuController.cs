using UnityEngine;
using System.Collections;

public class GateMenuController : MonoBehaviour {

	public GameObject menu;
	private RectTransform rectTransform;

	void Start () {
		rectTransform = menu.GetComponent<RectTransform> ();
		menu.SetActive (false);
	}

	void Update() {
		if(Input.GetMouseButtonDown(1)) {
			RaycastHit hit;
			Vector3 raycastPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1));
			if(Physics.Raycast(raycastPos, Vector3.forward, out hit)) {
				if(hit.transform.gameObject.tag == "Gate") {
					ShowMenu(hit.transform.gameObject);
				}
			}
		}
	}

	public void ShowMenu(GameObject gate) {
		rectTransform.position = Camera.main.WorldToScreenPoint (gate.transform.position);
		rectTransform.sizeDelta = new Vector2 (80, 130);

		menu.SetActive (true);
	}

}
