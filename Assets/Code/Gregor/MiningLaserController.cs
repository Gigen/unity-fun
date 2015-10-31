using UnityEngine;

public class MiningLaserController : MonoBehaviour {

    [SerializeField] private int LaserPower = 10;
    [SerializeField] private float LaserRange = 10;
    [SerializeField] private Transform FirePoint = null;
    [SerializeField] private LaserController LaserPrefab = null;

    private const float LaserTickDelay = 0.3f;
    private float LaserDelay = 0.0f;

    void Update() {
        if (Input.GetKey(KeyCode.Mouse1) && LaserDelay <= 0) {
            LaserDelay = LaserTickDelay;
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            MineMineral(mouseRay);
        } else {
            LaserDelay -= Time.deltaTime;
        }
    }

    public void MineMineral(Ray ray) {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider != null) {
                MineralController mineralController = hit.collider.GetComponent<MineralController>();
                if (mineralController != null) {
                    Vector3 dist = mineralController.transform.position - transform.position;
                    if (dist.magnitude < LaserRange) {
                        FireLaser(mineralController);
                    }
                }
            }
        }
    }

    private void FireLaser(MineralController mineralController) {
        int minerals = mineralController.GetMinerals(LaserPower);
        MainGregorLogic.Instance.PlayerController.Minerals += minerals;
        LaserController laserController = Instantiate(LaserPrefab);
        laserController.SetLaser(FirePoint, mineralController.transform);
    }
}