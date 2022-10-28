using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Dash ability allows the ability holder move quickly for a given time.
 
 */
[CreateAssetMenu(fileName ="Dash", menuName ="Abilities/Dash")]
public class DashAbility : Ability
{
    [SerializeField] private float _dashSpeed;

    public float DashSpeed => _dashSpeed;

    public bool isDashing;
    public float dashStartTime;
    public float dashDuration;

    PlayerCombat combat;

    public static event System.Action OnDash;
    public static event System.Action OnDashEnd;

    private void Awake()
    {
        isActivated = false;
    }
    public override void Activate()
    {
        OnStartDash();
    }

    public override void UpdateAbility(PlayerMovement movement, CharacterStats stats)
    {
        if (isActivated)
        {
            if (Time.time - dashStartTime <= dashDuration)
            {
                movement.dashSpeed = DashSpeed;
            }
            else
            {
                OnEndDash();
                movement.dashSpeed = 0;
            }
        }
    }

    void OnStartDash()
    {
        isActivated = true;
        dashStartTime = Time.time;
        if (OnDash != null)
        {
            OnDash();
        }
    }
    public void OnEndDash()
    {
        isActivated = false;
        dashStartTime = 0;
        OnDashEnd?.Invoke();
    }
}