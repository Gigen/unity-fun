using UnityEngine;
using System.Collections;


public class V8GameLogic : MonoBehaviour {
    public static V8GameLogic Instance;
    public Camera Camera;

    public V8Buildings V8BuildingsList;
    public Building SelectedBuilding;

    public Transform SelectBox;

    private V8GameStateManager V8GameStateManager;
    

    void Awake()
    {
        V8GameLogic.Instance = this;
        V8GameStateManager = new V8GameStateManager();
    }    

    void Start () {
    }
	
	void Update () {
        V8GameStateManager.HandleTouch();

        if (Input.GetKeyUp(KeyCode.C) && SelectedBuilding != null && SelectedBuilding.GetStorageSpace() != null)
            V8GameStateManager.SetState(V8GameStateManager.CONNECT_BUILDINGS_STATE);
        if (Input.GetKeyUp(KeyCode.B))
            V8GameStateManager.SetState(V8GameStateManager.BUILD_BUILDINGS_STATE);

    }
    public void SelectBuilding(Building b)
    {
        if (b != SelectedBuilding)
        {
            SelectedBuilding = b;
            SelectedBuilding.Select();
        }
    }
    public void Deselect()
    {
        if (SelectedBuilding != null)
        {
            SelectedBuilding.Deselect();
            SelectedBuilding = null;
        }
    }
}
