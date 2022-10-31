using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class CardSystem
{
    [SerializeField] private List<PlayerCardSlot> cardSlots = new List<PlayerCardSlot>();

    public List<PlayerCardSlot> CardSlots => cardSlots;
    public int CardSystemSize => cardSlots.Count;
    public UnityAction<PlayerCardSlot> OnInventorySlotChanged;

    public CardSystem(int size)
    {
        cardSlots = new List<PlayerCardSlot>(size);

        for(int i = 0; i < size; i++)
        {
            cardSlots.Add(new PlayerCardSlot());
        }
    }

    public void RemoveCardSlot(PlayerCardSlot cardSlotToRemove)
    {
        cardSlots.Remove(cardSlotToRemove);
    }

    public bool AddToCardSystem(Card cardToAdd)
    {
        if(HasFreeSlot(out PlayerCardSlot freeSlot))
        {
            freeSlot.InitCardSlot(cardToAdd);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        } 
        else // if there is no available slot we add a new one to put the card in.
        {
            freeSlot = new PlayerCardSlot(cardToAdd);
            cardSlots.Add(freeSlot); // add the new slot into the system.
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }
    }
    public bool HasFreeSlot(out PlayerCardSlot freeSlot)
    {
        freeSlot = CardSlots.FirstOrDefault(i => i.Card == null);
        return freeSlot == null ? false : true;
    }
}
