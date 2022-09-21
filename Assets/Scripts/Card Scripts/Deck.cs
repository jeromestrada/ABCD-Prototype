using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> cards;

    private void Awake()
    {
        cards = new List<Card>();
    }
    public void AddCard(Card card)
    {
        cards.Add(card);
    }
}
