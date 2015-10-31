using UnityEngine;

public class MainGregorLogic : MonoBehaviour {

    public static MainGregorLogic Instance;

    [HideInInspector] public PlayerController PlayerController;

    void Awake() {
        MainGregorLogic.Instance = this;
    }

}
