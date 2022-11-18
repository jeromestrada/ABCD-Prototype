using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStringState : ComboBaseState
{
    public AttackStringState(int _attackIndex, Vector3 _attackPoint, float _attackRadius, float _extension) : base(_attackIndex, _attackPoint, _attackRadius, _extension)
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
            if (shouldCombo)
            {
                var index = (attackIndex + 1) % stateMachine.NumOfStates;
                stateMachine.SetNextState(new AttackStringState(index, csm.AttackPoint, attackRadius, csm.GracePeriodExtensions[index]));
            }
            if (fixedtime - attackEndTime >= GracePeriod())
            {
                csm.comboExpired = true;
                stateMachine.SetNextStateToMain();
                attackFinished = false;
                shouldCombo = false;
            }
        }
    }

    public float GracePeriod()
    {
        csm.gracePeriodExtension = csm.gracePeriod + gracePeriodExtension;
        return csm.gracePeriodExtension;
    }
}

