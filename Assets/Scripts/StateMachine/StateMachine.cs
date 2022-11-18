using System;
using UnityEngine;

[System.Serializable]
public class StateMachine : MonoBehaviour
{
    public string customName;

    protected State mainStateType;
    public State CurrentState { get; private set; }

    protected State nextState;

    protected int numOfStates;

    public int NumOfStates => numOfStates;


    public virtual void Update()
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

    public void LateUpdate()
    {
        if(CurrentState != null)
        {
            CurrentState.OnLateUpdate();
        }
    }
    public void FixedUpdate()
    {
        if (CurrentState != null)
            CurrentState.OnFixedUpdate();
    }

    public void SetNextStateToMain()
    {
        // Debug.Log("Set to idle");
        nextState = mainStateType;
    }

    public void Awake()
    {
        SetState(mainStateType);
        SetNextStateToMain();
    }

    public virtual void OnValidate()
    {
        if (mainStateType == null)
        {
            mainStateType = new IdleCombatState();
        }
    }
}
