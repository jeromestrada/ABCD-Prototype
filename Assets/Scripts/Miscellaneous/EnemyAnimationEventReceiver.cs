using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEventReceiver : MonoBehaviour
{
    public EnemyCombatAI enemyAI;

    private void Awake()
    {
        if(enemyAI == null) enemyAI = GetComponentInParent<EnemyCombatAI>();
    }
    public void EnemyAttackHitEvent()
    {
        enemyAI.EnemyAttackHit_AnimationEvent();
    }
}
