using UnityEngine;

public class CubeController : MonoBehaviour {

    private float Angle = 0.0f;
    private const float Speed = 0.05f;
    private const float AngularSpeed = 1f;

    void Update() {
        if (Input.GetKey(KeyCode.W)) {
            transform.position = transform.position + new Vector3(Mathf.Sin(Angle * Mathf.Deg2Rad), 0, Mathf.Cos(Angle * Mathf.Deg2Rad)) * Speed;
        }
        if (Input.GetKey(KeyCode.A)) {
            Angle -= AngularSpeed;
            transform.eulerAngles = new Vector3(0, Angle, 0);
        }
        if (Input.GetKey(KeyCode.S)) {
            transform.position = transform.position - new Vector3(Mathf.Sin(Angle * Mathf.Deg2Rad), 0, Mathf.Cos(Angle * Mathf.Deg2Rad)) * Speed;
        }
        if (Input.GetKey(KeyCode.D)) {
            Angle += AngularSpeed;
            transform.eulerAngles = new Vector3(0, Angle, 0);
        }

    }
}
