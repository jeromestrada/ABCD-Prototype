using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[RequireComponent(typeof(UniqueID))]
public class DeckOfCards : CardSystemHolder
{
    [SerializeField] List<Card> startingCards;
    private List<int> slotNumbersList; // we will use this to access the cards using Linq functions 
    // like: ...   cardSystem.Find(cardSlot).Where(i => i.slotNumber == slotNumbersList[0])
    // remove number from slotNumbersList and DrawCard from the deck into the hand.

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

    public void ShuffleDeck(int seed) // shuffle the deck by assigning a randomly genereated number to each slot and
    {
        // generate a random number and assign it to a slot
        // add generated number to the slotNumbersList while maintaining order
        // an algorithm similar to bubble sort will suffice as we add the numbers one by one anyway
    }

    private void Start()
    {
        // add the starting cards into the deck
        foreach (Card card in startingCards)
        {
            _cardSystem.AddToCardSystem(card);
        }

        slotNumbersList = new List<int>();

        var deckSaveData = new CardSystemHolderSaveData(_cardSystem);
        SaveGameManager.data.deckDictionary.Add(GetComponent<UniqueID>().ID, deckSaveData);
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.C))
        {   // open deck of cards
            OnDynamicCardSystemDisplayRequested?.Invoke(_cardSystem);
        }
    }
}


