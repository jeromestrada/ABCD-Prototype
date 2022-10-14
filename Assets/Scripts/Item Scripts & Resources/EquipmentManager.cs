using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    // will manage equipping a weapon, 
    // this includes providing a delegate to the character animator to deal with animation changes
    // handles the MeshRenderer to display the weapon being wielded by the player.

    public static event System.Action<Item, Item> OnEquipmentChanged; // <old, new>
    public Weapon equippedWeapon;
    public Weapon testEquip;
    public Weapon testEquipReverse;
    SkinnedMeshRenderer equippedWeaponMesh;

    [SerializeField] private Protection equippedProtection;
    SkinnedMeshRenderer equippedProtectionMesh;

    public SkinnedMeshRenderer targetMesh;

    private void Start()
    {
        equippedWeaponMesh = new SkinnedMeshRenderer();
        
    }

    public void Equip(Item equipment)
    {
        int itemType = (int)equipment.ItemType;

        // only proceed when the weapon being equipped isn't the same as the equipped
        if(equipment != null)
        {
            int equipmentType = (int)equipment.ItemType;
            Item oldEquipment = Unequip(equipmentType);

            OnEquipmentChanged?.Invoke(oldEquipment, equipment);
            if(equipmentType == 0)
            {
                equippedWeapon = (Weapon)equipment;
                SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(equipment.mesh);
                newMesh.transform.parent = targetMesh.transform;
                newMesh.bones = targetMesh.bones;
                newMesh.rootBone = targetMesh.rootBone;
                equippedWeaponMesh = newMesh;
            }
            else if (equipmentType == 1)
            {
                //SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(equipment.mesh);
                equippedProtection = (Protection)equipment;
            }
        }
    }

    public Item Unequip(int itemType)
    {
        if(itemType == 0 && equippedWeapon != null)
        {
            if (equippedWeaponMesh != null)
            {
                if(equippedWeaponMesh != null) Destroy(equippedWeaponMesh.gameObject);
            }
            Item oldEquipment = equippedWeapon;
            equippedWeapon = null;
            OnEquipmentChanged?.Invoke(equippedWeapon, null);
            return oldEquipment;
        }
        else if (itemType == 1 && equippedProtection != null)
        {
            if (equippedProtectionMesh != null)
            {
                if (equippedProtectionMesh != null) Destroy(equippedProtectionMesh.gameObject);
            }
            Item oldEquipment = equippedProtection;
            equippedProtection = null;
            OnEquipmentChanged?.Invoke(equippedProtection, null);
            return oldEquipment;
        }

        return null;
    }
}
