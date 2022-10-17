using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    protected int _currentHealth;

    [SerializeField] private Stat _damage;
    [SerializeField] private Stat _armor;

    public int MaxHealth => _maxHealth;
    public Stat Damage => _damage;
    public Stat Armor => _armor;

    public event System.Action<int, int> OnHealthChanged;
    public event System.Action OnDying;

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

        if (_currentHealth <= 0)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        Debug.Log($"{gameObject.name} just died!");
        if (OnDying != null)
        {
            OnDying();
        }
        this.enabled = false;
    }
}