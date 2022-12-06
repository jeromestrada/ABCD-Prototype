using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public event System.Action OnEnemyDied;
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
            AddStatModifier(Damage, new Modifier("newMoonDamage", 15));
            AddStatModifier(Movespeed, new Modifier("newMoonMovespeed", 5));
        }
        else
        {
            RemoveStatModifier(Damage, _modifiers.Find(x => x.ModifierName == "newMoonDamage"));
            RemoveStatModifier(Movespeed, _modifiers.Find(x => x.ModifierName == "newMoonMovespeed"));
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
        OnEnemyDied?.Invoke();
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<EnemyCombatAI>().enabled = false;
        this.enabled = false;
    }
}
