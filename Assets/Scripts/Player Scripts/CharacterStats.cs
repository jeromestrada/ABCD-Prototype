using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour , MoonBound
{
    [SerializeField] private int _maxHealth = 100;
    protected int _currentHealth;

    [SerializeField] private Stat _damage;
    [SerializeField] private Stat _armor;
    // stats like movespeed, hp/mana regen, cdr, etc can be added here.

    public int MaxHealth => _maxHealth;
    public Stat Damage => _damage;
    public Stat Armor => _armor;

    public event System.Action<int, int> OnHealthChanged;
    public event System.Action OnDying;
    public event System.Action<CharacterStats> OnTakeDamage;

    protected void Awake()
    {
        _currentHealth = MaxHealth;
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

    public virtual void Buff(Moon moon)
    {
        Debug.Log($"{gameObject.name} affected by new moon phase.");
    }
}
