using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    WeaponManager weaponManager;
    [SerializeField] Weapon equippedWeapon;
    public bool canStringAttack;
    private int currentAttackString;
    private Transform currentAttackPoint;
    
    private float lastAttackStringTime;
    public float stringGracePeriod = 0.5f;

    public int playerDamage = 50;

    public float attackRadius = 3f;
    public LayerMask enemyMask;


    bool isCoolingDown = false;
    float finalStringTime;
    public float cooldownDuration = 3f;

    public event System.Action<int> OnAttack;
    PlayerController controller;

    private void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
        weaponManager.onWeaponChanged += OnWeaponChanged;

        controller = GetComponent<PlayerController>();
        lastAttackStringTime = 0;
        canStringAttack = true;
        ResetAttackString();
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
            ResetAttackString();
        }
        if (Input.GetButton("Fire1") && canStringAttack && !isCoolingDown)
        {
            canStringAttack = false; // we wait for the animation to hit before we can attack again
            lastAttackStringTime = float.MaxValue; 
            if (OnAttack != null)
            {
                currentAttackPoint = equippedWeapon.attackPoints[currentAttackString];
                OnAttack(currentAttackString % equippedWeapon.stringAttacksCount); // invoke the delegate
            }
            
            if (controller.isDashing)
            {
                controller.OnEndDash();
            }
        }
    }

    private void OnWeaponChanged(Weapon weapon)
    {
        equippedWeapon = weapon;
        ResetAttackString();
    }

    private void ResetAttackString()
    {
        currentAttackString = 0;
        if(equippedWeapon != null) currentAttackPoint = equippedWeapon.attackPoints[currentAttackString];
    }

    public void AttackFinish_AnimationEvent()
    {
        lastAttackStringTime = Time.time;
        canStringAttack = true;
        currentAttackString++; // increment to the next attack string
        if (currentAttackString == equippedWeapon.stringAttacksCount) // if we've reached the last string we cooldown
        {
            isCoolingDown = true;
            finalStringTime = Time.time;
            ResetAttackString();
        }
        
    }

    public void AttackHit_AnimationEvent()
    {
        // access a corresponding attackPoint for the current attack string instead of using a set attackPoint
        Collider[] hitEnemies = Physics.OverlapSphere(currentAttackPoint.position, attackRadius, enemyMask);

        foreach(Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(playerDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(currentAttackPoint == null)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(currentAttackPoint.position, attackRadius);
    }
}
