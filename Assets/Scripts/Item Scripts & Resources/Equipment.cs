using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    [SerializeField] private int _damage;
    [SerializeField] private int _armor;
    [SerializeField] private EquipmentType _type;

    [SerializeField] private int _stringAttacksCount;
    [SerializeField] private Transform[] _attackPoints; // an array of attack points that we can access to resolve attack hits
    [SerializeField] private WeaponAnimations _weaponAnimations;

    public int Damage => _damage;
    public int Armor => _armor;
    public int StringAttacksCount => _stringAttacksCount;
    public Transform[] AttackPoints => _attackPoints;
    public WeaponAnimations WeaponAnimations => _weaponAnimations;
    

    public override void Use()
    {// this part seems wrong, it gives this weapon access to the weaponManager but it just doesn't look right.
        equipmentManager = GameObject.Find("Player").GetComponent<EquipmentManager>();
        equipmentManager.Equip(this);
        base.Use();
    }
}
