using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    Animator animator;
    Enemy enemy;
    EnemyCombatAI enemyAI;

    public float locomotionSmoothTime = 0.1f;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        enemy = GetComponent<Enemy>();
        enemyAI = GetComponent<EnemyCombatAI>();

        Enemy.OnHealthChanged += OnTakingDamage;
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
