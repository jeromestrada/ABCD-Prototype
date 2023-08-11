using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    Animator animator;
    EnemyStats enemy;
    EnemyCombatAI enemyAI;

    public AnimationClip replaceableAttackAnim;
    public AnimationClip[] defaultAttackAnimSet;
    public AnimatorOverrideController enemyOverrideController;
    public int animationSetLength;

    public float locomotionSmoothTime = 0.1f;
    public void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        enemy = GetComponent<EnemyStats>();
        enemyAI = GetComponent<EnemyCombatAI>();

        if (enemyOverrideController == null)
        {
            enemyOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        }
        animator.runtimeAnimatorController = enemyOverrideController;

        if (defaultAttackAnimSet != null) animationSetLength = defaultAttackAnimSet.Length;
        Debug.Log($"animation length of {gameObject.name} is {animationSetLength}");
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
        Invoke(nameof(MoveAgain), 1f);
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
        int randNum = Random.Range(0, animationSetLength);
        Debug.Log($"{gameObject.name} rolled {randNum} for its attack!");
        enemyOverrideController[replaceableAttackAnim.name] = defaultAttackAnimSet[randNum];
        animator.SetTrigger("attackTrigger");
    }
}
