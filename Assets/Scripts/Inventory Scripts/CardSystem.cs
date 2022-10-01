using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class CardSystem
{
    // This Inventory system can be used in multiple scenarios
    // i.e. the current hand of the player, their deck, and the shop system
    [SerializeField] private List<CardSlot> cardSlots = new List<CardSlot>();

    public List<CardSlot> CardSlots => cardSlots;
    public int CardSystemSize => cardSlots.Count;
    public UnityAction<CardSlot> OnInventorySlotChanged;

    public CardSystem(int size)
    {
        cardSlots = new List<CardSlot>(size);

        for(int i = 0; i < size; i++)
        {
            cardSlots.Add(new CardSlot());
        }
    }

    public bool AddToInventory(Card cardToAdd, int numOfUses)
    {
        /*if(ContainsItem(cardToAdd, out List<InventorySlot> invSlots))
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
        }*/
        if(HasFreeSlot(out CardSlot freeSlot))
        {
            freeSlot.UpdateInventorySlot(cardToAdd, numOfUses);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }
        return false;
    }

    /*public bool ContainsItem(Card cardToAdd, out List<InventorySlot> invSlots)
    {
        invSlots = InventorySlots.Where( i => i.Card == cardToAdd).ToList();
        return invSlots == null ? false : true;
    }*/
    public bool HasFreeSlot(out CardSlot freeSlot)
    {
        freeSlot = CardSlots.FirstOrDefault(i => i.Card == null);
        return freeSlot == null ? false : true;
    }
}
