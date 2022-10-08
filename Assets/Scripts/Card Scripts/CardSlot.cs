using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardSlot : ISerializationCallbackReceiver
{
    protected Card card;
    [SerializeField] protected int _cardID = -1; // keeps track of what card is in this slot based on ID.
    [SerializeField] private int _stackSize;

    public Card Card => card;
    public int StackSize => _stackSize;

    public virtual void ClearSlot()
    {
        card = null;
        _cardID = -1;
        _stackSize = -1;
    }

    public virtual void AssignCard(CardSlot cardSlot)
    {
        if (card == cardSlot.Card) AddToStack(cardSlot.StackSize);
        else
        {
            card = cardSlot.card;
            _cardID = card.ID;
            _stackSize = 0;
            AddToStack(cardSlot.StackSize);
        }
    }

    public virtual void AssignCard(Card cardToAdd, int amount)
    {
        if (card == cardToAdd) AddToStack(amount);
        else
        {
            card = cardToAdd;
            _cardID = card.ID;
            _stackSize = 0;
            AddToStack(amount);
        }
    }

    public void AddToStack(int amount)
    {
        _stackSize += amount;
    }

    public virtual void InitCardSlot(Card cardToAdd)
    {
        card = cardToAdd;
        _cardID = card.ID;
        _stackSize = 1;
    }


    public void OnAfterDeserialize()
    {
        if (_cardID == -1) return; // the slot doesn't have any cards in it

        var db = Resources.Load<Database>("Card Database");
        card = db.GetCard(_cardID);
    }

    public void OnBeforeSerialize()
    {

    }
}
