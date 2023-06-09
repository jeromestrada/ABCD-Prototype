using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public Animator animator;
    public AnimationClip replaceableAttackAnim;
    public AnimationClip replaceableTransAnim;
    public AnimationClip[] defaultAttackAnimSet;
    protected AnimationClip[] currentAttackAnimSet;
    protected AnimationClip[] currentAttackTransitionAnimSet;

    EquipmentManager weaponManager;


    public PlayerMovement playerMovement;
    public AnimatorOverrideController overrideController;

    [SerializeField] private DeckOfCards deck;

    float motionSmoothness = 0.1f;

    private bool isInteractingWithGate;

    private void Awake()
    {
        if (animator == null) animator = GetComponentInParent<Animator>();
        if(playerMovement == null) playerMovement = GetComponentInParent<PlayerMovement>();
        if (weaponManager == null) weaponManager = GetComponent<EquipmentManager>();
    }

    private void OnEnable()
    {
        DashAbility.OnDash += OnDash;
        AttackStringState.OnAttackAnimationPlayRequest += OnAttackString;
        MovementManager.OnGateInteraction += OnGateInteract;
    }

    private void OnDisable()
    {
        DashAbility.OnDash -= OnDash;
        AttackStringState.OnAttackAnimationPlayRequest -= OnAttackString;
        MovementManager.OnGateInteraction -= OnGateInteract;
    }

    void Start()
    {
        isInteractingWithGate = false;
        if (overrideController == null)
        {
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        }
        animator.runtimeAnimatorController = overrideController;
        currentAttackAnimSet = defaultAttackAnimSet;

        if(deck == null) deck = GetComponentInChildren<DeckOfCards>();
    }

    

    void LateUpdate()
    {
        if (!isInteractingWithGate)
        {
            animator.SetFloat("speed", playerMovement.speed / playerMovement.maxSpeed, motionSmoothness, Time.deltaTime);
            animator.SetBool("isAttacking", playerMovement.isAttacking);
        }
    }

    // Attack animations
    private void OnAttackString(int attackString)
    {
        //Debug.Log($"attacking with attackString: {attackString}");
        overrideController[replaceableAttackAnim.name] = weaponManager.equippedWeapon.Attacks[attackString].AttackAnimation;
        overrideController[replaceableTransAnim.name] = weaponManager.equippedWeapon.Attacks[attackString].RecoveryAnimation;
        animator.SetTrigger("attackTrigger");
    }

    // Dash animations
    protected virtual void OnDash()
    {
        animator.SetTrigger("dashTrigger");
    }

    // Gate Animations, changing stages or what not
    protected virtual void OnGateInteract(float duration)
    {
        isInteractingWithGate = true;
        animator.SetFloat("speed", playerMovement.maxSpeed, motionSmoothness, Time.deltaTime);
        Invoke(nameof(ResetSpeed), duration);
    }

    void ResetSpeed()
    {
        isInteractingWithGate = false;
        animator.SetFloat("speed", 0f);
    }

    /*
     Character animator will have to have logic for all existing abilities so that when they fire their rexpective actions
     the animator can listen to each and trigger the corresponding animation

    protected virtual void OnBlock()
    protected virtual void OnParry()
    .
    .
    .
    protected virtual void
    */
}

[System.Serializable]
public struct WeaponAnimations
{
    public Equipment weapon;
    public AnimationClip[] clips;
}
