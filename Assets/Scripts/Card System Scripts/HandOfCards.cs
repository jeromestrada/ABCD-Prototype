using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandOfCards : CardSystemHolder
{
    [SerializeField] private int maxHandSize;
    [SerializeField] private DeckOfCards deck;
    public static UnityAction<CardSystem> OnHandOfCardsDisplayRequested;


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
        if (CardSystemSize < maxHandSize)
        {
            Card card = deck.GetTopCard();
            if (card == null) return;
            _cardSystem.AddToCardSystem(card);
            OnHandOfCardsDisplayRequested?.Invoke(_cardSystem);
        }
        else Debug.Log("Player hand is full!");
    }
}
