using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UniqueID))]
public class DiscardedCards : CardSystemHolder
{
    public static UnityAction<CardSystem> OnDiscardPileDisplayRequested;
    private void Start()
    {
        var discardPileSaveData = new CardSystemHolderSaveData(_cardSystem);
        SaveGameManager.data.discardPileDictionary.Add(GetComponent<UniqueID>().ID, discardPileSaveData);
    }
    public bool Discard(Card cardToDiscard) // add a card to the discard pile, the hand can call this function to discard a card
    {
        var addSuccess = CardSystem.AddToCardSystem(cardToDiscard);
        return addSuccess;
    }

    public Card GetCard()
    {
        // TODO: allow player to get a specific card from the dicards pile
        return null;
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Q))
        {   // open/close discard pile of cards
            OnDiscardPileDisplayRequested?.Invoke(_cardSystem);
        }
    }
}
