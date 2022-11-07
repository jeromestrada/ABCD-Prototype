using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Dash ability allows the ability holder move quickly for a given time.
 
 */
[CreateAssetMenu(fileName = "Dash", menuName = "Abilities/Block")]
public class BlockAbility : Ability
{
    [SerializeField] private int _blockAmount;

    public int BlockAmount => _blockAmount;
    public bool isBlocking;
    public float blockStartTime;
    public float blockDuration;

    public static event System.Action OnBlock;
    public static event System.Action OnBlockEnd;

    private void Awake()
    {
        isActivated = false;
    }
    public override void Activate()
    {
        OnStartBlock();
    }

    public override void UpdateAbility(PlayerMovement movement, CharacterStats stats)
    {
        if (isActivated)
        {
            if (Time.time - blockStartTime <= blockDuration)
            {
                if (!isBlocking)
                {
                    stats.Armor.AddModifier(BlockAmount);
                    isBlocking = true;
                }
                
            }
            else
            {
                OnEndBlock();
                stats.Armor.RemoveModifier(BlockAmount);
            }
        }
    }

    void OnStartBlock()
    {
        isActivated = true;
        blockStartTime = Time.time;
        OnBlock?.Invoke();
    }
    public void OnEndBlock()
    {
        isActivated = false;
        isBlocking = false;
        blockStartTime = 0;
        OnBlockEnd?.Invoke();
    }
}
