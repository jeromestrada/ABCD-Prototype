using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardSystemUIController : MonoBehaviour
{
    public DynamicCardSystemDisplay DeckPanel;
    public DynamicCardSystemDisplay HandPanel;


    private void Awake()
    {
        DeckPanel.gameObject.SetActive(false);
        HandPanel.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        DeckOfCards.OnDeckOfCardsDisplayRequested += DisplayDeck;
        DeckOfCards.OnDeckOfCardsDisplayHideRequested += HideDeck;
        HandOfCards.OnHandOfCardsDisplayRequested += DisplayHand;
    }
    private void OnDisable()
    {
        DeckOfCards.OnDeckOfCardsDisplayRequested -= DisplayDeck;
        DeckOfCards.OnDeckOfCardsDisplayHideRequested -= HideDeck;
        HandOfCards.OnHandOfCardsDisplayRequested -= DisplayHand;
    }
    // Update is called once per frame
    void Update()
    {
        if(DeckPanel.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape)) DeckPanel.gameObject.SetActive(false);
    }

    void DisplayDeck(CardSystem cardSysToDisplay)
    {
        DeckPanel.gameObject.SetActive(true); 
        DeckPanel.RefreshDynamicInventory(cardSysToDisplay);
    }

    void HideDeck()
    {
        DeckPanel.gameObject.SetActive(false);
    }

    void DisplayHand(CardSystem cardSysToDisplay)
    {
        HandPanel.gameObject.SetActive(true);
        HandPanel.RefreshDynamicInventory(cardSysToDisplay);
    }

}
