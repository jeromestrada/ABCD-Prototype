using UnityEngine;

public class AnimationEventReciever : MonoBehaviour
{
    public PlayerCombat combat;
    public PlayerMovement controller;

    public static event System.Action OnAttackFinished;

    public void AttackFinishEvent()
    {   // should be triggered towards the very end of each animation so that it prompts the animator to increment to the next animation if possible.
        combat.AttackFinish_AnimationEvent();
        controller.AttackFinish_AnimationEvent();
        AttackStringState.AttackFinished();
    }

    public void AttackHitEvent()
    {
        combat.AttackHit_AnimationEvent();
    }
}
