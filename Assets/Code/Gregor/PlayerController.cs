using UnityEngine;

public class PlayerController : MonoBehaviour {

    public delegate void OnMineralsValueChangeDelegate(int newValue);

    public OnMineralsValueChangeDelegate OnMineralsValueChange;

    private int minerals = 0;
    private int Lives = 3;

    public int Minerals {
        get { 
            return minerals; 
        }
        set { 
            minerals = value;
            if (OnMineralsValueChange != null) {
                OnMineralsValueChange(value);
            }
        }
    }

    void Start() {
        MainGregorLogic.Instance.PlayerController = this;
    }

}
