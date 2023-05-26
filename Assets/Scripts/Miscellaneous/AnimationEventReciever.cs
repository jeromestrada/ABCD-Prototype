using UnityEngine;

public class AnimationEventReciever : MonoBehaviour
{
    public CombatStateMachine csm;
    public ComboBaseState comboBaseState;
    public PlayerMovement controller;

    public static event System.Action OnAttackFinished;

    private void OnEnable()
    {
        DashAbility.OnDashEnd += AttackFinishEvent;
        DashAbility.OnDash += AttackFinishEvent;
    }
    private void OnDisable()
    {
        DashAbility.OnDashEnd -= AttackFinishEvent;
        DashAbility.OnDash -= AttackFinishEvent;
    }

    public void AttackFinishEvent()
    {   // should be triggered towards the very end of each animation so that it prompts the animator to increment to the next animation if possible.
        // combat.AttackFinish_AnimationEvent();
        controller.AttackFinish_AnimationEvent();
        AttackStringState.AttackFinished();
    }

    public void AttackHitEvent()
    {
        if(csm.CurrentState != null)
        {
            comboBaseState = (ComboBaseState)csm.CurrentState;
            comboBaseState.AttackHit_AnimationEvent();
        }
    }
}
