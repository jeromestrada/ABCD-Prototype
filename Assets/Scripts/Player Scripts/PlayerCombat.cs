using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    EquipmentManager weaponManager;
    [SerializeField] Equipment equippedWeapon;
    [SerializeField] PlayerStats playerStats;
    public bool canStringAttack;
    public int CurrentAttackString;
    private Transform currentStringAttackPoint;
    private Vector3 attackPoint;
    
    private float lastAttackStringTime;
    public float stringGracePeriod = 0.5f;
    // TODO: replace the grace period system with a transition phase in between attacks

    public float attackRadius = 3f;
    public LayerMask enemyMask;


    bool isCoolingDown = false;
    float finalStringTime;
    public float cooldownDuration = 3f;

    public static event System.Action<int> OnAttack;
    PlayerMovement controller;
    [SerializeField] MouseItemData mouseItemData;

    void OnEnable()
    {
        EquipmentManager.OnEquipmentChanged += OnWeaponChanged;
        DashAbility.OnDashEnd += OnDashEnd;
    }

    void OnDisable()
    {
        EquipmentManager.OnEquipmentChanged -= OnWeaponChanged;
        DashAbility.OnDashEnd -= OnDashEnd;
    }

    private void Start()
    {
        controller = GetComponent<PlayerMovement>();
        lastAttackStringTime = 0;
        canStringAttack = true;
        ResetAttackString();
    }

    private void OnDashEnd() // this can be refactored to allow dash attacks.
    {
        canStringAttack = true;
        ResetAttackString(); // reset the attack string to the beginning after a dash
    }

    // Update is called once per frame
    void Update()
    {
        if(equippedWeapon != null)
        {
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
                
                currentStringAttackPoint = equippedWeapon.AttackPoints[CurrentAttackString];
                OnAttack?.Invoke(CurrentAttackString % equippedWeapon.StringAttacksCount); // invoke the action
            }
        }
    }

    // TODO: figure out if theres a better system to set this, right now it just uses the raw values of the attack point and directly sets the offset
    public Vector3 PlaceAttackPoint()
    {   // the position of the current string's attack point is placed relative to the player's transform
        return transform.position +
            (transform.right * currentStringAttackPoint.position.x) +
            (transform.up * currentStringAttackPoint.position.y) +
            (transform.forward * currentStringAttackPoint.position.z);
    }

    private void OnWeaponChanged(Item oldWeapon, Item newWeapon)
    {
        if(newWeapon != null)
        {
            if (newWeapon.ItemType == EquipmentType.Weapon)
            {
                equippedWeapon = (Equipment)newWeapon;
                ResetAttackString();
            }
        }
    }

    public void ResetAttackString()
    {
        CurrentAttackString = 0;
        if(equippedWeapon != null) currentStringAttackPoint = equippedWeapon.AttackPoints[CurrentAttackString];
    }

    public void AttackFinish_AnimationEvent() // TODO: when this event fires, we play a transition phase where the next attack can be triggered
    {
        lastAttackStringTime = Time.time;
        canStringAttack = true;
        CurrentAttackString++; // increment to the next attack string
        if (CurrentAttackString == equippedWeapon.StringAttacksCount) // if we've reached the last string we cooldown
        {
            isCoolingDown = true;
            finalStringTime = Time.time;
            ResetAttackString();
        }
    }

    public void AttackHit_AnimationEvent()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint, attackRadius, enemyMask);
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyStats>().TakeDamage(playerStats.Damage.GetValue());
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
