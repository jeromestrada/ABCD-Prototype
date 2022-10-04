using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    public event System.Action OnTakingDamage;
    public event System.Action OnDying;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        if (OnTakingDamage != null)
        {
            OnTakingDamage();
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Die();
            }
        }        
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        if(OnDying != null)
        {
            OnDying();
        }
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<EnemyAI>().enabled = false;
        this.enabled = false;
    }
}
