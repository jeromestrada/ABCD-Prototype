using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    private bool canStringAttack = true;
    private int currentAttackString = 0;
    public int stringAttacksCount = 2;
    private float lastAttackStringTime;
    public float stringGracePeriod = 0.85f;


    bool isCoolingDown = false;
    float finalStringStart;
    public float cooldownDuration = 0.8f;

    public event System.Action<int> OnAttack;
    PlayerController controller;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        // TODO: maybe use the animation time data or completion instead?
        // experiment with masking the legs to allow it to move but letting the upper body's animation to finish
        if (Time.time - lastAttackStringTime >= stringGracePeriod)
        {
            currentAttackString = 0; // reset the string to the first animation if the grace period lapsed.
        }
        if (Time.time - finalStringStart >= cooldownDuration)
        {
            isCoolingDown = false;
        }
        if (Input.GetButton("Fire1") && canStringAttack && !controller.isDashing && !isCoolingDown)
        {
            if(OnAttack != null)
            {
                if (currentAttackString == stringAttacksCount - 1)
                {
                    isCoolingDown = true;
                    finalStringStart = Time.time;
                }
                canStringAttack = false; // we wait for the animation to hit before we can attack again
                OnAttack(currentAttackString % stringAttacksCount);
                currentAttackString++;
                lastAttackStringTime = Time.time;
            }
        }
        
    }

    public void AttackHit_AnimationEvent()
    {
        Debug.Log("Animation Hit!");
        canStringAttack = true;
    }
}
