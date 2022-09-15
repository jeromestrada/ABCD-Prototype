using UnityEngine;

public class AnimationEventReciever : MonoBehaviour
{
    public CharacterCombat combat;
    public PlayerController controller;

    public void AttackHitEvent()
    {
        combat.AttackHit_AnimationEvent();
        controller.AttackHit_AnimationEvent();
    }
}
