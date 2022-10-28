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
        if (Input.GetKeyDown(ability.Key))
        {
            ability.Activate();
        }
        if(ability.isActivated) ability.UpdateAbility(movement);
    }
}


