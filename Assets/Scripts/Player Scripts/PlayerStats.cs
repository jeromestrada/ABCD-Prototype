using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{

    public static event System.Action OnPlayerDying;

    private void OnEnable()
    {
        EquipmentManager.OnEquipmentChanged += UpdatePlayerStats;
        HandOfCards.OnStatCardDrawnDiscarded += UpdatePlayerStats;
        MoonPhaseSystem.OnMoonPhaseChange += UpdatePlayerStats;
        ConsumableManager.OnConsumableHandled += TakeConsumable;
        HeartSystemHolder.OnHeartsChanged += RealDeath;
        HungerSystem.OnHungerStatusChanged += UpdatePlayerStats;
    }

    private void OnDisable()
    {
        EquipmentManager.OnEquipmentChanged -= UpdatePlayerStats;
        HandOfCards.OnStatCardDrawnDiscarded -= UpdatePlayerStats;
        MoonPhaseSystem.OnMoonPhaseChange -= UpdatePlayerStats;
        ConsumableManager.OnConsumableHandled -= TakeConsumable;
        HeartSystemHolder.OnHeartsChanged -= RealDeath;
        HungerSystem.OnHungerStatusChanged -= UpdatePlayerStats;
    }

    private void UpdatePlayerStats(HungerSystem hungerSystem)
    {
        ApplyHungerStatus(hungerSystem);
    }

    public override void ApplyHungerStatus(HungerSystem hungerSystem)
    {
        switch (hungerSystem.HungerStatus)
        {
            case HungerState.Full:
                Debug.Log("FULL status!");
                Movespeed.AddModifier(-3);
                Damage.RemoveModifier(10);
                Damage.RemoveModifier(5);
                break;
            case HungerState.Hungry:
                Debug.Log("HUNGRY status!");
                Movespeed.RemoveModifier(-3);
                Damage.AddModifier(5);
                break;
            case HungerState.Starving:
                Debug.Log("STARVING status!");
                Damage.AddModifier(10);
                break;
            default:
                Debug.Log("DEFAULT status!");
                break;
        }
        base.ApplyHungerStatus(hungerSystem);
    }

    private void UpdatePlayerStats(Moon moon)
    {
        Buff(moon);
    }

    public override void Buff(Moon moon)
    {
        // Player specific buff logic should be here.
        if(moon.CurrentMoon == MoonPhase.Full)
        {
            Damage.AddModifier(50);
            Movespeed.AddModifier(5);
        }
        else
        {
            Damage.RemoveModifier(50);
            Movespeed.RemoveModifier(5);
        }
        Debug.Log($"PlayerStats: In {gameObject.name}'s Buff(). Moon phase received = {moon.CurrentMoon}");
        base.Buff(moon);
    }

    private void UpdatePlayerStats(Equipment oldEquipment, Equipment newEquipment)
    {
        if(newEquipment != null)
        {
            if (newEquipment.ItemType == EquipmentType.Weapon) Damage.AddModifier(newEquipment.Damage);
            else if (newEquipment.ItemType == EquipmentType.Protection) Armor.AddModifier(newEquipment.Armor);
        }
        if (oldEquipment != null)
        {
            if (oldEquipment.ItemType == EquipmentType.Weapon) Damage.RemoveModifier(oldEquipment.Damage);
            else if (oldEquipment.ItemType == EquipmentType.Protection) Armor.RemoveModifier(oldEquipment.Armor);
        }
    }

    private void UpdatePlayerStats(StatCard statCard, bool isDrawn)
    {
        if (isDrawn)
        {
            if (statCard.StatCardType == StatCardType.DamageStat) Damage.AddModifier(statCard.StatBonus.GetValue());
            else if (statCard.StatCardType == StatCardType.ArmorStat) Armor.AddModifier(statCard.StatBonus.GetValue());
            // more stat type handling can be added here, i.e. movespeed, cooldown reduction, hp/mana regen stats, etc..
        }
        else // the stat card is either being discarded / used (which is a form of discard)
        {
            if (statCard.StatCardType == StatCardType.DamageStat) Damage.RemoveModifier(statCard.StatBonus.GetValue());
            else if (statCard.StatCardType == StatCardType.ArmorStat) Armor.RemoveModifier(statCard.StatBonus.GetValue());
        }

    }

    public override void TakeDamage(int damage)
    {
        // call a hurt animation here for the player to play
        // Debug.Log($"{gameObject.name} is taking damage... {damage}");
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
        _currentHealth = 0; // set the current hp to 0 because a fatal blow can overkill
        // player will lose a heart first when reaching 0 hp.
        // only dies when there is no more heart to lose
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
