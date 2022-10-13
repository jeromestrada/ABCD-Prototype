using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    protected int _currentHealth;

    public int MaxHealth => _maxHealth;

    public static event System.Action<int, int> OnHealthChanged;
    public event System.Action OnDying;

    protected virtual void Start()
    {
        _currentHealth = MaxHealth;
    }
    public virtual void TakeDamage(int damage)
    {
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
        //this.gameObject.SetActive(false);
    }
}
