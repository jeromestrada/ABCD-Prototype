using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    Animator animator;

    Enemy enemy;

    EnemyAI enemyAI;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        enemy = GetComponent<Enemy>();
        enemyAI = GetComponent<EnemyAI>();

        enemy.OnTakingDamage += OnTakingDamage;
        enemy.OnDying += OnDying;

        enemyAI.OnEnemyAttack += OnEnemyAttack;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTakingDamage()
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
