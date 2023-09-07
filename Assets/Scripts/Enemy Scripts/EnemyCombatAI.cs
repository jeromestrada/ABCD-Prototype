using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCombatAI : MonoBehaviour
{
    [SerializeField] private EnemyStats _myStats;
    private Vector3 trueAttackPoint;
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
    public bool canMove;
    private bool searching = false;
    private bool isStrafing = false;
    private int strafeDir = 0;
    private Vector3 dir;


    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public Vector3 attackPoint;
    public float attackRadius = 1.5f;

    private float stoppingDistTemp;
    private float speedTemp;

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
        canMove = true;
        stoppingDistTemp = agent.stoppingDistance;
        speedTemp = agent.speed;
        SearchPoint();
    }

    void Update()
    {
        trueAttackPoint = transform.position + attackPoint;
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

        speedPercent = agent.velocity.magnitude / agent.speed;

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange && canMove) Chasing();
        if (playerInSightRange && playerInAttackRange) Attacking();
    }

    private void Patrolling()
    {
        if (walkPointSet) agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;

        if (!searching && speedPercent <= 0f) SearchWalkPoint();
    }

    private void SearchWalkPoint()
    {
        CancelInvoke(nameof(SearchPoint));
        searching = true;
        Invoke(nameof(SearchPoint), Random.Range(1.5f, 3f));
    }

    private void SearchPoint()
    {
        
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        searching = false;
        if (Physics.Raycast(walkPoint, -transform.up, 2f, Ground)) walkPointSet = true;
    }

    private void Chasing()
    {
        if(canMove) transform.LookAt(playerTrans);
        float chaseDistance = Vector3.Distance(agent.transform.position, playerTrans.position);
        //Debug.Log($"cd:{chaseDistance}, sd:{agent.stoppingDistance}");
        if (chaseDistance > stoppingDistTemp)
        {
            Debug.Log("CHASING");
            isStrafing = false;
            // revert alterations
            agent.stoppingDistance = stoppingDistTemp;
            agent.speed = speedTemp; 
            agent.SetDestination(playerTrans.position);
        }
        else
        {
            if (!isStrafing)
            {
                isStrafing = true;
                Invoke(nameof(Strafe), 2f); // strafe after death staring the player
            }
            else
            {
                agent.SetDestination(agent.transform.position + (dir * 4));
            }
        }
    }

    private void Strafe()
    {
        //Debug.Log("STRAFING````````````````````````````````````");
        isStrafing = true;
        agent.isStopped = false;
        agent.stoppingDistance = 1; // temporarily alter the stopping distance to allow the navmesh to strafe
        agent.speed /= 4; // slow down the agent for strafing.
        Invoke(nameof(StopStrafe), 2f);
    }

    private void StopStrafe()
    {
        agent.isStopped = true;
        dir = Random.Range(0, 2) == 0 ? agent.transform.right : -agent.transform.right;
        Invoke(nameof(Strafe), 1.2f);
    }

    private void Attacking()
    {
        
        agent.SetDestination(transform.position);
        if(canMove) transform.LookAt(playerTrans);
        if (!alreadyAttacked)
        {
            OnEnemyAttack?.Invoke();
            canMove = false;
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
        canMove = true;
        Collider[] hitPlayers = Physics.OverlapSphere(trueAttackPoint, attackRadius, Player);
        foreach (Collider player in hitPlayers) // setting up for multiplayer? maybe this makes sense...
        {
            Debug.Log(gameObject.name + " is hitting " + player.gameObject.name);
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
        Gizmos.DrawWireSphere(trueAttackPoint, attackRadius);
    }
}


