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

    private void OnEnable()
    {
        Equipment.OnEquipmentUse += Equip;
    }

    private void OnDisable()
    {
        Equipment.OnEquipmentUse -= Equip;
    }

    private void Start()
    {
        equippedWeaponMesh = new SkinnedMeshRenderer();
    }

    public void Equip(Equipment equipment)
    {
        EquipmentType equipmentType = equipment.ItemType;

        if (equipment != null)
        {
            Equipment oldEquipment = Unequip(equipmentType);
            OnEquipmentChanged?.Invoke(oldEquipment, equipment);
            switch (equipmentType)
            {
                case EquipmentType.Weapon:
                    equippedWeapon = (Equipment)equipment;
                    SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(equipment.mesh);
                    newMesh.transform.parent = targetMesh.transform;
                    newMesh.bones = targetMesh.bones;
                    newMesh.rootBone = targetMesh.rootBone;
                    equippedWeaponMesh = newMesh;
                    break;

                case EquipmentType.Protection:
                    equippedProtection = (Equipment)equipment;
                    break;
            }
        }
    }

    public Equipment Unequip(EquipmentType equipmentType)
    {
        Equipment oldEquipment;
        switch (equipmentType)
        {
            case EquipmentType.Weapon:
                if (equippedWeaponMesh != null) Destroy(equippedWeaponMesh.gameObject);
                oldEquipment = equippedWeapon;
                equippedWeapon = null;
                OnEquipmentChanged?.Invoke(equippedWeapon, null);
                return oldEquipment;

            case EquipmentType.Protection:
                if (equippedProtectionMesh != null) Destroy(equippedProtectionMesh.gameObject);
                oldEquipment = equippedProtection;
                equippedProtection = null;
                OnEquipmentChanged?.Invoke(equippedProtection, null);
                return oldEquipment;

            default:
                return null;
        }
    }
}
