using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private int healthAmount;
    [SerializeField] private int manaAmount;
    [SerializeField] private float regenRate = 2.0f; // default to regen every 2 seconds
    private float lastHealthRegenTime;
    private float lastManaRegenTime;
    private bool regeningHp = false;
    private bool regeningMp = false;
    public static event System.Action<int> OnRegenerateHP;
    public static event System.Action<int> OnRegenerateMP;


    private void OnEnable()
    {
        EngagementBubble.OnEngage += Engage;
    }

    private void OnDisable()
    {
        EngagementBubble.OnEngage -= Engage;
    }

    // Start is called before the first frame update
    void Start()
    {
        lastHealthRegenTime = Time.time;
        lastManaRegenTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerStats.fullHp && Time.time - lastHealthRegenTime >= regenRate)
        {
            if (!regeningHp)
            {
                regeningHp = true;
                Invoke(nameof(RegenHealth), regenRate);
                lastHealthRegenTime = Time.time;
            }
        }
        else if (playerStats.fullHp)
        {
            regeningHp = false;
            lastHealthRegenTime = Time.time;
        }

        if (!playerStats.fullMana && Time.time - lastManaRegenTime >= regenRate)
        {
            if (!regeningMp)
            {
                regeningMp = true;
                Invoke(nameof(RegenMana), regenRate);
                lastManaRegenTime = Time.time;
            }
        }
        else if (playerStats.fullMana)
        {
            regeningMp = false;
            lastManaRegenTime = Time.time;
        }
    }

    void RegenHealth()
    {
        OnRegenerateHP?.Invoke(healthAmount);
        
        regeningHp = false;
    }

    void RegenMana()
    {
        OnRegenerateMP?.Invoke(manaAmount);
        
        regeningMp = false;
    }

    void Engage(float potency)
    {
        regenRate += potency;
    }
}
