using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // will manage equipping a weapon, 
    // this includes updating the character animator's animation based on the weapon
    // handles the MeshRenderer to display the weapon being wielded by the player.

    public event System.Action onWeaponChanged;
    public Weapon equippedWeapon;
    public Weapon testEquip;
    SkinnedMeshRenderer equippedWeaponMesh;
    public SkinnedMeshRenderer targetMesh;

    private void Start()
    {
        equippedWeaponMesh = new SkinnedMeshRenderer();
        EquipWeapon(testEquip);
    }

    public void EquipWeapon(Weapon weapon)
    {
        equippedWeapon = weapon;
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(weapon.mesh);
        newMesh.transform.parent = targetMesh.transform;
        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;

        equippedWeaponMesh = newMesh;
        onWeaponChanged?.Invoke();
    }

    public void UnequipWeapon()
    {
        if(equippedWeaponMesh != null) Destroy(equippedWeaponMesh.gameObject);
        equippedWeapon = null;
        onWeaponChanged?.Invoke();
    }
}
