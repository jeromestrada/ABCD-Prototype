using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    State CurrentState;

    public void Update()
    {
        if(CurrentState != null)
        {
            CurrentState.OnUpdate();
        }
    }

    public void SetNextState(State _newState)
    {
        CurrentState.OnExit();
        CurrentState = _newState;
        CurrentState.OnEnter();
    }
}
