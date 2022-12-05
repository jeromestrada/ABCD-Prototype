using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStringState : ComboBaseState
{
    public static System.Action<int> OnStringAttack;

    public AttackStringState(int _attackIndex, Vector3 _attackPoint, float _attackRadius) 
        : base(_attackIndex, _attackPoint, _attackRadius)
    {
        
    }

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        attackFinished = false;
        shouldCombo = false;
        csm.comboExpired = false;
    }

    public override void OnUpdate()
    { // TODO: fix this part so that the attack strings are handled properly, right now the attacks are firing unnecessarily
        base.OnUpdate();
        if (attackFinished)
        {
            var index = (attackIndex + 1) % stateMachine.NumOfStates; // this sets up for the next attack animation
            if (shouldCombo)
            {
                OnStringAttack.Invoke(index);
                //stateMachine.SetNextState(new AttackStringState(index, csm.AttackPoint, csm.AttackRadius));
            }
            if (fixedtime - attackEndTime >= csm.CurrentGracePeriodExtension)
            {
                csm.comboExpired = true;
                stateMachine.SetNextStateToMain();
                attackFinished = false;
                shouldCombo = false;
            }
        }
    }

    
}

