using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CardSystemDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;
    protected CardSystem cardSystem;
    protected Dictionary<CardSlot_UI, PlayerCardSlot> slotDictionary;
    [SerializeField] protected CardSystemDisplayType cardSystemDisplayType;

    public CardSystem CardSystem => cardSystem;
    public Dictionary<CardSlot_UI, PlayerCardSlot> SlotDictionary => slotDictionary;
    public CardSystemDisplayType CardSystemDisplayType => cardSystemDisplayType;

    protected virtual void Start()
    {

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
            Debug.Log("Inspecting Card!");
        }

        // > we can add similar other types of Player Deck display (parent display)
        /*
            for instance, an edit tab, where we can add or remove cards, like a rest site/service stop/event that allows us to do so
            
         */
        else if (this.CardSystemDisplayType == CardSystemDisplayType.ShopInventory)
        {
            var clickedUISlotCasted = (ShopSlot_UI)clickedUISlot;
            Debug.Log($"Trying to buy {clickedUISlotCasted.AssignedShopSlot.Card.name}!");
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
}

public enum CardSystemDisplayType { HandInventory, DeckInventory, ShopInventory }
