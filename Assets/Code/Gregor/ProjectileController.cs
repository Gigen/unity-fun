using UnityEngine;

public class ProjectileController : MonoBehaviour {

    [SerializeField] private float ProjectileSpeed = 1.0f;
    [SerializeField] private float LifeTime = 1.0f;

    private float LifeTimeLeft;
    private bool IsFired;


    public void Fire(Transform spawn) {
        transform.position = spawn.position;
        transform.rotation = spawn.rotation;
        FireProjectile();
    }

    private void FireProjectile() {
        LifeTimeLeft = LifeTime;
        IsFired = true;
    }

    private void RemoveProjectile() {
        Destroy(gameObject);
    }

    void Update() {
        if (IsFired) {
            LifeTimeLeft -= Time.deltaTime;
            if (LifeTimeLeft < 0) {
                IsFired = false;
                RemoveProjectile();
            }

            transform.position += (new Vector3(Mathf.Sin(transform.eulerAngles.y * Mathf.Deg2Rad), 0, Mathf.Cos(transform.eulerAngles.y * Mathf.Deg2Rad)) * ProjectileSpeed);
        }   
    }


}
