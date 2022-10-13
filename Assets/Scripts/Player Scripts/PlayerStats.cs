using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    [SerializeField] private int _currentDamage;
    [SerializeField] private int _currentArmor;

    public int CurrentDamage => _currentDamage;
    public int CurrentArmor => _currentArmor;

    public override void TakeDamage(int damage)
    {
        var reducedDamage = damage - _currentArmor;
        Debug.Log($"Player is taking damage... {reducedDamage}");
        base.TakeDamage(reducedDamage);
    }
    public override void Die()
    {
        GetComponent<PlayerController>().enabled = false;
        base.Die();
    }
}
