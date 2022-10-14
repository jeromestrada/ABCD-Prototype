using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    [SerializeField] List<int> _attackModifiers;
    [SerializeField] List<int> _armorModifiers;

    public List<int> AttackModifiers => _attackModifiers;
    public List<int> ArmorModifiers => _armorModifiers;

    public override void TakeDamage(int damage)
    {
        var reducedDamage = Mathf.Clamp((damage - Armor), 0, damage);
        Debug.Log($"{gameObject.name} is taking damage... {reducedDamage}");
        base.TakeDamage(reducedDamage);
    }
    public override void Die()
    {
        GetComponent<PlayerController>().enabled = false;
        base.Die();
    }
}
