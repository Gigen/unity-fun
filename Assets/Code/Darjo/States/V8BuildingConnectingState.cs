using UnityEngine;
using System.Collections;

public class V8BuildingConnectingState : V8GameState
{
    public V8BuildingConnectingState(V8GameStateManager StateManager) : base(StateManager) { }
    public override void OnEnter() { }
    public override void OnHandleTouch()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = V8GameLogic.Instance.Camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Building hitBuilding = hit.collider.gameObject.GetComponent<Building>();
                if (hitBuilding != null)
                {
                    StorageSpace.Connect(V8GameLogic.Instance.SelectedBuilding, hitBuilding);
                    StateManager.SetState(StateManager.NORMAL_STATE);
                }
                else
                {
                    V8GameLogic.Instance.Deselect();
                    StateManager.SetState(StateManager.NORMAL_STATE);
                }
            }
        }
    }
    public override void OnExit() { }
}
