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
    public static UnityAction<bool> OnDeckOfCardsDisplayHideRequested;
    private bool isHidden;

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

    public bool AddCardToDeck(Card cardToAdd)
    {
        var addSuccess = CardSystem.AddToCardSystem(cardToAdd);
        if (addSuccess) ShuffleDeck(); // if the card is added, we reshuffle the Deck
        return addSuccess;
    }

    public void ShuffleDeck() // shuffle the deck by assigning a randomly genereated number to each slot and
    {
        slotNumbersList.Clear();
        foreach (CardSlot slot in CardSystem.CardSlots)
        {   // some random way of generating a random number.. could be better
            int rand = Random.Range(1, CardSystem.CardSlots.Count * Random.Range(11, 24)) * Random.Range(7, 18);
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
        Card card = cardSlot.Card;
        slotNumbersList.RemoveAt(0);
        CardSystem.RemoveCardSlot(cardSlot);
        OnDeckOfCardsDisplayRequested?.Invoke(_cardSystem);
        return card;
    }

    private void Start()
    {
        isHidden = true;
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
            isHidden = false;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {   // open deck of cards
            ShuffleDeck();
        }
        if (CardSystem.CardSlots.Count <= 0 && !isHidden) OnDeckOfCardsDisplayHideRequested?.Invoke(isHidden);
    }
}


