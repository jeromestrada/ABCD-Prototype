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

    public void SlotClicked(CardSlot_UI clickedUISlot)
    {
        // if it is the Player's hand, then we treat UI clicks as Use,
        if(this.CardSystemDisplayType == CardSystemDisplayType.HandInventory)
        {
            // if the card slot in the hand has a card and the mouse doesn't
            if(clickedUISlot.AssignedInventorySlot.Card != null && mouseInventoryItem.AssignedCardSlot.Card == null)
            {
                mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
                mouseInventoryItem.SavePickedFrom(clickedUISlot); // save the UI slot in case the player wants to return the card by right clicking
                clickedUISlot.ClearSlot();
                return;
            }
            // if the mouse has a card and the slot doesn't
            else if(clickedUISlot.AssignedInventorySlot.Card == null && mouseInventoryItem.AssignedCardSlot.Card != null)
            {
                clickedUISlot.AssignedInventorySlot.AssignCard(mouseInventoryItem.AssignedCardSlot);
                clickedUISlot.UpdateUISlot();
                mouseInventoryItem.ClearSlot();
                return;
            }
            // Both have items 
            else if (clickedUISlot.AssignedInventorySlot.Card != null && mouseInventoryItem.AssignedCardSlot.Card != null)
            {
                SwapSlots(clickedUISlot);
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
            Debug.Log("Trying to buy Card!");
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
