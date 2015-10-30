using UnityEngine;

public class MainCameraController : MonoBehaviour {

    public Transform FollowingTransform = null;

    private Vector3 OffsetVector = new Vector3(5, 5, -5);

    void Start() {
	
    }

    void Update() {
        if (FollowingTransform != null) {
            transform.position = FollowingTransform.position + OffsetVector;
        }
    }
}
