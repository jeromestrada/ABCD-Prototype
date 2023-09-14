using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerCardSlot : CardSlot
{
    
    [SerializeField] private int remainingUses;

    [SerializeField] private int slotNumber; // a number used to indicate the position of the slot in the card System
    // this number is arbitrary and is used to aid the shuffling of the card slots without changing their position in the system list.
    public int manaCost;
    public static event System.Action<int,Card> OnCardUse;

    public int SlotNumber => slotNumber;
    
    public PlayerCardSlot(Card source)
    {
        card = source;
        _cardID = card.ID;
        manaCost = source.manaCost;
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
        manaCost = card.manaCost;
    }

    public override void InitCardSlot(Card cardToAdd)
    {
        base.InitCardSlot(cardToAdd);
    }

    public void UseCardInSlot()
    {
        OnCardUse?.Invoke(manaCost, card);
    }

}
