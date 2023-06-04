using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The AttackStringState contains logic handling each attack
// When a state is passed into the csm as a next state, the state will time itself and will detect if another attack is inputted for a combo
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
    { 
        base.OnUpdate();
        if (attackFinished)
        {
            var index = (attackIndex + 1) % stateMachine.NumOfStates; // this sets up for the next attack animation
            if (shouldCombo) // if this is set in the combo base state update() we invoke a string attack
            {
                Debug.Log("tried to combo");
                OnStringAttack.Invoke(index);
                //stateMachine.SetNextState(new AttackStringState(index, csm.AttackPoint, csm.AttackRadius));
            }
            if (gracePeriod != 0 && fixedtime - attackEndTime >= gracePeriod)
            { // if the grace period for the combo runs out, we set the next state to the main state instead.
                stateMachine.SetNextStateToMain();
                attackFinished = false;
                shouldCombo = false;
            }
        }
        
    }
}

