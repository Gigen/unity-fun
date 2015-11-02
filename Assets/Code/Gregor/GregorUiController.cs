using UnityEngine;
using UnityEngine.UI;

public class GregorUiController : MonoBehaviour {
	
    [SerializeField] private Text MineralsText = null;

    void Start() {
        OnMineralsValueChange(MainGregorLogic.Instance.PlayerController.Minerals);
        MainGregorLogic.Instance.PlayerController.OnMineralsValueChange += OnMineralsValueChange;
    }

    public void OnMineralsValueChange(int newValue) {
        MineralsText.text = newValue.ToString();
    }

}
