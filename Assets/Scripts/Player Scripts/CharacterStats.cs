using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour , MoonBound, HungerBound
{
    [SerializeField] private int _maxHealth = 100;
    protected int _currentHealth;

    [SerializeField] private Stat _damage;
    [SerializeField] private Stat _armor;
    [SerializeField] private Stat _movespeed;
    // stats like movespeed, hp/mana regen, cdr, etc can be added here.

    // Add a list of Modifiers that the subclasses can reference to handle stat changes such as adding/removing modifiers based on their names.
    protected List<Modifier> _modifiers;

    public int MaxHealth => _maxHealth;
    public Stat Damage => _damage;
    public Stat Armor => _armor;
    public Stat Movespeed => _movespeed;

    public List<Modifier> Modifiers => _modifiers;

    public event System.Action<int, int> OnHealthChanged;
    public event System.Action OnDying;
    public event System.Action<CharacterStats> OnTakeDamage;
    public static event System.Action OnStatChange;

    protected void Awake()
    {
        _currentHealth = MaxHealth;
        OnStatChange?.Invoke();
        _modifiers = new List<Modifier>();
        Damage.ClearModifiers();
        Armor.ClearModifiers();
        Movespeed.ClearModifiers();
    }

    /// <summary>
    /// Adds the given Modifier to the given Stat.
    /// </summary>
    /// <param name="stat"></param>
    /// <param name="modifier"></param>
    public void AddStatModifier(Stat stat, Modifier modifier)
    {
        stat.AddModifier(modifier);
        _modifiers.Add(modifier);
    }

    /// <summary>
    /// Removes the given Modifier from the given Stat.
    /// </summary>
    /// <param name="stat"></param>
    /// <param name="modifier"></param>
    public void RemoveStatModifier(Stat stat, Modifier modifier)
    {
        stat.RemoveModifier(modifier);
        _modifiers.Remove(modifier);
    }

    public virtual void TakeDamage(int damage)
    {
        damage -= Armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        _currentHealth -= damage;
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        OnTakeDamage?.Invoke(this);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Heal(int healing)
    {
        healing = Mathf.Clamp(healing, 0, int.MaxValue); // look out for negative healing.

        _currentHealth += healing;
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        Debug.Log($"Healed for {healing} amount");
    }

    public virtual void Die()
    {
        // Debug.Log($"{gameObject.name} just died!");
        if (OnDying != null)
        {
            OnDying();
        }
    }

    public virtual void ApplyMoonBuff(Moon moon) // Moon bound buff, overriden by a specific unit type: player, different enemies, neutrals, etc...
    {
        Debug.Log($"{gameObject.name} affected by new moon phase.");
        OnStatChange?.Invoke();
    }

    public virtual void ApplyHungerStatus(HungerSystem hungerSystem)
    {
        OnStatChange?.Invoke();
    }
}
