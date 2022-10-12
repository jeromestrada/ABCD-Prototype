using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform playerTrans;

    public LayerMask Ground, Player;

    // Patroling
    public Vector3 walkPoint;
    public float walkPointRange;
    bool walkPointSet;

    public float speedPercent;

    // Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public int enemyDamage;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public Vector3 attackPoint;
    public float attackRadius;

    public event System.Action OnEnemyAttack;

    private void Awake()
    {
        playerTrans = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        attackPoint = transform.position;
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

        speedPercent = agent.velocity.magnitude / agent.speed;

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) Chasing();
        if (playerInSightRange && playerInAttackRange) Attacking();
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) agent.SetDestination(walkPoint);
        Vector3 distancToWalkPoint = transform.position - walkPoint;

        if (distancToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, Ground)) walkPointSet = true;
    }
    private void Chasing()
    {
        agent.SetDestination(playerTrans.position);
    }
    private void Attacking()
    {
        
        agent.SetDestination(transform.position);
        transform.LookAt(playerTrans);
        if (!alreadyAttacked)
        {
            if (OnEnemyAttack != null)
            {
                OnEnemyAttack();
            }
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void EnemyAttackHit_AnimationEvent()
    {   // this even triggers when the animation event reciever fires an attack hit event.
        // access a corresponding attackPoint for the current attack string instead of using a set attackPoint
        Collider[] hitPlayers = Physics.OverlapSphere(attackPoint, attackRadius, Player);
        foreach (Collider player in hitPlayers) // setting up for multiplayer? maybe this makes sense...
        {
            Debug.Log("hitting something");
            player.GetComponent<PlayerStats>().TakeDamage(enemyDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint, attackRadius);
    }
}


