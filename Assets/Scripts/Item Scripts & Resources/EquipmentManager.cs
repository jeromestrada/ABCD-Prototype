using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    // will manage equipping a weapon, 
    // this includes providing a delegate to the character animator to deal with animation changes
    // handles the MeshRenderer to display the weapon being wielded by the player.

    public static event System.Action<Equipment, Equipment> OnEquipmentChanged; // <old, new>
    public Equipment equippedWeapon;
    public Equipment testEquip;
    public Equipment testEquipReverse;
    SkinnedMeshRenderer equippedWeaponMesh;

    [SerializeField] private Equipment equippedProtection;
    SkinnedMeshRenderer equippedProtectionMesh;

    public SkinnedMeshRenderer targetMesh;

    private void Start()
    {
        equippedWeaponMesh = new SkinnedMeshRenderer();
    }

    public void Equip(Equipment equipment)
    {
        int equipmentType = (int)equipment.ItemType;

        if (equipment != null)
        {
            Equipment oldEquipment = Unequip(equipmentType);

            OnEquipmentChanged?.Invoke(oldEquipment, equipment);
            if(equipmentType == 0)
            {
                equippedWeapon = (Equipment)equipment;
                SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(equipment.mesh);
                newMesh.transform.parent = targetMesh.transform;
                newMesh.bones = targetMesh.bones;
                newMesh.rootBone = targetMesh.rootBone;
                equippedWeaponMesh = newMesh;
            }
            else if (equipmentType == 1)
            {
                // TODO: add a mesh logic for armor meshes to be rendered
                //SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(equipment.mesh);
                equippedProtection = (Equipment)equipment;
            }
        }
    }

    public Equipment Unequip(int equipmentType)
    {
        if(equipmentType == 0 && equippedWeapon != null)
        {
            if (equippedWeaponMesh != null)
            {
                if(equippedWeaponMesh != null) Destroy(equippedWeaponMesh.gameObject);
            }
            Equipment oldEquipment = equippedWeapon;
            equippedWeapon = null;
            OnEquipmentChanged?.Invoke(equippedWeapon, null);
            return oldEquipment;
        }
        else if (equipmentType == 1 && equippedProtection != null)
        {
            if (equippedProtectionMesh != null)
            {
                if (equippedProtectionMesh != null) Destroy(equippedProtectionMesh.gameObject);
            }
            Equipment oldEquipment = equippedProtection;
            equippedProtection = null;
            OnEquipmentChanged?.Invoke(equippedProtection, null);
            return oldEquipment;
        }

        return null;
    }
}
