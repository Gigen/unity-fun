using UnityEngine;

public class MineralController : MonoBehaviour {
   
    [SerializeField] private int MaxMineralCapacity = 100;

    private int MineralCapacity;

    void Start() {
        MineralCapacity = MaxMineralCapacity;
    }

    public int GetMinerals(int numberOfMinerals) {
        if (numberOfMinerals < MineralCapacity) {
            MineralCapacity -= numberOfMinerals;
            return numberOfMinerals;
        } else {
            int MineralsLeft = MineralCapacity;
            MineralCapacity = 0;
            gameObject.SetActive(false);
            return MineralsLeft;
        }
    }

}
