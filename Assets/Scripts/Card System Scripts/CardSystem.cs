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

    public bool AddToCardSystem(Card cardToAdd)
    {
        if(HasFreeSlot(out CardSlot freeSlot))
        {
            freeSlot.InitCardSlot(cardToAdd);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        } 
        else // if there is no available slot we add a new one to put the card in.
        {
            freeSlot = new CardSlot(cardToAdd);
            cardSlots.Add(freeSlot); // add the new slot into the system.
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }
    }
    public bool HasFreeSlot(out CardSlot freeSlot)
    {
        freeSlot = CardSlots.FirstOrDefault(i => i.Card == null);
        return freeSlot == null ? false : true;
    }
}
