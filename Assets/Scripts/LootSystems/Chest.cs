using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Chest : Interactable
{
    [SerializeField] private LootGenerator _lootGenerator; // generates loot options based on given pool
    [SerializeField] private DeckOfCards deck;
    [SerializeField] private LootSystem _lootSystem;
    [SerializeField] private List<Card> _cardLootGenerated;
    [SerializeField] private int _cardLootCount;

    public static UnityAction<LootSystem, DeckOfCards> OnLootWindowRequested;

    private void Awake()
    {
        _cardLootGenerated = new List<Card>();
        _lootSystem = new LootSystem(_cardLootCount);
        GenerateLootOptions(_cardLootCount);
    }

    public override void Interact(InteractableScanner scanner, out bool interactSuccessful)
    {
        deck = scanner.GetComponentInChildren<DeckOfCards>(); // get the deck from the scanner

        if (deck != null)
        {
            OnLootWindowRequested?.Invoke(_lootSystem, deck);
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
            while (_cardLootGenerated.Contains(card)) card = _lootGenerator.GenerateLoot(); // prevents duplicates, might change it to allow atleast 1 duplicate
            _cardLootGenerated.Add(card);
        }
        AddGeneratedLootToSystem();
    }

    public void AddGeneratedLootToSystem()
    {
        foreach(var lootCard in _cardLootGenerated)
        {
            _lootSystem.AddToLoot(lootCard);
        }
    }

    [ContextMenu("Generate New Loot")]
    public void GenerateNewLootOptions()
    {
        GenerateLootOptions(_cardLootCount);
    }
}
