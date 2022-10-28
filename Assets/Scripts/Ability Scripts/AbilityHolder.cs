using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    [SerializeField] Ability ability; // change to a list so holder can cast multiple different abilities
    float cooldownTime;

    [SerializeField] private PlayerMovement movement;

    private void Update()
    {
        var dash = ((DashAbility)ability);
        if (Input.GetKeyDown(dash.Key))
        {
            dash.Activate();
        }
        if(dash.isActivated) dash.UpdateAbility(movement);
    }
}


