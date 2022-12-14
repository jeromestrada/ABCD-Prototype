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
    [SerializeField] private Buff _blockBuff;

    public Buff BlockBuff => _blockBuff;

    public int BlockAmount => _blockAmount;
    public bool isBlocking;
    public float blockStartTime;
    public float blockDuration;

    public static event System.Action OnBlock;
    public static event System.Action OnBlockEnd;

    public void Awake()
    {
        isActivated = false;
    }
    public override void Activate()
    {
        OnStartBlock();
    }

    public override void UpdateAbility(CharacterController controller, PlayerMovement movement, CharacterStats stats)
    {
        if (isActivated)
        {
            if (Time.time - blockStartTime <= blockDuration)
            {
                if (!isBlocking)
                {
                    stats.ApplyBuff(BlockBuff);
                    isBlocking = true;
                }
            }
            else
            {
                OnEndBlock();
                stats.RemoveBuff(BlockBuff);
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
