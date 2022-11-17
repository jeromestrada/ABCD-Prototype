using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStateMachine : StateMachine
{
    [SerializeField] private PlayerStats playerStats;
    private Vector3 attackPoint;
    private Transform currentStringAttackPoint;

    public float attackRadius = 3f;
    public LayerMask enemyMask;

    EquipmentManager weaponManager;
    [SerializeField] Equipment equippedWeapon;
    public bool canStringAttack;
    public int CurrentAttackString;



    private float lastAttackStringTime;
    public float stringGracePeriod = 0.5f;
    // TODO: replace the grace period system with a transition phase in between attacks

    [SerializeField] ComboCharacter combo;

    bool isCoolingDown = false;
    float finalStringTime;
    public float cooldownDuration = 3f;

    public static event System.Action<int> OnAttack;
    PlayerMovement controller;
    [SerializeField] MouseItemData mouseItemData;

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
        lastAttackStringTime = 0;
        canStringAttack = true;
        ResetAttackString();
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
                ResetAttackString();
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

    public void AttackHit_AnimationEvent()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint, attackRadius, enemyMask);
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyStats>().TakeDamage(playerStats.Damage.GetValue());
        }
    }

    public void ResetAttackString()
    {
        CurrentAttackString = 0;
        if (equippedWeapon != null) currentStringAttackPoint = equippedWeapon.AttackPoints[CurrentAttackString];
    }

    private void OnDrawGizmosSelected()
    {
        if (currentStringAttackPoint == null)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint, attackRadius);
    }
}
