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

    WeaponManager weaponManager;
    Dictionary<Weapon, AnimationClip[]> weaponAnimationsDict;

    PlayerController playerController;
    public AnimatorOverrideController overrideController;
    CharacterCombat combat;

    [SerializeField] DeckInventory deck;

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

        deck = GetComponentInChildren<DeckInventory>();
        deck.CardSystem.OnInventorySlotChanged += AddWeaponAnimation;

        playerController.OnDash += OnDash;
        combat.OnAttack += OnAttack; // subscribe to the delegate

        weaponManager = GetComponent<WeaponManager>();
        weaponManager.onWeaponChanged += OnWeaponChanged;

        // handle the animations
        // TODO: call the same logic whenever the player adds/removes a weapon card in the deck.
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

        // Update is called once per frame
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

    public void AddWeaponAnimation(CardSlot slot)
    {
        if (slot.Card.cardType == CardType.ItemCard)
        {
            var itemCard = (ItemCard)slot.Card;
            if (itemCard.item is Weapon) weaponAnimationsDict.Add((Weapon)itemCard.item, ((Weapon)itemCard.item).weaponAnimations.clips);
            Debug.Log("Added animation to dictionary!");
        }
        else return;
    }

    protected virtual void OnAttack(int attackString)
    {
        animator.SetTrigger("attackTrigger"); // trigger in the animator
        overrideController[replaceableAttackAnim] = currentAttackAnimSet[attackString];
        playerController.isAttacking = true;
    }

    protected virtual void OnWeaponChanged(Weapon weapon)
    {
        ChangeCurrentAttackAnimSet(weapon);
    }

    protected virtual void OnDash()
    {
        animator.SetTrigger("dashTrigger"); // trigger in the animator
        
    }
}

[System.Serializable]
public struct WeaponAnimations
{
    public Weapon weapon;
    public AnimationClip[] clips;
}
