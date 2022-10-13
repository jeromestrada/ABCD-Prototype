using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEventReceiver : MonoBehaviour
{
    public EnemyCombatAI enemyAI;

    public void EnemyAttackHitEvent()
    {
        enemyAI.EnemyAttackHit_AnimationEvent();
    }
}
