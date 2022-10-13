using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterStats
{
    public override void TakeDamage(int damage)
    {
        Debug.Log($"{gameObject.name} is taking damage...");
        base.TakeDamage(damage);
    }

    public override void Die()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<EnemyCombatAI>().enabled = false;
        base.Die();
    }
}
