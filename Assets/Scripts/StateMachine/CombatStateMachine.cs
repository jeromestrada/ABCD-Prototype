using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStateMachine : StateMachine
{
    [SerializeField] protected PlayerStats playerStats;
    protected Transform currentStringAttackPoint;
    protected float attackRadius = 3f;

    public LayerMask enemyMask;

    EquipmentManager weaponManager;
    [SerializeField] protected Equipment equippedWeapon;
    public bool canStringAttack;

    [SerializeField] ComboCharacter combo;

    bool isCoolingDown = false;
    float finalStringTime;
    public float cooldownDuration = 3f;

    public static event System.Action<int> OnAttack;
    PlayerMovement controller;
    [SerializeField] MouseItemData mouseItemData;

    public PlayerStats PlayerStats => playerStats;
    public Equipment EquippedWeapon => equippedWeapon;
    public Transform CurrentStringAttackPoint => currentStringAttackPoint;
    public float AttackRadius => attackRadius;

    void OnEnable()
    {
        EquipmentManager.OnEquipmentChanged += OnWeaponChanged;
    }

    void OnDisable()
    {
        EquipmentManager.OnEquipmentChanged -= OnWeaponChanged;
    }

    private void Start()
    {
        controller = GetComponent<PlayerMovement>();
    }

    public override void Update()
    {
        base.Update();
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

    public void UpdateAttackPoint(Transform newAttackPoint)
    {
        currentStringAttackPoint = newAttackPoint;
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
}
