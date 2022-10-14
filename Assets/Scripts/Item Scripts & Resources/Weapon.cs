using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class Weapon : Item
{
    
    EquipmentManager weaponManager;

    [SerializeField] private int _damage;
    public int stringAttacksCount;
    public Transform[] attackPoints; // an array of attack points that we can access to resolve attack hits
    public WeaponAnimations weaponAnimations;

    public int Damage => _damage;
    private void Awake()
    {
        _itemType = ItemType.Weapon;
    }

    public override void Use()
    {// this part seems wrong, it gives this weapon access to the weaponManager but it just doesn't look right.
        weaponManager = GameObject.Find("Player").GetComponent<EquipmentManager>();
        weaponManager.Equip(this);
        base.Use();
    }
}
