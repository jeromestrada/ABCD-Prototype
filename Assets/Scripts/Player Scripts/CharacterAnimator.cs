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

    EquipmentManager weaponManager;
    Dictionary<Weapon, AnimationClip[]> weaponAnimationsDict;

    PlayerController playerController;
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

        playerController = GetComponent<PlayerController>();

        playerController.OnDash += OnDash;
        PlayerCombat.OnAttack += OnAttack;

        EquipmentManager.OnEquipmentChanged += OnWeaponChanged;

        if(deck == null) deck = GetComponentInChildren<DeckOfCards>();
        deck.CardSystem.OnInventorySlotChanged += AddWeaponAnimation;

        weaponAnimationsDict = new Dictionary<Weapon, AnimationClip[]>();
    }

    void ChangeCurrentAttackAnimSet(Weapon newWeapon)
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
        if (slot.Card.cardType == CardType.ItemCard && ((ItemCard)slot.Card).item is Weapon)
        {
            var itemCard = (ItemCard)slot.Card;
            var weapon = (Weapon)itemCard.item;
            if (itemCard.item is Weapon && !weaponAnimationsDict.ContainsKey(weapon))
                weaponAnimationsDict.Add(weapon, (weapon.weaponAnimations.clips));
        }
        else return;
    }

    protected virtual void OnAttack(int attackString)
    {
        animator.SetTrigger("attackTrigger");
        overrideController[replaceableAttackAnim] = currentAttackAnimSet[attackString];
        playerController.isAttacking = true;
    }

    protected virtual void OnWeaponChanged(Item oldWeapon, Item newWeapon)
    {
        if(newWeapon.ItemType == 0)
        ChangeCurrentAttackAnimSet((Weapon)newWeapon);
    }

    protected virtual void OnDash()
    {
        animator.SetTrigger("dashTrigger");
        
    }
}

[System.Serializable]
public struct WeaponAnimations
{
    public Weapon weapon;
    public AnimationClip[] clips;
}
