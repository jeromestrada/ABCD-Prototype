using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeckInventory : InventoryHolder
{
    [SerializeField] List<Card> startingCards;

    private void Start()
    {
        // add the starting cards into the deck
        foreach (Card card in startingCards)
        {
            inventorySystem.AddToInventory(card, 1);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // open deck of cards
            OnDynamicInventoryDisplayRequested?.Invoke(inventorySystem);
        }
    }

}
