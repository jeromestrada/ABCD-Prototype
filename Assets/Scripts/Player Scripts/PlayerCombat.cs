using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    /*

    

    private void Start()
    {
        
    }

    private void OnDashEnd() // this can be refactored to allow dash attacks.
    {
        canStringAttack = true;
        ResetAttackString(); // reset the attack string to the beginning after a dash
    }

    // Update is called once per frame
    void Update()
    {
        if(equippedWeapon != null)
        {
            *//*if ((Time.time - finalStringTime) >= cooldownDuration)
            {
                isCoolingDown = false;
            }
            if ((Time.time - lastAttackStringTime) >= stringGracePeriod)
            {
                ResetAttackString();
            }*/
            
            /*if (Input.GetButton("Fire1") && !MouseItemData.IsPointerOverUIObjects() && !MouseItemData.inUI && canStringAttack && !isCoolingDown)
            {
                canStringAttack = false; // we wait for the animation to hit before we can attack again
                lastAttackStringTime = float.MaxValue;
                
                currentStringAttackPoint = equippedWeapon.AttackPoints[CurrentAttackString];
                OnAttack?.Invoke(CurrentAttackString % equippedWeapon.StringAttacksCount); // invoke the action
            }*//*
        }
    }


    *//*public void AttackFinish_AnimationEvent() // TODO: when this event fires, we play a transition phase where the next attack can be triggered
    {
        if(equippedWeapon != null)
        {
            lastAttackStringTime = Time.time;
            canStringAttack = true;
            CurrentAttackString++; // increment to the next attack string
            if (CurrentAttackString == equippedWeapon.StringAttacksCount) // if we've reached the last string we cooldown
            {
                isCoolingDown = true;
                finalStringTime = Time.time;
                ResetAttackString();
            }
        }
    }*/



    
}
