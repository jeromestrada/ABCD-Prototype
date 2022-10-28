using UnityEngine;

public class AnimationEventReciever : MonoBehaviour
{
    public PlayerCombat combat;
    public PlayerMovement controller;

    public void AttackFinishEvent()
    {
        combat.AttackFinish_AnimationEvent();
        controller.AttackFinish_AnimationEvent();
    }

    public void AttackHitEvent()
    {
        combat.AttackHit_AnimationEvent();
    }
}
