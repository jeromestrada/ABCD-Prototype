using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour , MoonBound, HungerBound, Buffable
{
    [SerializeField] private int _maxHealth = 100;
    protected int _currentHealth;
    [SerializeField] protected List<Stat> _statsList;
    protected List<Modifier> _modifiers;

    

    public int MaxHealth => _maxHealth;

    

    public Stat Damage => _statsList.Find(s => s.StatName == "Damage");
    public Stat Armor => _statsList.Find(s => s.StatName == "Armor");
    public Stat Movespeed => _statsList.Find(s => s.StatName == "Movespeed");
    public Stat CritChance => _statsList.Find(s => s.StatName == "CriticalChance");
    public Stat CritDamage => _statsList.Find(s => s.StatName == "CriticalDamage");

    public List<Modifier> Modifiers => _modifiers;

    public List<Buff> Buffs = new List<Buff>();

    [SerializeField] protected BuffsList BuffsList;

    public event System.Action<int, int> OnHealthChanged;
    public event System.Action OnDying;
    public event System.Action<CharacterStats> OnTakeDamage;
    public static event System.Action OnStatChange;

    protected void Awake()
    {
        _currentHealth = MaxHealth;
        _modifiers = new List<Modifier>();
        if(_statsList == null) _statsList = new List<Stat>();

        OnStatChange?.Invoke();
    }

    public Stat Stat(string statName)
    {
        return _statsList.Find(s => s.StatName == statName);
    }

    // wrapper method for getting the buff by name
    public Buff Buff(string buffName)
    {
        return BuffsList.Buff(buffName);
    }

    public virtual void ApplyBuff(string buff)
    {
        var b = Buff(buff);
        if(b == null) return;
        b.Apply(this);
        Buffs.Add(b);
    }

    public virtual void RemoveBuff(string buffName)
    {
        var buff = Buff(buffName);
        if (buff == null) return;
        buff.Purge(this);
        Buffs.Remove(buff);
    }

    public virtual void ApplyBuff(Buff buff)
    {
        Debug.Log($"Buff is {buff.Name}");
        if(buff == null) return;
        buff.Apply(this);
        Buffs.Add(buff);
    }

    public virtual void RemoveBuff(Buff buff)
    {
        if (buff == null) return;
        buff.Purge(this);
        Buffs.Remove(buff);
    }

    /// <summary>
    /// Adds the given Modifier to the given Stat.
    /// </summary>
    /// <param name="stat"></param>
    /// <param name="modifier"></param>
    public void AddStatModifier(Stat stat, Modifier modifier)
    {
        stat.AddModifier(modifier);
        /*_modifiers.Add(modifier);*/
    }

    /// <summary>
    /// Removes the given Modifier from the given Stat.
    /// </summary>
    /// <param name="stat"></param>
    /// <param name="modifier"></param>
    public void RemoveStatModifier(Stat stat, Modifier modifier)
    {
        if(stat != null && modifier != null)
        {
            stat.RemoveModifier(modifier);
            //_modifiers.Remove(modifier);
        }
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
        // Debug.Log($"{gameObject.name} affected by new moon phase.");
        OnStatChange?.Invoke();
    }

    public virtual void ApplyHungerStatus(HungerSystem hungerSystem)
    {
        OnStatChange?.Invoke();
    }
}

public enum StatType { Damage, Armor, Movespeed, CriticalDamage, CriticalChance }
