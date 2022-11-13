using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStringState : ComboBaseState
{
    public static event System.Action<int> OnAnimationPlayRequest;
    private bool attackFinished = false;

    public AttackStringState(int _attackIndex) : base(_attackIndex)
    {
        attackIndex = _attackIndex;
    }

    public override void OnEnter(StateMachine _stateMachine)
    {
        AnimationEventReciever.OnAttackFinished -= AttackFinished;
        AnimationEventReciever.OnAttackFinished += AttackFinished;
        base.OnEnter(_stateMachine);
        OnAnimationPlayRequest?.Invoke(attackIndex); // the character animator will listen to this an will fire an animation based on the passed attackIndex
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if(attackFinished)
        {
            if (shouldCombo)
            {
                stateMachine.SetNextState(new AttackStringState((attackIndex + 1) % stateMachine.NumOfStates));
            }
            if (fixedtime >= 1f)
            {
                stateMachine.SetNextStateToMain();
            }
        }
    }

    public void AttackFinished()
    {
        Debug.Log("Attack ended");
        attackFinished = true;
    }
}

