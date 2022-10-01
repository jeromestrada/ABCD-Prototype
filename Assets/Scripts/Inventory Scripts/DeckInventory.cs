using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeckInventory : CardSystemHolder
{
    [SerializeField] List<Card> startingCards;

    private void Start()
    {
        // add the starting cards into the deck
        foreach (Card card in startingCards)
        {
            cardSystem.AddToInventory(card, card.NumOfUses);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // open deck of cards
            OnDynamicCardSystemDisplayRequested?.Invoke(cardSystem);
        }
    }

}
