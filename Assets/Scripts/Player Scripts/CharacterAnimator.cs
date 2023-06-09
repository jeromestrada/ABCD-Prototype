using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(PlayerMovement))]
public class CharacterAnimator : MonoBehaviour
{
    public Animator animator;
    public AnimationClip replaceableAttackAnim;
    public AnimationClip replaceableTransAnim;
    public AnimationClip[] defaultAttackAnimSet;
    protected AnimationClip[] currentAttackAnimSet;
    protected AnimationClip[] currentAttackTransitionAnimSet;

    EquipmentManager weaponManager;
    //Dictionary<Equipment, AnimationClip[]> weaponAnimationsDict , weaponTransitionAnimsDict;


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
        //EquipmentManager.OnEquipmentChanged += OnWeaponChanged;
        //deck.CardSystem.OnInventorySlotChanged += AddWeaponAnimation;
        AttackStringState.OnAttackAnimationPlayRequest += OnAttackString;
        MovementManager.OnGateInteraction += OnGateInteract;
    }

    private void OnDisable()
    {
        DashAbility.OnDash -= OnDash;
        //EquipmentManager.OnEquipmentChanged -= OnWeaponChanged;
        //deck.CardSystem.OnInventorySlotChanged -= AddWeaponAnimation;
        AttackStringState.OnAttackAnimationPlayRequest -= OnAttackString;
        MovementManager.OnGateInteraction -= OnGateInteract;
    }

    void Start()
    {
        isInteractingWithGate = false;
        //animator = GetComponentInChildren<Animator>();
        if (overrideController == null)
        {
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        }
        animator.runtimeAnimatorController = overrideController;
        currentAttackAnimSet = defaultAttackAnimSet;

        // playerMovement = GetComponent<PlayerMovement>();

        if(deck == null) deck = GetComponentInChildren<DeckOfCards>();

        /*weaponAnimationsDict = new Dictionary<Equipment, AnimationClip[]>();
        weaponTransitionAnimsDict = new Dictionary<Equipment, AnimationClip[]>();*/
    }

    

    void LateUpdate()
    {
        if (!isInteractingWithGate)
        {
            animator.SetFloat("speed", playerMovement.speed / playerMovement.maxSpeed, motionSmoothness, Time.deltaTime);
            animator.SetBool("isAttacking", playerMovement.isAttacking);
        }
    }

    // will not use a Dictionary to store animations anymore
    /*public void AddWeaponAnimation(PlayerCardSlot slot)
    {
        if (slot.Card.CardType == CardType.ItemCard && ((ItemCard)slot.Card).item is Equipment)
        {
            var itemCard = (ItemCard)slot.Card;
            var weapon = (Equipment)itemCard.item;
            if (itemCard.item is Equipment && !weaponAnimationsDict.ContainsKey(weapon))
            {
                weaponAnimationsDict.Add(weapon, (weapon.WeaponAnimations.clips));
                weaponTransitionAnimsDict.Add(weapon, (weapon.WeaponTransitions.clips));
            }
        }
        else return;
    }*/

    // Attack animations
    private void OnAttackString(int attackString)
    {
        //Debug.Log($"attacking with attackString: {attackString}");
        overrideController[replaceableAttackAnim.name] = weaponManager.equippedWeapon.Attacks[attackString].AttackAnimation;
        overrideController[replaceableTransAnim.name] = weaponManager.equippedWeapon.Attacks[attackString].RecoveryAnimation;
        animator.SetTrigger("attackTrigger");
    }
    /*void ChangeCurrentAttackAnimSet(Equipment newWeapon) // each weapon will have an array of animations in them
    {
        if (newWeapon == null)
        {
            currentAttackAnimSet = defaultAttackAnimSet;
            return;
        }
        else
        {
            ;
            currentAttackTransitionAnimSet = weaponTransitionAnimsDict[newWeapon];
        }
    }*/

    /*protected virtual void OnWeaponChanged(Equipment oldWeapon, Equipment newWeapon)
    {
        ChangeCurrentAttackAnimSet(newWeapon);
    }*/

    // Dash animations
    protected virtual void OnDash()
    {
        animator.SetTrigger("dashTrigger");
    }

    // Gate Animations, changing stages or what not
    protected virtual void OnGateInteract(float duration)
    {
        //Debug.Log("Interacting with a gate for " + duration + " seconds...");
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
