using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    protected void Start()
    {
        EquipmentManager.OnEquipmentChanged += UpdatePlayerStats;
    }

    private void UpdatePlayerStats(Item oldEquipment, Item newEquipment)
    {
        if(newEquipment != null)
        {
            if (newEquipment.ItemType == ItemType.Weapon) Damage.AddModifier(((Weapon)newEquipment).Damage);
            else if (newEquipment.ItemType == ItemType.Protection) Damage.AddModifier(((Protection)newEquipment).Armor);
        }
        if (oldEquipment != null)
        {
            if (oldEquipment.ItemType == ItemType.Weapon) Damage.RemoveModifier(((Weapon)oldEquipment).Damage);
            else if (oldEquipment.ItemType == ItemType.Protection) Damage.RemoveModifier(((Protection)oldEquipment).Armor);
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
