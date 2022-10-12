using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    WeaponManager weaponManager;
    [SerializeField] Weapon equippedWeapon;
    public bool canStringAttack;
    public int CurrentAttackString;
    private Transform currentStringAttackPoint;
    private Vector3 attackPoint;
    
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

    [SerializeField] MouseItemData mouseItemData;

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
        attackPoint = PlaceAttackPoint();
        if (Input.GetButton("Fire1") && !MouseItemData.IsPointerOverUIObjects() && !mouseItemData.inUI && canStringAttack && !isCoolingDown)
        {
            canStringAttack = false; // we wait for the animation to hit before we can attack again
            lastAttackStringTime = float.MaxValue; 
            if (OnAttack != null)
            {
                currentStringAttackPoint = equippedWeapon.attackPoints[CurrentAttackString];
                OnAttack(CurrentAttackString % equippedWeapon.stringAttacksCount); // invoke the delegate
            }
            if (controller.isDashing)
            {
                controller.OnEndDash();
            }
        }
    }

    // TODO: figure out if theres a better system to set this, right now it just uses the raw values of the attack point and directly sets the offset
    public Vector3 PlaceAttackPoint()
    {   // the position of the current string's attack point is added to the player's transform
        return transform.position +
            (transform.right * currentStringAttackPoint.position.x) +
            (transform.up * currentStringAttackPoint.position.y) +
            (transform.forward * currentStringAttackPoint.position.z);
    }

    private void OnWeaponChanged(Weapon weapon)
    {
        equippedWeapon = weapon;
        ResetAttackString();
    }

    public void ResetAttackString()
    {
        CurrentAttackString = 0;
        if(equippedWeapon != null) currentStringAttackPoint = equippedWeapon.attackPoints[CurrentAttackString];
    }

    public void AttackFinish_AnimationEvent()
    {
        lastAttackStringTime = Time.time;
        canStringAttack = true;
        CurrentAttackString++; // increment to the next attack string
        if (CurrentAttackString == equippedWeapon.stringAttacksCount) // if we've reached the last string we cooldown
        {
            isCoolingDown = true;
            finalStringTime = Time.time;
            ResetAttackString();
        }
        
    }

    public void AttackHit_AnimationEvent()
    {   // this even triggers when the animation event reciever fires an attack hit event.
        // access a corresponding attackPoint for the current attack string instead of using a set attackPoint
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint, attackRadius, enemyMask);
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(playerDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(currentStringAttackPoint == null)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint, attackRadius);
    }
}
