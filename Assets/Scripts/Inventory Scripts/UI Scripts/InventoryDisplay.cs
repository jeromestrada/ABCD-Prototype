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

    public abstract void AssignSlot(InventorySystem invToDisplay);

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

    public void SlotClicked(InventorySlot_UI clickedSlot)
    {
        Debug.Log("Slot clicked!");

        // if it is the Player's hand, then we treat UI clicks as Use,
        if(this.InventoryDisplayType == InventoryDisplayType.HandInventory)
        {
            Debug.Log("Using Card!");
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
