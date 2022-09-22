using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    public bool canStringAttack;
    private int currentAttackString; 
    public int stringAttacksCount = 2;
    private float lastAttackStringTime;
    public float stringGracePeriod = 0.5f;

    public int playerDamage = 50;

    public Transform attackPoint;
    public float attackRadius = 3f;
    public LayerMask enemyMask;


    bool isCoolingDown = false;
    float finalStringTime;
    public float cooldownDuration = 3f;

    public event System.Action<int> OnAttack;
    PlayerController controller;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
        lastAttackStringTime = 0;
        canStringAttack = true;
        currentAttackString = 0;
    }
    // Update is called once per frame
    void Update()
    {
        // TODO: will use full animation instead, and have a separate recovery animation for each attack
        // this will eliminate the need to use events to sync different attack string animations.
        
        if ((Time.time - finalStringTime) >= cooldownDuration)
        {
            isCoolingDown = false;
        }
        if ((Time.time - lastAttackStringTime) >= stringGracePeriod)
        {
            currentAttackString = 0; // reset the string to the first animation if the grace period lapsed.
        }
        if (Input.GetButton("Fire1") && canStringAttack && !isCoolingDown)
        {
            canStringAttack = false; // we wait for the animation to hit before we can attack again
            lastAttackStringTime = float.MaxValue; 
            if (OnAttack != null)
            {
                OnAttack(currentAttackString % stringAttacksCount); // invoke the delegate
            }
            
            if (controller.isDashing)
            {
                controller.OnEndDash();
            }
        }
    }

    public void AttackFinish_AnimationEvent()
    {
        lastAttackStringTime = Time.time;
        canStringAttack = true;
        currentAttackString++; // increment to the next attack string
        if (currentAttackString == stringAttacksCount) // if we've reached the last string we cooldown
        {
            isCoolingDown = true;
            finalStringTime = Time.time;
            currentAttackString = 0;
        }
        
    }

    public void AttackHit_AnimationEvent()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRadius, enemyMask);

        foreach(Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(playerDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
