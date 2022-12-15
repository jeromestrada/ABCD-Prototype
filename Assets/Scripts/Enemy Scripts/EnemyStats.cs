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
        ApplyMoonBuff(moon);
    }

    public override void ApplyMoonBuff(Moon moon)
    {
        
        // Enemy specific buff logic here.
        if (moon.CurrentMoon == MoonPhase.New)
        {
            ApplyBuff(Buff("New Moon Buff"));
        }
        else
        {
            RemoveBuff(Buff("New Moon Buff"));
        }
        // Debug.Log($"EnemyStats: In {gameObject.name}'s Buff(). Moon phase received = {moon.CurrentMoon}");
        base.ApplyMoonBuff(moon);
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
