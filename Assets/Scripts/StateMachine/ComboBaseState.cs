using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboBaseState : State
{

    public float duration; // might not be needed because there will be an animation event listener attached to the animator.
    protected Animator animator; // can probably refactored so that the animator listens for a comboBaseState invoking an OnRequestAnimationPlay or something.
    protected bool shouldCombo = false; // replaces the canStringAttack in the player combat?
    protected int attackIndex; // will contain which attack string the combo is currently on?


    public ComboBaseState(int _attackIndex)
    {
        attackIndex = _attackIndex;
    }

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if(Input.GetMouseButtonDown(0)) shouldCombo = true;
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
