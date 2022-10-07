using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardSlot : ISerializationCallbackReceiver
{
    private Card card;
    [SerializeField] private int _cardID = -1; // keeps track of what card is in this slot based on ID.
    [SerializeField] private int remainingUses;

    [SerializeField] private int slotNumber; // a number used to indicate the position of the slot in the card System
    // this number is arbitrary and is used to aid the shuffling of the card slots without changing their position in the system list.

    public int SlotNumber => slotNumber;
    public int RemainingUses => remainingUses;
    public Card Card => card;

    public CardSlot(Card source)
    {
        card = source;
        remainingUses = card.NumOfUses;
        _cardID = card.ID;
    }

    public CardSlot()
    {
        ClearSlot();
    }

    public void AssignSlotNumber(int numberToAssign)
    {
        slotNumber = numberToAssign;
    }

    public void ClearSlot()
    {
        card = null;
        _cardID = -1;
        remainingUses = -1;
    }

    public void AssignCard(CardSlot cardSlot)
    {
        card = cardSlot.card;
        _cardID = card.ID;
        remainingUses = cardSlot.RemainingUses;
    }

    public void InitCardSlot(Card cardToAdd)
    {
        card = cardToAdd;
        _cardID = card.ID;
        remainingUses = card.NumOfUses;
    }

    public void UseCardInSlot()
    {
        card.Use();
        remainingUses--;
        if (remainingUses == 0) ClearSlot();
    }

    public void OnBeforeSerialize()
    {
        
    }

    public void OnAfterDeserialize()
    {
        if (_cardID == -1) return; // the slot doesn't have any cards in it

        var db = Resources.Load<Database>("Card Database");
        card = db.GetCard(_cardID);
    }
}
