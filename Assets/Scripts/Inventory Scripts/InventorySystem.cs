using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventorySystem
{
    // This Inventory system can be used in multiple scenarios
    // i.e. the current hand of the player, their deck, and the shop system
    [SerializeField] private List<InventorySlot> inventorySlots = new List<InventorySlot>();

    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => inventorySlots.Count;
    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size)
    {
        inventorySlots = new List<InventorySlot>(size);

        for(int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(Card cardToAdd, int amountToAdd)
    {
        if(ContainsItem(cardToAdd, out List<InventorySlot> invSlots))
        {
            foreach(var slot in invSlots) // gets the first slot that can accomodate the amount to add
            {
                if (slot.RoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
        }
        if(HasFreeSlot(out InventorySlot freeSlot))
        {
            freeSlot.UpdateInventorySlot(cardToAdd, amountToAdd);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }
        return false;
    }

    public bool ContainsItem(Card cardToAdd, out List<InventorySlot> invSlots)
    {
        invSlots = InventorySlots.Where( i => i.Card == cardToAdd).ToList();
        return invSlots == null ? false : true;
    }
    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.Card == null);
        return freeSlot == null ? false : true;
    }
}
