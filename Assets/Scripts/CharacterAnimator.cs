using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class CharacterAnimator : MonoBehaviour
{
    Animator animator;
    PlayerController playerController;
    CharacterCombat combat;

    float motionSmoothness = 0.1f;

    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerController = GetComponent<PlayerController>();
        combat = GetComponent<CharacterCombat>();

        combat.OnAttack += OnAttack; // subscribe to the delegate
    }

    // Update is called once per frame
    void Update()
    {
        
        animator.SetFloat("speed", playerController.speed / playerController.maxSpeed, motionSmoothness, Time.deltaTime);
        if (playerController.isMoving)
        {
            animator.SetTrigger("attackCancel"); // whenever the player is moving trigger this so we can cancel attack animations
        }
        else
        {
            animator.ResetTrigger("attackCancel");
        }
    }

    protected virtual void OnAttack()
    {
        animator.SetTrigger("attackTrigger"); // trigger in the animator    
    }
}
