using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandOfCards : CardSystemHolder
{
    [SerializeField] private int maxHandSize;
    [SerializeField] private DeckOfCards deck;
    public static UnityAction<CardSystem> OnHandOfCardsDisplayRequested;

    // Start is called before the first frame update
    void Start()
    {
        
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
        Card card = deck.GetTopCard();
        if (card == null) return;
        _cardSystem.AddToCardSystem(card);
        OnHandOfCardsDisplayRequested?.Invoke(_cardSystem);
    }
}
