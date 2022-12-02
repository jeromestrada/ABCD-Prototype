using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandOfCards : CardSystemHolder
{
    [SerializeField] private int _maxHandSize;
    [SerializeField] private DeckOfCards deck;
    [SerializeField] private DiscardedCards discardPile;

    public static UnityAction<CardSystem> OnHandOfCardsDisplayRequested;
    public static event System.Action<StatCard, bool> OnStatCardDrawnDiscarded;
    public int MaxHandSize => _maxHandSize;


    protected override void Awake()
    {
        base.Awake();
        SaveLoad.OnLoadGame += LoadHand;
    }

    // Start is called before the first frame update
    void Start()
    {
        var handSaveData = new CardSystemHolderSaveData(_cardSystem);
        SaveGameManager.data.handDictionary.Add(GetComponent<UniqueID>().ID, handSaveData);
    }

    private void LoadHand(SaveData data)
    {
        if (data.handDictionary.TryGetValue(GetComponent<UniqueID>().ID, out CardSystemHolderSaveData handData))
        {   // check the save data for this deck, if it exists load it in
            Debug.Log("hand data found!");
            _cardSystem = handData.cardSystem;
        }
        if(_cardSystem.CardSystemSize > 0)
        {
            OnHandOfCardsDisplayRequested?.Invoke(_cardSystem);
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.X))
        {
            DrawCard();
        }
    }

    public void DrawCard()
    {
        if (CardSystemSize < _maxHandSize)
        {
            Card card = deck.GetTopCard();
            if (card == null) return;
            _cardSystem.AddToCardSystem(card);
            OnHandOfCardsDisplayRequested?.Invoke(_cardSystem);
            if(card.CardType == CardType.StatCard)
            {
                OnStatCardDrawnDiscarded?.Invoke((StatCard)card, true); // this invocation handles the player's stats affected by the cards currently at hand
            }
        }
        else Debug.Log("Player hand is full!");
    }

    public void DrawNCards(int amountToDraw)
    {
        for(int i = 0; i < amountToDraw; i++)
        {
            DrawCard();
        }
    }

    public void DiscardNCards(int amountToDiscard)
    {
        for(int i = 0; i < amountToDiscard; i++)
        {
            var card = CardSystem.CardSlots[0].Card;
            discardPile.Discard(card);
            if (card.CardType == CardType.StatCard)
            {
                OnStatCardDrawnDiscarded?.Invoke((StatCard)card, false); // this invocation handles the player's stats affected by the cards currently at hand
            }
            CardSystem.CardSlots.RemoveAt(0);
            OnHandOfCardsDisplayRequested?.Invoke(_cardSystem);
        }
    }

    public void DiscardAll()
    {
        DiscardNCards(CardSystemSize);
    }
}
