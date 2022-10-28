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
        // cast time can be added so that abilities can be cancelled skillfully when needed.
        // TODO: add the cast time mechanic in the abilities themselves, Coroutines seems to be a good approach
        foreach(var a in ability)
        {// cycle through all the abilites in the list and listen for their key down
            if (Input.GetKeyDown(a.Key))
            {
                a.Activate();
            }
            if (a.isActivated) a.UpdateAbility(movement, characterStats);
        }
        
    }
}


