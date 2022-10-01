using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardSlot
{
    [SerializeField] private Card card;
    [SerializeField] private int remainingUses;

    public int RemainingUses => remainingUses;

    public Card Card => card;

    public CardSlot(Card source)
    {
        card = source;
    }

    public CardSlot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        card = null;
        remainingUses = -1;
    }

    public void AssignCard(CardSlot cardSlot)
    {
        card = cardSlot.card;
        remainingUses = cardSlot.RemainingUses;
    }

    public void InitCardSlot(Card cardToAdd)
    {
        card = cardToAdd;
        remainingUses = card.NumOfUses;
    }

    public void UseCardInSlot()
    {
        card.Use();
        remainingUses--;
        if (remainingUses == 0) ClearSlot();
    }
}
