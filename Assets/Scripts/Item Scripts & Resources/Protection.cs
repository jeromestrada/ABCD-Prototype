using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Protection", menuName = "Inventory/Protection")]
public class Protection : Item
{
    [SerializeField] private int _armor;
    public int Armor => _armor; 
    private void Awake()
    {
        _itemType = ItemType.Protection;
    }

    public override void Use()
    {// this part seems wrong, it gives this weapon access to the weaponManager but it just doesn't look right.
        equipmentManager = GameObject.Find("Player").GetComponent<EquipmentManager>();
        equipmentManager.Equip(this);
        base.Use();
    }
}
