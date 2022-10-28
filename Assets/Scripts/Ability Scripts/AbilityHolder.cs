using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    [SerializeField] Ability[] ability; // change to a list so holder can cast multiple different abilities
    float cooldownTime;

    [SerializeField] private PlayerMovement movement;
    [SerializeField] private CharacterStats characterStats;

    private void Update()
    {
        foreach(var a in ability)
        {
            if (Input.GetKeyDown(a.Key))
            {
                a.Activate();
            }
            if (a.isActivated) a.UpdateAbility(movement, characterStats);
        }
        
    }
}


