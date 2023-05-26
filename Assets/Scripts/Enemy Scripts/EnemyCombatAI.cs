using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCombatAI : MonoBehaviour
{
    [SerializeField] private EnemyStats _myStats;

    public NavMeshAgent agent;
    public Transform playerTrans;

    private Transform previousTrans;

    public LayerMask Ground, Player;

    // Patroling
    public Vector3 walkPoint;
    public float walkPointRange = 5f;
    bool walkPointSet;

    public float speedPercent;

    // Attacking
    public float timeBetweenAttacks = 2;
    bool alreadyAttacked;


    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public Vector3 attackPoint;
    public float attackRadius = 1.5f;

    public event System.Action OnEnemyAttack;

    private void OnEnable()
    {
        EnemyStats.OnStatChange += UpdateMoveSpeed;
    }

    private void OnDisable()
    {
        EnemyStats.OnStatChange -= UpdateMoveSpeed;
    }

    private void UpdateMoveSpeed()
    {
        agent.speed = _myStats.Movespeed.GetValue();
    }

    private void Awake()
    {
        if(playerTrans == null) playerTrans = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = _myStats.Movespeed.GetValue();
        previousTrans = transform;
    }

    void Update()
    {
        attackPoint = transform.position;
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

        speedPercent = agent.velocity.magnitude / agent.speed;

        if (speedPercent <= 0) Invoke(nameof(SearchWalkPoint), 3f);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) Chasing();
        if (playerInSightRange && playerInAttackRange) Attacking();
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) agent.SetDestination(walkPoint);
        Vector3 distancToWalkPoint = transform.position - walkPoint;

        if (distancToWalkPoint.magnitude < 2f)
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
            OnEnemyAttack?.Invoke();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void EnemyAttackHit_AnimationEvent()
    {
        Collider[] hitPlayers = Physics.OverlapSphere(attackPoint, attackRadius, Player);
        foreach (Collider player in hitPlayers) // setting up for multiplayer? maybe this makes sense...
        {
            player.GetComponentInChildren<PlayerStats>().TakeDamage(_myStats.Damage.GetValue());
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


