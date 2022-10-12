using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    [SerializeField] private int _currentArmor;

    public int CurrentArmor => _currentArmor;

    public override void TakeDamage(int damage)
    {
        Debug.Log($"Player is taking damage... {damage}");
        base.TakeDamage(damage);
    }
    public override void Die()
    {
        GetComponent<PlayerController>().enabled = false;
        base.Die();
    }
}
