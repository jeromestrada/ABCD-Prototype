using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class Weapon : Item
{
    WeaponManager weaponManager;

    public int damage;
    public int stringAttacksCount;
    public Transform[] attackPoints; // an array of attack points that we can access to resolve attack hits
    public WeaponAnimations weaponAnimations;
    public SkinnedMeshRenderer mesh;

    public override void Use()
    {
        // this part seems wrong, it gives this weapon access to the weaponManager but it just doesn't look right.
        weaponManager = GameObject.Find("Player").GetComponent<WeaponManager>();
        weaponManager.EquipWeapon(this);
        base.Use();
    }
}
