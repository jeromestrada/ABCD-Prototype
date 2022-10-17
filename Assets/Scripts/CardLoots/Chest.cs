using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    [SerializeField] private LootGenerator _lootGenerator; // generates loot options based on given pool
    [SerializeField] private DeckOfCards deck;
    [SerializeField] private List<Card> _cardLootGenerated;
    [SerializeField] private int _cardLootCount;
    private void Start()
    {
        _cardLootGenerated = new List<Card>();
    }
    public override void Interact(InteractableScanner scanner, out bool interactSuccessful)
    {
        deck = scanner.GetComponentInChildren<DeckOfCards>(); // get the deck from the scanner

        if (deck != null)
        {
            GenerateLootOptions(_cardLootCount);
            interactSuccessful = true;
        }
        else
        {
            interactSuccessful = false;
            Debug.LogError("Player inventory not found!");
        }
    }

    public void GenerateLootOptions(int optionsCount)
    {
        if (optionsCount <= 0) return;

        _cardLootGenerated.Clear();
        for (int i = 0; i < optionsCount; i++)
        {
            var card = _lootGenerator.GenerateLoot();
            while (_cardLootGenerated.Contains(card)) card = _lootGenerator.GenerateLoot(); // prevents duplicates
            _cardLootGenerated.Add(card);
        }
    }
}
