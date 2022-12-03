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
                AddStatModifier(Movespeed, new Modifier("Drowsy Movement", -3));
                //Movespeed.AddModifier(-3);
                RemoveStatModifier(Damage, _modifiers.Find(x => x.ModifierName == "Starving Damage Buff"));
                //Damage.RemoveModifier(10);
                RemoveStatModifier(Damage, _modifiers.Find(x => x.ModifierName == "Hungry Damage Buff"));
                //Damage.RemoveModifier(5);
                break;
            case HungerState.Hungry:
                Debug.Log("HUNGRY status!");
                RemoveStatModifier(Movespeed, _modifiers.Find(x => x.ModifierName == "Drowsy Movement"));
                //Movespeed.RemoveModifier(-3);
                AddStatModifier(Damage, new Modifier("Hungry Damage Buff", 5));
                //Damage.AddModifier(5);
                break;
            case HungerState.Starving:
                Debug.Log("STARVING status!");
                AddStatModifier(Damage, new Modifier("Starving Damage Buff", 10));
                //Damage.AddModifier(10);
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
            AddStatModifier(Damage, new Modifier("Full Moon Damage", 50));
            //Damage.AddModifier(50);
            AddStatModifier(Movespeed, new Modifier("Full Moon Movespeed", 5));
            //Movespeed.AddModifier(5);
        }
        else
        {
            RemoveStatModifier(Damage, _modifiers.Find(x => x.ModifierName == "Full Moon Damage"));
            //Damage.RemoveModifier(50);
            RemoveStatModifier(Movespeed, _modifiers.Find(x => x.ModifierName == "Full Moon Movespeed"));
            //Movespeed.RemoveModifier(5);
        }
        Debug.Log($"PlayerStats: In {gameObject.name}'s Buff(). Moon phase received = {moon.CurrentMoon}");
        base.Buff(moon);
    }

    private void UpdatePlayerStats(Equipment oldEquipment, Equipment newEquipment)
    {
        if(newEquipment != null)
        {
            if (newEquipment.ItemType == EquipmentType.Weapon) AddStatModifier(Damage, new Modifier(newEquipment.name, newEquipment.Damage));
            else if (newEquipment.ItemType == EquipmentType.Protection) AddStatModifier(Armor, new Modifier(newEquipment.name, newEquipment.Armor));
        }
        if (oldEquipment != null)
        {
            if (oldEquipment.ItemType == EquipmentType.Weapon) RemoveStatModifier(Damage, _modifiers.Find(x => x.ModifierName == oldEquipment.name));
            //Damage.RemoveModifier(oldEquipment.Damage);
            else if (oldEquipment.ItemType == EquipmentType.Protection) RemoveStatModifier(Armor, _modifiers.Find(x => x.ModifierName == oldEquipment.name));
            //Armor.RemoveModifier(oldEquipment.Armor);
        }
    }

    //. TODO: stat cards should remove their effects when used
    private void UpdatePlayerStats(StatCard statCard, bool isDrawn)
    {
        if (isDrawn)
        {
            if (statCard.StatCardType == StatCardType.DamageStat) AddStatModifier(Damage, new Modifier(statCard.name, statCard.StatBonus.GetValue()));
            //Damage.AddModifier(statCard.StatBonus.GetValue());
            else if (statCard.StatCardType == StatCardType.ArmorStat) AddStatModifier(Armor, new Modifier(statCard.name, statCard.StatBonus.GetValue()));
            // Armor.AddModifier(statCard.StatBonus.GetValue());
            // more stat type handling can be added here, i.e. movespeed, cooldown reduction, hp/mana regen stats, etc..
        }
        else // the stat card is either being discarded or used (which is a form of discard)
        {
            if (statCard.StatCardType == StatCardType.DamageStat) RemoveStatModifier(Damage, _modifiers.Find(x => x.ModifierName == statCard.name));
            //Damage.RemoveModifier(statCard.StatBonus.GetValue());
            else if (statCard.StatCardType == StatCardType.ArmorStat) RemoveStatModifier(Armor, _modifiers.Find(x => x.ModifierName == statCard.name));
            // Armor.RemoveModifier(statCard.StatBonus.GetValue());
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
