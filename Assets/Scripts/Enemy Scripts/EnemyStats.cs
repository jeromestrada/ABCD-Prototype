using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private void OnEnable()
    {
        MoonPhaseSystem.OnMoonPhaseChange += UpdateEnemyStats;
    }

    private void OnDisable()
    {
        MoonPhaseSystem.OnMoonPhaseChange -= UpdateEnemyStats;
    }

    private void UpdateEnemyStats(Moon moon)
    {
        Buff(moon);
    }

    public override void Buff(Moon moon)
    {
        
        // Enemy specific buff logic here.
        if (moon.CurrentMoon == MoonPhase.New)
        {
            Damage.AddModifier(15);
            Movespeed.AddModifier(5);
        }
        else
        {
            Damage.RemoveModifier(15);
            Movespeed.RemoveModifier(5);
        }
        Debug.Log($"EnemyStats: In {gameObject.name}'s Buff(). Moon phase received = {moon.CurrentMoon}");
        base.Buff(moon);
    }

    public override void TakeDamage(int damage)
    {
        var reducedDamage = Mathf.Clamp((damage - Armor.GetValue()), 0, damage);
        // Debug.Log($"{gameObject.name} is taking damage...");
        base.TakeDamage(reducedDamage);
    }

    public override void Die()
    {
        base.Die();
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<EnemyCombatAI>().enabled = false;
        this.enabled = false;
    }
}
