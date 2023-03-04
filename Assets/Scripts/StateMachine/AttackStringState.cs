using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStringState : ComboBaseState
{
    public static System.Action<int> OnStringAttack;

    public AttackStringState(int _attackIndex, Vector3 _attackPoint, float _attackRadius, int _attackDamage, float _gracePeriod, LayerMask _targetMask) 
        : base(_attackIndex, _attackPoint, _attackRadius, _attackDamage, _gracePeriod, _targetMask) {}

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        attackFinished = false;
        shouldCombo = false;
    }

    public override void OnUpdate()
    { // TODO: fix this part so that the attack strings are handled properly, right now the attacks are firing unnecessarily
        base.OnUpdate();
        if (attackFinished)
        {
            Debug.Log("finished, can combo now");
            var index = (attackIndex + 1) % stateMachine.NumOfStates; // this sets up for the next attack animation
            if (shouldCombo)
            {
                Debug.Log("tried to combo");
                OnStringAttack.Invoke(index);
                //stateMachine.SetNextState(new AttackStringState(index, csm.AttackPoint, csm.AttackRadius));
            }
            if (gracePeriod != 0 && fixedtime - attackEndTime >= gracePeriod)
            {
                stateMachine.SetNextStateToMain();
                attackFinished = false;
                shouldCombo = false;
            }
        }
        
    }
}

