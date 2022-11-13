using System;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public string customName;

    private State mainStateType;
    public State CurrentState { get; private set; }

    private State nextState;

    private int numOfStates;

    public int NumOfStates => numOfStates;

    public void Update()
    {
        if (nextState != null)
        {
            SetState(nextState);
        }
        if(CurrentState != null)
        {
            CurrentState.OnUpdate();
        }
    }

    public void SetNumOfStates(int _numOfStates)
    {
        numOfStates = _numOfStates;
    }

    public void SetState(State _newState)
    {
        nextState = null;
        if (CurrentState != null)
        {
            CurrentState.OnExit();
        }
        CurrentState = _newState;
        CurrentState.OnEnter(this);
    }

    public void SetNextState(State _newState)
    {
        if(_newState != null)
        {
            nextState = _newState;
        }
    }

    private void LateUpdate()
    {
        if(CurrentState != null)
        {
            CurrentState.OnLateUpdate();
        }
    }
    private void FixedUpdate()
    {
        if (CurrentState != null)
            CurrentState.OnFixedUpdate();
    }

    public void SetNextStateToMain()
    {
        nextState = mainStateType;
    }

    private void Awake()
    {
        SetState(mainStateType);
        SetNextStateToMain();
    }

    private void OnValidate()
    {
        if (mainStateType == null)
        {
            if (customName == "Combat")
            {
                mainStateType = new IdleCombatState();
            }
        }
    }
}
