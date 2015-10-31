using UnityEngine;

public class CubeController : MonoBehaviour {

    [SerializeField] private AnimationCurve AccelerationCurve = null;

    private float Angle = 0.0f;
    private float Speed = 0.0f;

    private const float AngularSpeed = 2f;
    private const float Torque = 0.5f;
    private const float MoveSpeedTrashold = 0.01f;
    private const float CurveMultiplier = 0.005f;
    private const float TopSpeed = 0.3f;

    void Update() {
        UpdateRoverSpeed();

        UpdateRoverRotation();
    }

    private void UpdateRoverSpeed() {
        if (Input.GetKey(KeyCode.W)) {
            Speed += AccelerationCurve.Evaluate(Mathf.Abs(Speed) / TopSpeed) * CurveMultiplier;
        } else if (Input.GetKey(KeyCode.S)) {
            Speed -= AccelerationCurve.Evaluate(Mathf.Abs(Speed) / TopSpeed) * CurveMultiplier;
        } else {
            if (Mathf.Abs(Speed) < MoveSpeedTrashold) {
                Speed = 0;
            } else if (Speed > 0) {
                Speed -= Time.deltaTime * Torque;
            } else if (Speed < 0) {
                Speed += Time.deltaTime * Torque;
            }
        }

        transform.position = transform.position + new Vector3(Mathf.Sin(Angle * Mathf.Deg2Rad), 0, Mathf.Cos(Angle * Mathf.Deg2Rad)) * Speed;
    }

    private void UpdateRoverRotation() {
        if (Input.GetKey(KeyCode.A)) {
            if (Mathf.Abs(Speed) > MoveSpeedTrashold) {
                Angle += AngularSpeed * Speed > 0 ? -1 : 1;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, Angle, transform.eulerAngles.z);
            }
        }
        if (Input.GetKey(KeyCode.D)) {
            if (Mathf.Abs(Speed) > MoveSpeedTrashold) {
                Angle += AngularSpeed * Speed > 0 ? 1 : -1;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, Angle, transform.eulerAngles.z);
            }
        }
    }
}
