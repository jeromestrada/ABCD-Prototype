using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[RequireComponent(typeof(UniqueID))]
public class DeckOfCards : CardSystemHolder
{
    [SerializeField] List<Card> startingCards;
    [SerializeField] private List<int> slotNumbersList;
    public static UnityAction<CardSystem> OnDeckOfCardsDisplayRequested;

    protected override void Awake()
    {
        base.Awake();
        SaveLoad.OnLoadGame += LoadDeck;
    }

    private void LoadDeck(SaveData data)
    {
        if(data.deckDictionary.TryGetValue(GetComponent<UniqueID>().ID, out CardSystemHolderSaveData deckData))
        {   // check the save data for this deck, if it exists load it in
            _cardSystem = deckData.cardSystem;
        }
    }

    public void ShuffleDeck() // shuffle the deck by assigning a randomly genereated number to each slot and
    {
        foreach(CardSlot slot in CardSystem.CardSlots)
        {
            int rand = Random.Range(0, CardSystem.CardSlots.Count);
            slot.AssignSlotNumber(rand);
            int index = slotNumbersList.BinarySearch(rand);
            if (index < 0) index = ~index;
            slotNumbersList.Insert(index, rand);
        }
    }

    public Card GetTopCard()
    {
        if(CardSystem.CardSystemSize == 0) return null;

        CardSlot cardSlot = CardSystem.CardSlots.Find(s => s.SlotNumber == slotNumbersList[0]);
        var index = CardSystem.CardSlots.FindIndex(s => s.SlotNumber == slotNumbersList[0]);
        Card card = cardSlot.Card;
        slotNumbersList.RemoveAt(0);
        CardSystem.CardSlots.RemoveAt(index);
        return card;
    }

    private void Start()
    {
        // add the starting cards into the deck
        foreach (Card card in startingCards)
        {
            _cardSystem.AddToCardSystem(card);
        }

        slotNumbersList = new List<int>();
        ShuffleDeck();

        var deckSaveData = new CardSystemHolderSaveData(_cardSystem);
        SaveGameManager.data.deckDictionary.Add(GetComponent<UniqueID>().ID, deckSaveData);
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.C))
        {   // open deck of cards
            OnDeckOfCardsDisplayRequested?.Invoke(_cardSystem);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {   // open deck of cards
            ShuffleDeck();
        }
    }
}


