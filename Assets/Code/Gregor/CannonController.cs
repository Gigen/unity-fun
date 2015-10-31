using UnityEngine;

public class CannonController : MonoBehaviour {

    [SerializeField] Transform FirePoint = null;
    [SerializeField] ProjectileController ProjectileControllerPrefab = null;

    private const float speed = 10.0f;
    private float hitdist = 0.0f;
    private Plane GroundPlane;

    void Start() {
        GroundPlane = new Plane(Vector3.down, transform.position);
    }

    void Update() {
        CalculateCannonRotation();

        ShootCannon();
    }

    private void CalculateCannonRotation() {
        Ray MouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (GroundPlane.Raycast(MouseRay, out hitdist)) {
            Vector3 targetPoint = MouseRay.GetPoint(hitdist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
            Vector3 targetEulerRotation = targetRotation.eulerAngles;
            transform.eulerAngles = new Vector3(transform.parent.eulerAngles.x, targetEulerRotation.y, transform.parent.eulerAngles.z);
        }
    }

    private void ShootCannon() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            ProjectileController newProjectile = Instantiate(ProjectileControllerPrefab);
            newProjectile.Fire(FirePoint ?? transform); // same as (FirePoint == null ? transform : FirePoint)
        }
    }

}
