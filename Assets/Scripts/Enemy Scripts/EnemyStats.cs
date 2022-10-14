using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public override void TakeDamage(int damage)
    {
        var reducedDamage = Mathf.Clamp((damage - Armor), 0, damage);
        Debug.Log($"{gameObject.name} is taking damage...");
        base.TakeDamage(reducedDamage);
    }

    public override void Die()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<EnemyCombatAI>().enabled = false;
        base.Die();
    }
}
