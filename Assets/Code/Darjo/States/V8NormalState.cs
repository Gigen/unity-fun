using UnityEngine;
using System.Collections;

public class V8NormalState : V8GameState
{
    public V8NormalState(V8GameStateManager StateManager) : base(StateManager) { }

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
                    V8GameLogic.Instance.SelectBuilding(hitBuilding);
                else
                    V8GameLogic.Instance.Deselect();
            }
        }
    }
    public override void OnExit() { }
}