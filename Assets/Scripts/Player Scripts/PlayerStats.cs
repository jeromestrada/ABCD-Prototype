using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private int criticalPity = 0;

    public static event System.Action OnPlayerDying;
    public static event System.Action<List<Stat>> OnStatsDisplayUpdateRequested;

    private void OnEnable()
    {
        EquipmentManager.OnEquipmentChanged += UpdatePlayerStats;
        HandOfCards.OnStatCardDrawnDiscarded += UpdatePlayerStats;
        StatCard.OnStatCardUsed += UpdatePlayerStats;
        MoonPhaseSystem.OnMoonPhaseChange += UpdatePlayerStats;
        ConsumableManager.OnConsumableHandled += TakeConsumable;
        HeartSystemHolder.OnHeartsChanged += RealDeath;
        HungerSystem.OnHungerStatusChanged += UpdatePlayerStats;
    }

    private void OnDisable()
    {
        EquipmentManager.OnEquipmentChanged -= UpdatePlayerStats;
        HandOfCards.OnStatCardDrawnDiscarded -= UpdatePlayerStats;
        StatCard.OnStatCardUsed -= UpdatePlayerStats;
        MoonPhaseSystem.OnMoonPhaseChange -= UpdatePlayerStats;
        ConsumableManager.OnConsumableHandled -= TakeConsumable;
        HeartSystemHolder.OnHeartsChanged -= RealDeath;
        HungerSystem.OnHungerStatusChanged -= UpdatePlayerStats;
    }

    public int CriticalHit(int baseDamage)
    {
        var critical = Random.Range(0, 100) + 1;
        if(CritChance.GetValue() > 0) // only have crit pity if there is atleast some critical chance
        {
            if (criticalPity >= 5 || critical <= CritChance.GetValue()) // if we haven't crit for 5 hits straight, we guarantee a crit hit
            {
                criticalPity = 0; // reset the pity
                var finalDamage = (int) (baseDamage + (baseDamage * (float)(CritDamage.GetValue() / 100f)));
                Debug.Log($"CRIT! {finalDamage}");
                return finalDamage;
            }
            criticalPity++;
        }
        
        return baseDamage;
    }

    private void UpdatePlayerStats(HungerSystem hungerSystem)
    {
        ApplyHungerStatus(hungerSystem);
        OnStatsDisplayUpdateRequested?.Invoke(_statsList);
    }

    public override void ApplyHungerStatus(HungerSystem hungerSystem)
    {
        switch (hungerSystem.HungerStatus)
        {
            case HungerState.Full:
                ApplyBuff("Full Buff");
                RemoveBuff("Starving Buff");
                break;

            case HungerState.Hungry:
                ApplyBuff("Hungry Buff");
                RemoveBuff("Full Buff");
                break;

            case HungerState.Starving:
                ApplyBuff("Starving Buff");
                RemoveBuff("Hungry Buff");
                break;
        }
        base.ApplyHungerStatus(hungerSystem);
        OnStatsDisplayUpdateRequested?.Invoke(_statsList);
    }

    private void UpdatePlayerStats(Moon moon)
    {
        ApplyMoonBuff(moon);
        OnStatsDisplayUpdateRequested?.Invoke(_statsList);
    }

    public override void ApplyMoonBuff(Moon moon)
    {
        if(moon.CurrentMoon == MoonPhase.Full)
        {
            ApplyBuff("Full Moon Buff");
        }
        else
        {
            RemoveBuff("Full Moon Buff");
        }
        base.ApplyMoonBuff(moon);
    }

    private void UpdatePlayerStats(Equipment oldEquipment, Equipment newEquipment)
    {
        if(newEquipment != null)
        {
            ApplyBuff(newEquipment.EquipmentBuff);
        }

        if (oldEquipment != null)
        {
            RemoveBuff(oldEquipment.EquipmentBuff);
        }
        OnStatsDisplayUpdateRequested?.Invoke(_statsList);
    }

    /// <summary>
    /// Updates the Player's Stats using the StatCard provided.
    /// </summary>
    /// <param name="statCard"></param>
    /// <param name="isDrawn"></param>
    private void UpdatePlayerStats(StatCard statCard, bool isDrawn)
    {
        if (isDrawn)
        {
            ApplyBuff(statCard.StatBuff);
        }
        else // the stat card is either being discarded or used (which is a form of discard)
        {
            RemoveBuff(statCard.StatBuff);
        }
        OnStatsDisplayUpdateRequested?.Invoke(_statsList);
    }

    public override void TakeDamage(int damage)
    {
        // call a hurt animation here for the player to play
        base.TakeDamage(damage);
    }

    public void TakeConsumable(Consumable consumable)
    {
        switch(consumable.Type)
        {
            case ConsumableType.Healing:
                Debug.Log("That was some relief!");
                Heal(consumable.HealAmount); break;
            default:
                Debug.Log("That was an empty bottle");
                break;
        }
    }

    public override void Heal(int healing)
    {
        base.Heal(healing);
    }
    public override void Die()
    {
        base.Die();
        Debug.Log("Losing a heart...");
        _currentHealth = 0;

        OnPlayerDying?.Invoke();
        // disable movement temporarily, slow down the game
        // animate a revive animation, get some hp back etc...
    }

    public void RealDeath(int hearts) // if hearts dips below 0, really die
    {
        if(hearts < 0)
        {
            Debug.Log("Doom approaches");
            GetComponent<PlayerMovement>().enabled = false;
            this.gameObject.SetActive(false);
            this.enabled = false;
        }
        else
        {
            Heal((int)(MaxHealth / 2));
        }
    }
}
