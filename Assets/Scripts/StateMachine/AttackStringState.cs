using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStringState : ComboBaseState
{
    public static event System.Action<int> OnAnimationPlayRequest;
    public static bool attackFinished = false;

    public AttackStringState(int _attackIndex) : base(_attackIndex)
    {
        attackIndex = _attackIndex;
    }

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        attackFinished = false;
        shouldCombo = false;
        duration = 0.5f;
        Debug.Log($"Invoking attack with {attackIndex} and sc: {shouldCombo}");
        OnAnimationPlayRequest?.Invoke(attackIndex); // the character animator will listen to this an will fire an animation based on the passed attackIndex
        
    }

    public override void OnUpdate()
    { // TODO: fix this part so that the attack strings are handled properly, right now the attacks are firing unnecessarily
        base.OnUpdate();
        if (attackFinished)
        {
            //Debug.Log($"Attack finished moving to next string! Should combo? {shouldCombo}");
            if (shouldCombo)
            {
                stateMachine.SetNextState(new AttackStringState((attackIndex + 1) % stateMachine.NumOfStates));
            }
        }
        if (fixedtime >= 2f)
        {
            Debug.Log("combo expired will now go to idle");
            stateMachine.SetNextStateToMain();
            attackFinished = false;
            shouldCombo = false;
        }

    }

    public static void AttackFinished()
    {
        attackFinished = true;
        shouldCombo = false;
    }
}

