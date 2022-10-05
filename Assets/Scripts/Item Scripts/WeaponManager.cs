using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // will manage equipping a weapon, 
    // this includes providing a delegate to the character animator to deal with animation changes
    // handles the MeshRenderer to display the weapon being wielded by the player.

    public event System.Action<Weapon> onWeaponChanged;
    public Weapon equippedWeapon;
    public Weapon testEquip;
    public Weapon testEquipReverse;
    SkinnedMeshRenderer equippedWeaponMesh;
    public SkinnedMeshRenderer targetMesh;

    private void Start()
    {
        equippedWeaponMesh = new SkinnedMeshRenderer();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Test equip");
            EquipWeapon(testEquip);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Test equip reverse");
            EquipWeapon(testEquipReverse);
        } 
    }

    public void EquipWeapon(Weapon weapon)
    {   // only proceed when the weapon being equipped isn't the same as the equipped
        if(weapon != equippedWeapon)
        {
            UnequipWeapon();

            equippedWeapon = weapon;
            // Render the weapon mesh
            SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(weapon.mesh);
            newMesh.transform.parent = targetMesh.transform;
            newMesh.bones = targetMesh.bones;
            newMesh.rootBone = targetMesh.rootBone;
            equippedWeaponMesh = newMesh;
            onWeaponChanged?.Invoke(equippedWeapon);
        }
    }

    public void UnequipWeapon()
    {
        if(equippedWeaponMesh != null) Destroy(equippedWeaponMesh.gameObject);
        equippedWeapon = null;
        onWeaponChanged?.Invoke(equippedWeapon);
    }
}
