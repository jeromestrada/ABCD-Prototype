using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{

    public static event System.Action OnPlayerDying;

    private void OnEnable()
    {
        EquipmentManager.OnEquipmentChanged += UpdatePlayerStats;
        HandOfCards.OnHandChanged += UpdatePlayerStats;
        MoonPhaseSystem.OnMoonPhaseChange += UpdatePlayerStats;
        ConsumableManager.OnConsumableHandled += TakeConsumable;
        HeartSystemHolder.OnHeartsChanged += RealDeath;
    }

    private void OnDisable()
    {
        EquipmentManager.OnEquipmentChanged -= UpdatePlayerStats;
        HandOfCards.OnHandChanged -= UpdatePlayerStats;
        MoonPhaseSystem.OnMoonPhaseChange += UpdatePlayerStats;
        ConsumableManager.OnConsumableHandled -= TakeConsumable;
        HeartSystemHolder.OnHeartsChanged -= RealDeath;
    }

    private void UpdatePlayerStats(Moon moon)
    {
        Buff(moon);
    }

    public override void Buff(Moon moon)
    {
        base.Buff(moon);
        // Player specific buff logic should be here.
        Debug.Log($"PlayerStats: In {gameObject.name}'s Buff(). Moon phase received = {moon.CurrentMoon}");
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

    private void UpdatePlayerStats(Card statCard)
    {
        if(statCard.CardType == CardType.StatCard)
        {
            StatCard statCardCasted = (StatCard) statCard;
            if (statCardCasted.StatCardType == StatCardType.DamageStat) Damage.AddModifier(statCardCasted.StatBonus.GetValue());
            else if (statCardCasted.StatCardType == StatCardType.ArmorStat) Armor.AddModifier(statCardCasted.StatBonus.GetValue());
            // more stat type handling can be added here, i.e. movespeed, cooldown reduction, hp/mana regen stats, etc..
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
