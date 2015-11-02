using UnityEngine;

public class MainCameraController : MonoBehaviour {

    [SerializeField] private bool IsBirdView = false;

    public Transform FollowingTransform = null;

    private Vector3 OffsetVector = new Vector3(6, 10, -6);
    private Vector3 BirdViewOffsetVector = new Vector3(0, 10, 0);

    private Vector3 Angle = new Vector3(45, 315, 0);
    private Vector3 BirdViewAngle = new Vector3(90, 315, 0);

    void Start() {
        transform.eulerAngles = IsBirdView ? BirdViewAngle : Angle;
    }

    void Update() {
        if (FollowingTransform != null) {
            if (IsBirdView) {
                transform.position = FollowingTransform.position + BirdViewOffsetVector;
            } else {
                transform.position = FollowingTransform.position + OffsetVector;
            }
        }
    }
}
