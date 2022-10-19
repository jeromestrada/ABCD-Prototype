using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyConfirmWindow : MonoBehaviour
{
    [SerializeField] private Image _cardSprite;
    private Card _cardToDisplay;
    private ShopSlot _shopSlotDisplayed;
    [SerializeField] private ShopSystem _shopSystem;
    [SerializeField] private Button _buyButton;
    [SerializeField] private GameObject _windowPanel;
    [SerializeField] private DeckOfCards _deckOfCards;

    public GameObject WindowPanel => _windowPanel;
    private void Awake()
    {
        _buyButton?.onClick.AddListener(OnBuyConfirm);
    }

    private void OnBuyConfirm()
    {
        Debug.Log($"Confirming buy: {_cardToDisplay.name}");
        // buy the card from the shop system here. use the _shopSystem cached in this object
        _shopSystem.BuyFromShop(_shopSlotDisplayed, _deckOfCards);
        gameObject.SetActive(false);
    }

    public void UpdateConfirmImage(ShopSlot shopSlot)
    {
        _shopSlotDisplayed = shopSlot;
        _cardToDisplay = _shopSlotDisplayed.Card;
        _cardSprite.sprite = _cardToDisplay.cardIcon;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
    
}
