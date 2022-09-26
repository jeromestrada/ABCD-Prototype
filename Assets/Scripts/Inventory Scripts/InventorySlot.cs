using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private Card card;
    [SerializeField] private int stackSize;

    public Card Card => card;
    public int StackSize => stackSize;

    public InventorySlot(Card source, int amount)
    {
        card = source;
        stackSize = amount;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        card = null;
        stackSize = -1;
    }

    public void AssignItem(InventorySlot invSlot)
    {
        if (card == invSlot.card) AddToStack(invSlot.stackSize);
        else
        {
            card = invSlot.card;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }

    public void UpdateInventorySlot(Card cardToAdd, int amountToAdd)
    {
        card = cardToAdd;
        stackSize += amountToAdd;
    }

    public bool RoomLeftInStack(int amount, out int amountRemaining)
    {
        amountRemaining = card.MaxStackSize - stackSize;
        return RoomLeftInStack(amount);
    }

    public bool RoomLeftInStack(int amount)
    {
        if (stackSize + amount <= card.MaxStackSize) return true;
        else return false;
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }
}
