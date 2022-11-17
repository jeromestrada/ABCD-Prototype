using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class CharacterAnimator : MonoBehaviour
{
    Animator animator;
    public AnimationClip replaceableAttackAnim;
    public AnimationClip[] defaultAttackAnimSet;
    protected AnimationClip[] currentAttackAnimSet;
    protected AnimationClip[] currentAttackTransitionAnimSet;

    EquipmentManager weaponManager;
    Dictionary<Equipment, AnimationClip[]> weaponAnimationsDict , weaponTransitionAnimsDict;


    PlayerMovement playerMovement;
    public AnimatorOverrideController overrideController;

    [SerializeField] private DeckOfCards deck;

    float motionSmoothness = 0.1f;

    private void OnEnable()
    {
        DashAbility.OnDash += OnDash;
        AttackStringState.OnAttackAnimationPlayRequest += OnAttackString;
    }

    private void OnDisable()
    {
        DashAbility.OnDash -= OnDash;
        AttackStringState.OnAttackAnimationPlayRequest -= OnAttackString;
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if (overrideController == null)
        {
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        }
        animator.runtimeAnimatorController = overrideController;
        currentAttackAnimSet = defaultAttackAnimSet;

        playerMovement = GetComponent<PlayerMovement>();

        EquipmentManager.OnEquipmentChanged += OnWeaponChanged;

        if(deck == null) deck = GetComponentInChildren<DeckOfCards>();
        deck.CardSystem.OnInventorySlotChanged += AddWeaponAnimation;

        weaponAnimationsDict = new Dictionary<Equipment, AnimationClip[]>();
        weaponTransitionAnimsDict = new Dictionary<Equipment, AnimationClip[]>();
    }

    private void OnAttackString(int attackString)
    {
        // Debug.Log($"Triggered OnAttackString with the {attackString} string");
        animator.SetTrigger("attackTrigger");
        overrideController[replaceableAttackAnim] = currentAttackAnimSet[attackString];
    }

    void ChangeCurrentAttackAnimSet(Equipment newWeapon)
    {
        if (newWeapon == null)
        {
            currentAttackAnimSet = defaultAttackAnimSet;
            return;
        }
        else
        {
            if (weaponAnimationsDict.ContainsKey(newWeapon))
            {
                currentAttackAnimSet = weaponAnimationsDict[newWeapon];
                currentAttackTransitionAnimSet = weaponTransitionAnimsDict[newWeapon];
            }
        }
    }

    void LateUpdate()
    {
        
        animator.SetFloat("speed", playerMovement.speed / playerMovement.maxSpeed, motionSmoothness, Time.deltaTime);
        animator.SetBool("isAttacking", playerMovement.isAttacking);
    }

    public void AddWeaponAnimation(PlayerCardSlot slot)
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
    }

    // Attack animations
    protected virtual void OnAttack(int attackString)
    {
        animator.SetTrigger("attackTrigger");
        overrideController[replaceableAttackAnim] = currentAttackAnimSet[attackString];
    }

    protected virtual void OnWeaponChanged(Equipment oldWeapon, Equipment newWeapon)
    {
        ChangeCurrentAttackAnimSet(newWeapon);
    }

    // Dash animations
    protected virtual void OnDash()
    {
        animator.SetTrigger("dashTrigger");
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
