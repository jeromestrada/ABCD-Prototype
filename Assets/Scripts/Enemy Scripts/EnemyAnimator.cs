using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    Animator animator;
    EnemyStats enemy;
    EnemyCombatAI enemyAI;

    public float locomotionSmoothTime = 0.1f;
    public void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        enemy = GetComponent<EnemyStats>();
        enemyAI = GetComponent<EnemyCombatAI>();
    }

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
    
    void Update()
    {
        animator.SetFloat("speedPercent", enemyAI.speedPercent, locomotionSmoothTime, Time.deltaTime);
    }

    public void OnTakingDamage(int currentHealth, int maxHealth)
    {
        animator.SetTrigger("hurtTrigger");
        Invoke(nameof(MoveAgain), 2f);
    }
    public void MoveAgain()
    {
        enemyAI.canMove = true;
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
