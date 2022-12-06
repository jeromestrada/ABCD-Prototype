using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStateMachine : StateMachine
{
    [SerializeField] protected PlayerStats playerStats;
    protected Transform currentStringAttackPoint;
    protected float attackRadius = 3f;

    public LayerMask enemyMask;

    [SerializeField] protected Equipment equippedWeapon;
    protected Vector3 attackPoint;
    public bool comboExpired;
    
    [SerializeField] ComboCharacter combo;
    public float globalGracePeriod = 1f;
    public float CurrentGracePeriodExtension;


    public PlayerStats PlayerStats => playerStats;
    public Equipment EquippedWeapon => equippedWeapon;
    public Transform CurrentStringAttackPoint => currentStringAttackPoint;
    public float AttackRadius => attackRadius;
    public Vector3 AttackPoint => attackPoint;
    public float[] WeaponGracePeriodExtensions => equippedWeapon.GracePeriodExtensions;

    void OnEnable()
    {
        EquipmentManager.OnEquipmentChanged += OnWeaponChanged;
        ComboCharacter.OnFirstAttack += OnAttack;
        AttackStringState.OnStringAttack += OnAttack;
        ComboBaseState.OnEnterAttackString += TotalGracePeriod;
    }


    void OnDisable()
    {
        EquipmentManager.OnEquipmentChanged -= OnWeaponChanged;
        ComboCharacter.OnFirstAttack -= OnAttack;
        AttackStringState.OnStringAttack -= OnAttack;
        ComboBaseState.OnEnterAttackString -= TotalGracePeriod;
    }
    private void OnAttack(int attackIndex)
    {
        comboExpired = false;
        UpdateAttackPoint(equippedWeapon.AttackPoints[attackIndex]);
        SetNextState(new AttackStringState(attackIndex, attackPoint, attackRadius, playerStats.Damage.GetValue(), CurrentGracePeriodExtension, enemyMask));
    }

    public override void Update()
    {
        base.Update();
        if(equippedWeapon != null)
            attackPoint = PlaceAttackPoint();
    }

    private void OnWeaponChanged(Item oldWeapon, Item newWeapon)
    {
        if (newWeapon != null)
        {
            if (newWeapon.ItemType == EquipmentType.Weapon)
            {
                equippedWeapon = (Equipment)newWeapon;
                combo.SetMaxCombo(equippedWeapon);
                currentStringAttackPoint = equippedWeapon.AttackPoints[0];
            }
        }
    }

    public void TotalGracePeriod(int _index)
    {
        CurrentGracePeriodExtension = globalGracePeriod + WeaponGracePeriodExtensions[_index];
    }

    public void UpdateAttackPoint(Transform newAttackPoint)
    {
        currentStringAttackPoint = newAttackPoint;
        attackPoint = PlaceAttackPoint();
    }

    // TODO: figure out if theres a better system to set this, right now it just uses the raw values of the attack point and directly sets the offset
    public Vector3 PlaceAttackPoint()
    {   // the position of the current string's attack point is placed relative to the player's transform
        return transform.position +
            (transform.right * CurrentStringAttackPoint.position.x) +
            (transform.up * CurrentStringAttackPoint.position.y) +
            (transform.forward * CurrentStringAttackPoint.position.z);
    }

    public override void OnValidate()
    {
        base.OnValidate();
        if (customName == null)
        {
            customName = "Combat";
        }
        mainStateType = new IdleCombatState();
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint, attackRadius);
    }
}
