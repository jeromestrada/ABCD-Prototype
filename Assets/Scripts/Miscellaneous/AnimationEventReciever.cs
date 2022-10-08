using UnityEngine;

public class AnimationEventReciever : MonoBehaviour
{
    public CharacterCombat combat;
    public PlayerController controller;

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
