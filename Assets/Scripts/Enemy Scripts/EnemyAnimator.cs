using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    Animator animator;
    EnemyStats enemy;
    EnemyCombatAI enemyAI;

    public float locomotionSmoothTime = 0.1f;

    private void OnEnable()
    {
        enemy.OnHealthChanged += OnTakingDamage;
        enemy.OnDying += OnDying;
        enemyAI.OnEnemyAttack += OnEnemyAttack;
    }
    private void OnDisable()
    {
        enemy.OnHealthChanged -= OnTakingDamage;
        enemy.OnDying -= OnDying;
        enemyAI.OnEnemyAttack -= OnEnemyAttack;
    }
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        enemy = GetComponent<EnemyStats>();
        enemyAI = GetComponent<EnemyCombatAI>();
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
