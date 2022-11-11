using UnityEngine;

public class AnimationEventReciever : MonoBehaviour
{
    public PlayerCombat combat;
    public PlayerMovement controller;

    public void AttackFinishEvent()
    {   // should be triggered towards the very end of each animation so that it prompts the animator to increment to the next animation if possible.
        combat.AttackFinish_AnimationEvent();
        controller.AttackFinish_AnimationEvent();
    }

    public void AttackHitEvent()
    {
        combat.AttackHit_AnimationEvent();
    }
}
