using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerCardSlot : CardSlot
{
    
    [SerializeField] private int remainingUses;

    [SerializeField] private int slotNumber; // a number used to indicate the position of the slot in the card System
    // this number is arbitrary and is used to aid the shuffling of the card slots without changing their position in the system list.

    public int SlotNumber => slotNumber;
    public int RemainingUses => remainingUses;
    
    public PlayerCardSlot(Card source)
    {
        card = source;
        _cardID = card.ID;
        remainingUses = card.NumOfUses;
    }
    public PlayerCardSlot()
    {
        ClearSlot();
    }

    public override void ClearSlot()
    {
        base.ClearSlot();
        remainingUses = -1;
    }

    public void AssignSlotNumber(int numberToAssign)
    {
        slotNumber = numberToAssign;
    }

    public override void AssignCard(CardSlot cardSlot)
    {
        card = cardSlot.Card;
        _cardID = card.ID;
        remainingUses = ((PlayerCardSlot)cardSlot).RemainingUses;
    }

    public override void InitCardSlot(Card cardToAdd)
    {
        base.InitCardSlot(cardToAdd);
        remainingUses = card.NumOfUses;
    }

    public void UseCardInSlot()
    {
        // Debug.Log($"Using card {card.name}");
        card.Use();
        remainingUses--;
        if (remainingUses == 0) ClearSlot();
    }

}
