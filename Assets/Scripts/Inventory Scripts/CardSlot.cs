using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardSlot
{
    [SerializeField] private Card card;
    [SerializeField] private int numOfUses;

    public Card Card => card;
    public int NumOfUses => numOfUses;

    public CardSlot(Card source, int amount)
    {
        card = source;
        numOfUses = amount;
    }

    public CardSlot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        card = null;
        numOfUses = -1;
    }

    public void AssignItem(CardSlot invSlot)
    {
        if (card == invSlot.card) AddToStack(invSlot.numOfUses);
        else
        {
            card = invSlot.card;
            numOfUses = 0;
            AddToStack(invSlot.numOfUses);
        }
    }

    public void UpdateInventorySlot(Card cardToAdd, int amountToAdd)
    {
        card = cardToAdd;
        if (numOfUses == -1) numOfUses = 1;
        else numOfUses += amountToAdd;
    }

  /*  public bool RoomLeftInStack(int amount, out int amountRemaining)
    {
        amountRemaining = card.MaxStackSize - stackSize;
        return RoomLeftInStack(amount);
    }*/
/*
    public bool RoomLeftInStack(int amount)
    {
        if (stackSize + amount <= card.MaxStackSize) return true;
        else return false;
    }*/

    public void AddToStack(int amount)
    {
        numOfUses += amount;
    }

    public void RemoveFromStack(int amount)
    {
        numOfUses -= amount;
    }
}
