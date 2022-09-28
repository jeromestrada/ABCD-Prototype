using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class Weapon : Item
{
    public int damage;
    public int stringAttacksCount;
    public Transform[] attackPoints; // an array of attack points that we can access to resolve attack hits
    public WeaponAnimations weaponAnimations;
    public SkinnedMeshRenderer mesh;

    public override void Use()
    {
        // Equip the weapon here
        base.Use();
    }
}
