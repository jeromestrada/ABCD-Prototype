using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;
    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;
    protected InventoryDisplayType inventoryDisplayType;

    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;
    public InventoryDisplayType InventoryDisplayType => inventoryDisplayType;

    protected virtual void Start()
    {

    }

    public abstract void AssignSlots(InventorySystem invToDisplay);

    public void AssignInventoryType(InventoryDisplayType type)
    {
        inventoryDisplayType = type;
    }

    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach(var slot in SlotDictionary)
        {
            if(slot.Value == updatedSlot)
            {
                slot.Key.UpdateUISlot(updatedSlot);
            }
        }
    }

    public void SlotClicked(InventorySlot_UI clickedUISlot)
    {
        // if it is the Player's hand, then we treat UI clicks as Use,
        if(this.InventoryDisplayType == InventoryDisplayType.HandInventory)
        {
            // if the card slot in the hand has a card and the mouse doesn't
            if(clickedUISlot.AssignedInventorySlot.Card != null && mouseInventoryItem.AssignedInventorySlot.Card == null)
            {
                mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
                mouseInventoryItem.SavePickedFrom(clickedUISlot); // save the UI slot in case the player wants to return the card by right clicking
                clickedUISlot.ClearSlot();
                return;
            }
            if(clickedUISlot.AssignedInventorySlot.Card == null && mouseInventoryItem.AssignedInventorySlot.Card != null)
            {
                clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
                clickedUISlot.UpdateUISlot();
                mouseInventoryItem.ClearSlot();
            }
        }

        // if it is the Player's deck, we treat it as card Inspection, we only display the card's details
        else if (this.InventoryDisplayType == InventoryDisplayType.DeckInventory)
        {
            Debug.Log("Inspecting Card!");
        }

        // > we can add similar other types of Player Deck display (parent display)
        /*
            for instance, an edit tab, where we can add or remove cards, like a rest site/service stop/event that allows us to do so
            
         */
        else if (this.InventoryDisplayType == InventoryDisplayType.ShopInventory)
        {
            Debug.Log("Trying to buy Card!");
        }
    }
}

public enum InventoryDisplayType { HandInventory, DeckInventory, ShopInventory }
