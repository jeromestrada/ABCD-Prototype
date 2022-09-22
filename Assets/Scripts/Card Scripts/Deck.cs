using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> cards; // we can assign a starting set of cards
    public List<Weapon> weapons; // keeps track of the available weapons in the deck

    private void Awake()
    {
        if(cards == null)
        {
            cards = new List<Card>();
        }
        else
        {
            foreach(ItemCard i in cards)
            {
                AddWeapon(i);
            }
        }
    }
    public void AddCard(Card card) // use when adding a new card
    {
        cards.Add(card);
        if(card.cardType == CardType.ItemCard)
        {
            AddWeapon((ItemCard)card);
        }
    }

    public void AddWeapon(ItemCard itemCard)
    {
        if(itemCard.item is Weapon)
            weapons.Add((Weapon)itemCard.item);
    }
}
