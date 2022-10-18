using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    protected void Start()
    {
        EquipmentManager.OnEquipmentChanged += UpdatePlayerStats;
    }

    private void UpdatePlayerStats(Equipment oldEquipment, Equipment newEquipment)
    {
        if(newEquipment != null)
        {
            if (newEquipment.ItemType == EquipmentType.Weapon) Damage.AddModifier(newEquipment.Damage);
            else if (newEquipment.ItemType == EquipmentType.Protection) Armor.AddModifier(newEquipment.Armor);
        }
        if (oldEquipment != null)
        {
            if (oldEquipment.ItemType == EquipmentType.Weapon) Damage.RemoveModifier(oldEquipment.Damage);
            else if (oldEquipment.ItemType == EquipmentType.Protection) Armor.RemoveModifier(oldEquipment.Armor);
        }
    }

    public override void TakeDamage(int damage)
    {
        // call a hurt animation here for the player to play
        Debug.Log($"{gameObject.name} is taking damage... {damage}");
        base.TakeDamage(damage);
    }
    public override void Die()
    {
        GetComponent<PlayerController>().enabled = false;
        base.Die();
    }
}
