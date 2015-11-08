using UnityEngine;
using System.Collections;

public class V8GameStateManager
{
    V8GameState CurrentGameState;

    public V8NormalState NORMAL_STATE;
    public V8BuildingConnectingState CONNECT_BUILDINGS_STATE;
    public V8BuildBuildingState BUILD_BUILDINGS_STATE;

    public V8GameStateManager()
    {
        CreateStates();
        SetState(NORMAL_STATE);
    }

    void CreateStates()
    {
        NORMAL_STATE = new V8NormalState(this);
        CONNECT_BUILDINGS_STATE = new V8BuildingConnectingState(this);
        BUILD_BUILDINGS_STATE = new V8BuildBuildingState(this);
    }

    public void SetState(V8GameState state)
    {
        if (CurrentGameState != null)
            CurrentGameState.OnExit();
        CurrentGameState = state;
        CurrentGameState.OnEnter();
    }

    public void HandleTouch()
    {
        CurrentGameState.OnHandleTouch();
    }
}

public class V8GameState {
    protected V8GameStateManager StateManager;
    public V8GameState(V8GameStateManager StateManager)
    {
        this.StateManager = StateManager;
    }
    public virtual void OnEnter() { }
    public virtual void OnHandleTouch() { }
    public virtual void OnExit() { }
}