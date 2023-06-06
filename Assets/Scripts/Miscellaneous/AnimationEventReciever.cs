using UnityEngine;

public class AnimationEventReciever : MonoBehaviour
{
    public CombatStateMachine csm;
    public ComboBaseState comboBaseState;
    public PlayerMovement playerMovement;

    public static event System.Action OnAttackFinished;

    private void OnEnable()
    {
        DashAbility.OnDashEnd += AttackFinishEvent;
        DashAbility.OnDash += AttackFinishEvent;

        if (playerMovement == null) playerMovement = GetComponent<PlayerMovement>();
    }
    private void OnDisable()
    {
        DashAbility.OnDashEnd -= AttackFinishEvent;
        DashAbility.OnDash -= AttackFinishEvent;
    }

    private void Awake()
    {
        if(csm == null) csm = GetComponentInChildren<CombatStateMachine>();
    }

    public void AttackFinishEvent()
    {   // should be triggered towards the very end of each animation so that it prompts the animator to increment to the next animation if possible.
        // combat.AttackFinish_AnimationEvent();
        playerMovement.AttackFinish_AnimationEvent();
        AttackStringState.AttackFinished();
    }

    public void AttackHitEvent()
    {
        if(csm.CurrentState != null)
        {
            if(csm.CurrentState.GetType() == typeof(AttackStringState))
            {
                comboBaseState = (ComboBaseState)csm.CurrentState;
                comboBaseState.AttackHit_AnimationEvent();
            }
        }
    }
}
