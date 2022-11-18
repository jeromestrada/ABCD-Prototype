using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[RequireComponent(typeof(UniqueID))]
public class DeckOfCards : CardSystemHolder
{
    [SerializeField] CardSystem _viewOnlyDeck;
    [SerializeField] List<Card> _startingCards;
    [SerializeField] private List<int> slotNumbersList;
    public static UnityAction<CardSystem> OnDeckOfCardsDisplayRequested;
    public static UnityAction<CardSystem> OnDeckOfCardsDisplayHideRequested;
    private bool isHidden;

    private void OnEnable()
    {
        PickableCard.OnCardPickup += PickCard;
    }
    private void OnDisable()
    {
        PickableCard.OnCardPickup -= PickCard;
    }

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
        if (data.viewOnlyDeckDictionary.TryGetValue(GetComponent<UniqueID>().ID, out CardSystemHolderSaveData viewDeckData))
        {   // check the save data for this deck, if it exists load it in
            _viewOnlyDeck = viewDeckData.cardSystem;
        }
    }

    // this allows the deck to destroy the pickable when picking it up
    public void PickCard(PickableCard pickable)
    {
        AddCardToDeck(pickable.card);
        Destroy(pickable.gameObject);
    }

    public void AddCardToDeck(Card cardToAdd)
    {
        // Debug.Log($"Adding {cardToAdd.name}");
        var addSuccess = CardSystem.AddToCardSystem(cardToAdd);
        if (addSuccess)
        {
            _viewOnlyDeck.AddToCardSystem(cardToAdd); // add the new card into the view only deck as well
            ShuffleDeck(); // if the card is added, we reshuffle the Deck
        }
    }

    public void ShuffleDeck() // shuffle the deck by assigning a randomly genereated number to each slot and
    {
        slotNumbersList.Clear();
        foreach (PlayerCardSlot slot in CardSystem.CardSlots)
        {   // some random way of generating a random number.. could be better | still affected by the seed consistently
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

        PlayerCardSlot cardSlot = CardSystem.CardSlots.Find(s => s.SlotNumber == slotNumbersList[0]);
        Card card = cardSlot.Card;
        slotNumbersList.RemoveAt(0);
        CardSystem.RemoveCardSlot(cardSlot);
        return card;
    }


    public void RemoveCard(PlayerCardSlot cardSlot)
    {
        CardSystem.RemoveCardSlot(cardSlot);
        UpdateViewOnlyDeck();
    }

    public void UpdateViewOnlyDeck()
    {
        _viewOnlyDeck.CardSlots.Clear();
        foreach(var c in _cardSystem.CardSlots)
        {
            _viewOnlyDeck.AddToCardSystem(c.Card);
        }
    }

    private void Start()
    {
        isHidden = true;
        // add the starting cards into the deck
        foreach (Card card in _startingCards)
        {
            _cardSystem.AddToCardSystem(card);
        }

        foreach (Card card in _startingCards)
        {
            _viewOnlyDeck.AddToCardSystem(card);
        }

        slotNumbersList = new List<int>();
        ShuffleDeck();

        var deckSaveData = new CardSystemHolderSaveData(_cardSystem);
        var viewOnlyDeckSaveData = new CardSystemHolderSaveData(_viewOnlyDeck);
        SaveGameManager.data.deckDictionary.Add(GetComponent<UniqueID>().ID, deckSaveData);
        SaveGameManager.data.viewOnlyDeckDictionary.Add(GetComponent<UniqueID>().ID, viewOnlyDeckSaveData);
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.C))
        {   // open/close deck of cards
            if (isHidden && _cardSystem.CardSystemSize != 0)
            {
                OnDeckOfCardsDisplayRequested?.Invoke(_cardSystem);
                isHidden = false;
            }
            else
            {
                OnDeckOfCardsDisplayHideRequested?.Invoke(_cardSystem);
                isHidden = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {   // open/close deck of cards
            Debug.Log($"viewing, isHidden is {isHidden}, vod has {_viewOnlyDeck.CardSystemSize}");
            if (isHidden && _viewOnlyDeck.CardSystemSize != 0)
            {
                OnDeckOfCardsDisplayRequested?.Invoke(_viewOnlyDeck);
                isHidden = false;
            }
            else
            {
                OnDeckOfCardsDisplayHideRequested?.Invoke(_viewOnlyDeck);
                isHidden = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {   // shuffle the deck of cards
            ShuffleDeck();
        }
        if (CardSystem.CardSlots.Count <= 0 && !isHidden) OnDeckOfCardsDisplayHideRequested?.Invoke(_cardSystem);
    }
}


