using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStringState : ComboBaseState
{
    public static event System.Action<int> OnAnimationPlayRequest;


    public AttackStringState(int _attackIndex) : base(_attackIndex)
    {
        attackIndex = _attackIndex;
    }

    public override void OnEnter(StateMachine _stateMachine)
    {
        AnimationEventReciever.OnAttackFinished -= UpdateState;
        AnimationEventReciever.OnAttackFinished += UpdateState;
        base.OnEnter(_stateMachine);
        OnAnimationPlayRequest?.Invoke(attackIndex); // the character animator will listen to this an will fire an animation based on the passed attackIndex
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public void UpdateState()
    {
        if (shouldCombo)
        {
            stateMachine.SetNextState(new AttackStringState((attackIndex + 1) % stateMachine.NumOfStates));
        }
        else
        {
            stateMachine.SetNextStateToMain();
        }
    }
}

