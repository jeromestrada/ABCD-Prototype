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

    EquipmentManager weaponManager;
    Dictionary<Equipment, AnimationClip[]> weaponAnimationsDict;

    PlayerMovement playerController;
    public AnimatorOverrideController overrideController;
    PlayerCombat combat;

    [SerializeField] private DeckOfCards deck;

    float motionSmoothness = 0.1f;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if (overrideController == null)
        {
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        }
        animator.runtimeAnimatorController = overrideController;
        currentAttackAnimSet = defaultAttackAnimSet;

        playerController = GetComponent<PlayerMovement>();

        playerController.OnDash += OnDash;
        PlayerCombat.OnAttack += OnAttack;

        EquipmentManager.OnEquipmentChanged += OnWeaponChanged;

        if(deck == null) deck = GetComponentInChildren<DeckOfCards>();
        deck.CardSystem.OnInventorySlotChanged += AddWeaponAnimation;

        weaponAnimationsDict = new Dictionary<Equipment, AnimationClip[]>();
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
            if(weaponAnimationsDict.ContainsKey(newWeapon)) currentAttackAnimSet = weaponAnimationsDict[newWeapon];
        }
    }

    void LateUpdate()
    {
        
        animator.SetFloat("speed", playerController.speed / playerController.maxSpeed, motionSmoothness, Time.deltaTime);
        if (playerController.isDashing)
        {
            animator.SetTrigger("attackCancel"); // whenever the player is moving trigger this so we can cancel attack animations
            //combat.canStringAttack = true;
            playerController.isAttacking = false;
        }
        else
        {
            animator.ResetTrigger("dashTrigger");
            animator.ResetTrigger("attackCancel");
        }
        animator.SetBool("isAttacking", playerController.isAttacking);
        animator.SetBool("canMove", playerController.isMoving);
    }

    public void AddWeaponAnimation(PlayerCardSlot slot)
    {
        if (slot.Card.CardType == CardType.ItemCard && ((ItemCard)slot.Card).item is Equipment)
        {
            var itemCard = (ItemCard)slot.Card;
            var weapon = (Equipment)itemCard.item;
            if (itemCard.item is Equipment && !weaponAnimationsDict.ContainsKey(weapon))
                weaponAnimationsDict.Add(weapon, (weapon.WeaponAnimations.clips));
        }
        else return;
    }

    protected virtual void OnAttack(int attackString)
    {
        animator.SetTrigger("attackTrigger");
        overrideController[replaceableAttackAnim] = currentAttackAnimSet[attackString];
        playerController.isAttacking = true;
    }

    protected virtual void OnWeaponChanged(Equipment oldWeapon, Equipment newWeapon)
    {
        ChangeCurrentAttackAnimSet(newWeapon);
    }

    protected virtual void OnDash()
    {
        animator.SetTrigger("dashTrigger");
        
    }
}

[System.Serializable]
public struct WeaponAnimations
{
    public Equipment weapon;
    public AnimationClip[] clips;
}
