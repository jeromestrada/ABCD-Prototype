using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Dash", menuName ="Abilities/Dash")]
public class DashAbility : Ability
{
    [SerializeField] private float _dashVelocity;

    public bool isDashing;
    public float dashSpeed;
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

    public void UpdateAbility(PlayerMovement movement)
    {
        if (isActivated)
        {
            if (Time.time - dashStartTime <= dashDuration)
            {
                movement.dashSpeed = dashSpeed;
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
