using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    [SerializeField] Ability ability; // change to a list so holder can cast multiple different abilities
    float cooldownTime;
    private CharacterController controller;
    private PlayerMovement movement;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        var dash = ((DashAbility)ability);
        if (Input.GetKeyDown(dash.Key))
        {
            dash.Activate();
        }
        if (dash.isDashing)
        {
            if (Time.time - dash.dashStartTime <= dash.dashDuration)
            {
                movement.dashSpeed = dash.dashSpeed;
            }
            else
            {
                dash.OnEndDash();
                movement.dashSpeed = 0;
            }
        }
    }
}


