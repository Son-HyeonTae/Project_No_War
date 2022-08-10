using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyStateMachine : MonoBehaviour
{
    [HideInInspector] public Statement CurrentState;
    [HideInInspector] public Statement PreviousState;
    private Entity EntityComponent;

    private void Awake()
    {
        EntityComponent = GetComponent<Entity>();
    }

    public bool Initialize(Statement state)
    {
        CurrentState = state;
        PreviousState = CurrentState;
        state.OnEnter();
        return CurrentState != null;
    }

    public bool Change(Statement state)
    {
        if(CurrentState != null && CurrentState.bCanChange && PreviousState != null && PreviousState.bCanChange)
        {
            if (CurrentState == state)
                return false;
            PreviousState = CurrentState != null ? CurrentState : null;
            CurrentState = state;

            if (PreviousState != null) PreviousState.OnExit();
            if (CurrentState != null) CurrentState.OnEnter();
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.Execute(EntityComponent);
        }
    }
}
