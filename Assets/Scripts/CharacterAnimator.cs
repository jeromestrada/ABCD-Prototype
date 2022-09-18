using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class CharacterAnimator : MonoBehaviour
{
    Animator animator;
    public AnimationClip replaceableAttackAnim;
    public AnimationClip[] defaultAttackAnimSet;
    protected AnimationClip[] currentAttackAnimSet;

    PlayerController playerController;
    public AnimatorOverrideController overrideController;
    CharacterCombat combat;

    float motionSmoothness = 0.1f;

    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if (overrideController == null)
        {
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        }
        animator.runtimeAnimatorController = overrideController;
        currentAttackAnimSet = defaultAttackAnimSet;

        playerController = GetComponent<PlayerController>();
        combat = GetComponent<CharacterCombat>();

        playerController.OnDash += OnDash;
        combat.OnAttack += OnAttack; // subscribe to the delegate
    }

    // Update is called once per frame
    void Update()
    {
        
        animator.SetFloat("speed", playerController.speed / playerController.maxSpeed, motionSmoothness, Time.deltaTime);
        if (playerController.isDashing)
        {
            animator.SetTrigger("attackCancel"); // whenever the player is moving trigger this so we can cancel attack animations
            combat.canStringAttack = true;
            playerController.isAttacking = false;
        }
        else
        {
            animator.ResetTrigger("attackCancel");
        }
    }

    protected virtual void OnAttack(int attackString)
    {
        animator.SetTrigger("attackTrigger"); // trigger in the animator
        overrideController[replaceableAttackAnim] = currentAttackAnimSet[attackString];
    }

    protected virtual void OnDash()
    {
        animator.SetTrigger("dashTrigger"); // trigger in the animator
        
    }
}
