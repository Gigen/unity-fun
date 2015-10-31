using UnityEngine;

public class LaserController : MonoBehaviour {

    [SerializeField] private float LifeTime = 0.1f;
    [SerializeField] private float LaserWidth = 0.1f;

    private float LifeTimeLeft;
    private LineRenderer LineRenderer;
    private Transform StartTransform;
    private Transform EndTransform;

    void Update() {
        if (LifeTimeLeft > 0) {
            LifeTimeLeft -= Time.deltaTime;
            UpdateLaser();
        } else {
            Destroy(gameObject);
        }
    }

    public void SetLaser(Transform startTransform, Transform endTransform) {
        LineRenderer = GetComponent<LineRenderer>();
        LineRenderer.SetWidth(LaserWidth, LaserWidth);
        StartTransform = startTransform;
        EndTransform = endTransform;
        LifeTimeLeft = LifeTime;
        UpdateLaser();
    }

    private void UpdateLaser() {
        LineRenderer.SetPosition(0, StartTransform.position);
        LineRenderer.SetPosition(1, EndTransform.position);
    }

}
