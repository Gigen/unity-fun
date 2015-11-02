using UnityEngine;
using System.Collections;

public class ViewController : MonoBehaviour {

	public float MouseSensitivity;

	private bool isMovementActive;
	private GameObject grabbedObject;
	private Vector2 lastMousePosition;
	private Vector2 mouseDelta;

	void Start () {
		isMovementActive = false;
		grabbedObject = null;
	}

	void Update () {
		mouseDelta = lastMousePosition - new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		lastMousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

		if (Input.GetKeyDown (KeyCode.Space)) {
			if (Input.GetMouseButton(0)) return;
			isMovementActive = true;
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			isMovementActive = false;
		}

		if (isMovementActive) {
			if (Input.GetMouseButtonDown (0)) {
				lastMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			}
			if (Input.GetMouseButton (0)) {
				Camera.main.transform.position += new Vector3 (mouseDelta.x / MouseSensitivity, mouseDelta.y / MouseSensitivity, 0);
			}

			if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
				Camera.main.orthographicSize = Mathf.Max (Camera.main.orthographicSize + 0.5f, 1);
			}
			if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
				Camera.main.orthographicSize = Mathf.Max (Camera.main.orthographicSize - 0.5f, 1);
			}
		}
		else {
			if(Input.GetMouseButtonDown(0)) {
				RaycastHit hit;
				Vector3 raycastPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1));
				if(Physics.Raycast(raycastPos, Vector3.forward, out hit)) {
					if(hit.transform.gameObject.tag == "Gate") {
						grabbedObject = hit.transform.gameObject;
					}
				}
			}
			if(Input.GetMouseButtonUp(0)) {
				if(grabbedObject != null) {
					grabbedObject = null;
				}
			}
		}

		if (grabbedObject != null) {
			Vector3 newGatePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
			newGatePos.z = grabbedObject.transform.position.z;

			if(Input.GetKey(KeyCode.LeftControl)) {
				newGatePos.x = Mathf.Round(newGatePos.x);
				newGatePos.y = Mathf.Round(newGatePos.y);
			}

			grabbedObject.transform.position = newGatePos;
		}
	}
}
