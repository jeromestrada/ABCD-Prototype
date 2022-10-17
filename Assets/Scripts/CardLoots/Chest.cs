using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    [SerializeField] private LootGenerator _lootGenerator; // generates loot options based on given pool
    [SerializeField] private DeckOfCards deck;

    public override void Interact(InteractableScanner scanner, out bool interactSuccessful)
    {
        Debug.Log("Interacting with a loot chest");
        // display the shop when interacted with
        deck = scanner.GetComponentInChildren<DeckOfCards>(); // get the deck from the scanner

        if (deck != null)
        {
            // generate loot options here and display it to the player interacting with this chest
            var card = _lootGenerator.GenerateLoot();
            Debug.Log($"{card.name} was generated from the pool");
            interactSuccessful = true;
        }
        else
        {
            interactSuccessful = false;
            Debug.LogError("Player inventory not found!");
        }
    }
}
