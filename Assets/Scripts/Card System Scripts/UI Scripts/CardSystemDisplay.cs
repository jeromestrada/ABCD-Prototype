using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CardSystemDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;
    protected CardSystem cardSystem;
    protected Dictionary<CardSlot_UI, PlayerCardSlot> slotDictionary;
    [SerializeField] protected CardSystemDisplayType cardSystemDisplayType;

    private KeyCode[] hotKeys = new KeyCode[] // default hot keys for the hand are the numbers on top of the keyboard
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9,
        KeyCode.Alpha0
    };

    private int selectedHotKeyIndex;
    private int previousHotKeyPressed;
    public CardSystem CardSystem => cardSystem;
    public Dictionary<CardSlot_UI, PlayerCardSlot> SlotDictionary => slotDictionary;
    public CardSystemDisplayType CardSystemDisplayType => cardSystemDisplayType;

    protected virtual void Start()
    {
        previousHotKeyPressed = -1;
    }

    private void Update()
    {
        HandleHotKeys();
    }

    public abstract void AssignSlots(CardSystem invToDisplay);

    public abstract void ClearSlots();

    public void AssignInventoryType(CardSystemDisplayType type)
    {
        cardSystemDisplayType = type;
    }

    protected virtual void UpdateSlot(PlayerCardSlot updatedSlot)
    {
        foreach(var slot in SlotDictionary)
        {
            if(slot.Value == updatedSlot)
            {
                slot.Key.UpdateUISlot(updatedSlot);
            }
        }
    }

    public void SlotClicked(Slot_UI clickedUISlot)
    {
        // if it is the Player's hand, then we treat UI clicks as Use,
        if(this.CardSystemDisplayType == CardSystemDisplayType.HandInventory)
        {
            CardSlot_UI clickedUISlotCasted = (CardSlot_UI) clickedUISlot;
            // if the card slot in the hand has a card and the mouse doesn't
            if (clickedUISlotCasted.AssignedInventorySlot.Card != null && mouseInventoryItem.AssignedCardSlot.Card == null)
            {
                mouseInventoryItem.UpdateMouseSlot(clickedUISlotCasted.AssignedInventorySlot);
                mouseInventoryItem.SavePickedFrom(clickedUISlotCasted); // save the UI slot in case the player wants to return the card by right clicking
                clickedUISlotCasted.ClearSlot();
                return;
            }
            // if the mouse has a card and the slot doesn't
            else if(clickedUISlotCasted.AssignedInventorySlot.Card == null && mouseInventoryItem.AssignedCardSlot.Card != null)
            {
                clickedUISlotCasted.AssignedInventorySlot.AssignCard(mouseInventoryItem.AssignedCardSlot);
                clickedUISlotCasted.UpdateUISlot();
                mouseInventoryItem.ClearSlot();
                return;
            }
            // Both have items 
            else if (clickedUISlotCasted.AssignedInventorySlot.Card != null && mouseInventoryItem.AssignedCardSlot.Card != null)
            {
                SwapSlots(clickedUISlotCasted);
            }
        }

        // if it is the Player's deck, we treat it as card Inspection, we only display the card's details
        else if (this.CardSystemDisplayType == CardSystemDisplayType.DeckInventory)
        {
            CardSlot_UI clickedUISlotCasted = (CardSlot_UI)clickedUISlot;
            Debug.Log($"Inspecting {clickedUISlotCasted.AssignedInventorySlot.Card.name}!");
        }
        // > we can add similar other types of Player Deck display (parent display)
        /*
            for instance, an edit tab, where we can add or remove cards, like a rest site/service stop/event that allows us to do so
         */
        else if (this.CardSystemDisplayType == CardSystemDisplayType.ShopInventory)
        {
            var clickedUISlotCasted = (ShopSlot_UI)clickedUISlot;
            Debug.Log($"Trying to buy {clickedUISlotCasted.AssignedShopSlot.Card.name}!");
            // Open up confirmation window and wait for Buy button to be clicked
            ((ShopKeeperDisplay)this).DisplayBuyConfirmWindow(clickedUISlotCasted);
        }

        else if (this.CardSystemDisplayType == CardSystemDisplayType.LootInventory)
        {
            var clickedUISlotCasted = (LootSlot_UI)clickedUISlot;
            Debug.Log($"Trying to loot {clickedUISlotCasted.AssignedLootSlot.Card.name}!");
            // Open up confirmation window and wait for Loot button to be clicked
            ((LootSystemDisplay)this).DisplayLootConfirmWindow(clickedUISlotCasted);
        }
    }
    public void SwapSlots(CardSlot_UI clickedUISlot)
    {
        var clonedSlot = new PlayerCardSlot(mouseInventoryItem.AssignedCardSlot.Card);
        mouseInventoryItem.ClearSlot();
        mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);

        clickedUISlot.AssignedInventorySlot.AssignCard(clonedSlot);
        clickedUISlot.UpdateUISlot();
    }

    public void HandleHotKeys()
    {
        if (CardSystemDisplayType == CardSystemDisplayType.HandInventory)
        {
            for (int i = 0; i < hotKeys.Length; i++)
            {
                if (Input.GetKeyDown(hotKeys[i]))
                {

                    Debug.Log($"Selecting {hotKeys[i]} in hand");
                    selectedHotKeyIndex = i;
                    if (previousHotKeyPressed == selectedHotKeyIndex) // if a hotkey is pressed twice in a row, use the item.
                    {
                        Debug.Log($"Confirmed use of {hotKeys[i]} in hand");
                    }
                    previousHotKeyPressed = selectedHotKeyIndex;
                }
            }
        }
    }
}

public enum CardSystemDisplayType { HandInventory, DeckInventory, ShopInventory, LootInventory }
