using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    public bool canStringAttack;
    private int currentAttackString; 
    public int stringAttacksCount = 2;
    private float lastAttackStringTime;
    public float stringGracePeriod = 0.5f;


    bool isCoolingDown = false;
    float finalStringStart;
    public float cooldownDuration = 0.8f;

    public event System.Action<int> OnAttack;
    PlayerController controller;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
        lastAttackStringTime = Time.time;
        canStringAttack = true;
        currentAttackString = 0;
    }
    // Update is called once per frame
    void Update()
    {
        // TODO: will use full animation instead, and have a separate recovery animation for each attack
        // this will eliminate the need to use events to sync different attack string animations.
        
        if (Time.time - finalStringStart >= cooldownDuration)
        {
            isCoolingDown = false;
        }
        if (Time.time - lastAttackStringTime > stringGracePeriod)
        {
            currentAttackString = 0; // reset the string to the first animation if the grace period lapsed.
        }
        if (Input.GetButton("Fire1") && !isCoolingDown && canStringAttack)
        {
            canStringAttack = false; // we wait for the animation to hit before we can attack again
            if (controller.isDashing)
            {
                controller.OnEndDash();
            }
            if (OnAttack != null)
            {
                OnAttack(currentAttackString % stringAttacksCount); // invoke the delegate
                lastAttackStringTime = Time.time;
                currentAttackString++; // increment to the next attack string
                if (currentAttackString == stringAttacksCount) // if we've reached the last string we cooldown
                {
                    isCoolingDown = true;
                    finalStringStart = Time.time;
                } 
            }
        }
    }

    public void AttackFinish_AnimationEvent()
    {
        /*Debug.Log("Animation Hit! " + (currentAttackString - 1));*/
        canStringAttack = true; 
    }
}
