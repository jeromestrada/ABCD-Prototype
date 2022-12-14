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
                Debug.Log("FULL status!");
                // AddStatModifier(Movespeed, new Modifier("Drowsy Movement", -3));
                ApplyBuff(BuffsList.Buffs.Find(x => x.Name == "Drowsy"));

                RemoveStatModifier(Damage, _modifiers.Find(x => x.ModifierName == "Starving Damage Buff"));
                RemoveStatModifier(Damage, _modifiers.Find(x => x.ModifierName == "Hungry Damage Buff"));
                break;

            case HungerState.Hungry:
                Debug.Log("HUNGRY status!");
                AddStatModifier(Damage, new Modifier("Hungry Damage Buff", 5));
                //RemoveStatModifier(Movespeed, _modifiers.Find(x => x.ModifierName == "Drowsy Movement"));
                RemoveBuff(BuffsList.Buffs.Find(x => x.Name == "Drowsy"));
                break;

            case HungerState.Starving:
                Debug.Log("STARVING status!");
                AddStatModifier(Damage, new Modifier("Starving Damage Buff", 10));
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
        // Player specific buff logic should be here.
        if(moon.CurrentMoon == MoonPhase.Full)
        {
            AddStatModifier(Damage, new Modifier("Full Moon Damage", 50));
            AddStatModifier(Movespeed, new Modifier("Full Moon Movespeed", 5));
        }
        else
        {
            RemoveStatModifier(Damage, _modifiers.Find(x => x.ModifierName == "Full Moon Damage"));
            RemoveStatModifier(Movespeed, _modifiers.Find(x => x.ModifierName == "Full Moon Movespeed"));
        }
        base.ApplyMoonBuff(moon);
    }

    private void UpdatePlayerStats(Equipment oldEquipment, Equipment newEquipment)
    {
        if(newEquipment != null)
        {
            switch (newEquipment.ItemType)
            {
                case EquipmentType.Weapon:
                    AddStatModifier(Damage, new Modifier(newEquipment.name, newEquipment.Damage));
                    break;
                case EquipmentType.Protection:
                    AddStatModifier(Armor, new Modifier(newEquipment.name, newEquipment.Armor));
                    break;
            }
        }
        if (oldEquipment != null)
        {
            switch (oldEquipment.ItemType)
            {
                case EquipmentType.Weapon:
                    RemoveStatModifier(Damage, _modifiers.Find(x => x.ModifierName == oldEquipment.name));
                    break;
                case EquipmentType.Protection:
                    RemoveStatModifier(Armor, _modifiers.Find(x => x.ModifierName == oldEquipment.name));
                    break;
            }
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
            // more stat type handling can be added here, i.e. movespeed, cooldown reduction, hp/mana regen stats, etc..
            switch (statCard.StatCardType)
            {
                case StatCardType.DamageStat:
                    AddStatModifier(Damage, new Modifier(statCard.name, statCard.StatBonus.GetValue()));
                    break;
                case StatCardType.ArmorStat:
                    AddStatModifier(Armor, new Modifier(statCard.name, statCard.StatBonus.GetValue()));
                    break;
            }
        }
        else // the stat card is either being discarded or used (which is a form of discard)
        {
            switch (statCard.StatCardType)
            {
                case StatCardType.DamageStat:
                    RemoveStatModifier(Damage, _modifiers.Find(x => x.ModifierName == statCard.name));
                    break;
                case StatCardType.ArmorStat:
                    RemoveStatModifier(Armor, _modifiers.Find(x => x.ModifierName == statCard.name));
                    break;
            }
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
