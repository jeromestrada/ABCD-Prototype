using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    Animator animator;
    EnemyStats enemy;
    EnemyCombatAI enemyAI;

    public float locomotionSmoothTime = 0.1f;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        enemy = GetComponent<EnemyStats>();
        enemyAI = GetComponent<EnemyCombatAI>();

        enemy.OnHealthChanged += OnTakingDamage;
        enemy.OnDying += OnDying;

        enemyAI.OnEnemyAttack += OnEnemyAttack;
    }
    void Update()
    {
        animator.SetFloat("speedPercent", enemyAI.speedPercent, locomotionSmoothTime, Time.deltaTime);
    }

    public void OnTakingDamage(int currentHealth, int maxHealth)
    {
        animator.SetTrigger("hurtTrigger");
    }

    public void OnDying()
    {
        animator.SetBool("isDead", true);
    }

    public void OnEnemyAttack()
    {
        animator.SetTrigger("attackTrigger");
    }
}
