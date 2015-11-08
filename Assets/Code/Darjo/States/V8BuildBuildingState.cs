using UnityEngine;
using System.Collections;

public class V8BuildBuildingState : V8GameState
{
    public GameObject Parent;
    private float menuSize;
    private Building SelectedBuildingToBuild;
    private Building BuildingProxy;

    public V8BuildBuildingState(V8GameStateManager StateManager) : base(StateManager) { }
    public override void OnEnter() {
        V8GameLogic.Instance.Deselect();
        CreateMenu();
    }

    public void CreateMenu()
    {
        Parent = new GameObject();
        Parent.name = "BuildingsMenu";
        int numBuildings = V8GameLogic.Instance.V8BuildingsList.BuildingsData.Count;
        menuSize = Screen.width * 0.1f;
        for (int i = 0; i < numBuildings; i++)
        {
            GameObject gameobject = new GameObject();
            GameObject building = GameObject.Instantiate(V8GameLogic.Instance.V8BuildingsList.BuildingsData[i].Prefab.gameObject);
            gameobject.transform.parent = Parent.transform;
            building.transform.parent = Parent.transform;
            Camera camera = gameobject.AddComponent<Camera>();
            building.AddComponent<RotatingAroundAxis>();
            building.transform.position = new Vector3(1000 + (i * 20), 0, 0);
            camera.transform.position = new Vector3(1000 + (i*20), 8.64f, -11.57f);
            camera.transform.localEulerAngles = new Vector3(22.92895f, 0, 0);
            camera.depth = 100 + i;
            //camera.clearFlags = CameraClearFlags.Depth;
            camera.pixelRect = new Rect(Screen.width - menuSize - 10f, (10 + menuSize) * i, menuSize, menuSize);
        }
    }

    public Building CheckClickOnMenu(Vector2 mousePosition)
    {
        for (int i = 0; i < V8GameLogic.Instance.V8BuildingsList.BuildingsData.Count; i++)
        {
            Rect r = new Rect(Screen.width - menuSize - 10f, (10 + menuSize) * i, menuSize, menuSize);
            if (r.Contains(mousePosition))
                return V8GameLogic.Instance.V8BuildingsList.BuildingsData[i].Prefab;
        }
        return null;
    }

    public override void OnHandleTouch()
    {
        RaycastHit hit = new RaycastHit();
        bool didHitGround = false;
        Ray ray = V8GameLogic.Instance.Camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity,1 << 8))
        {
            if (BuildingProxy != null)
            {
                didHitGround = true;
                BuildingProxy.transform.position = hit.point;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Building b = CheckClickOnMenu(Input.mousePosition);
            if (b != null && b != SelectedBuildingToBuild)
            {
                if (BuildingProxy != null)
                    GameObject.Destroy(BuildingProxy.gameObject);
                SelectedBuildingToBuild = b;
                InstantiateSelectedBuilding(hit.point);
            }
            else if (b == null && didHitGround)
            {
                BuildingProxy.Init();
                InstantiateSelectedBuilding(hit.point);
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            StateManager.SetState(StateManager.NORMAL_STATE);
        }
    }

    public void InstantiateSelectedBuilding(Vector3 position)
    {
        BuildingProxy = GameObject.Instantiate(SelectedBuildingToBuild) as Building;
        BuildingProxy.transform.position = position;
    }

    public override void OnExit() {
        GameObject.Destroy(Parent);
        if (BuildingProxy != null)
            GameObject.Destroy(BuildingProxy.gameObject);
        SelectedBuildingToBuild = null;
    }
}

public class RotatingAroundAxis : MonoBehaviour
{
    public void Update()
    {
        transform.Rotate(Vector3.up, 45f*Time.deltaTime);
    }
}
