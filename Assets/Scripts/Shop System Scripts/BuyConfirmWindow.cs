using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyConfirmWindow : MonoBehaviour
{
    [SerializeField] private Image _cardSprite;
    private Card _cardToDisplay;
    [SerializeField] private ShopSystem _shopSystem;
    [SerializeField] private Button _buyButton;
    [SerializeField] private GameObject _windowPanel;

    public GameObject WindowPanel => _windowPanel;
    private void Awake()
    {
        _buyButton?.onClick.AddListener(OnBuyConfirm);
    }

    private void OnBuyConfirm()
    {
        
    }

    public void UpdateConfirmImage(ShopSlot shopSlot)
    {
        _cardToDisplay = shopSlot.Card;
        _cardSprite.sprite = _cardToDisplay.cardIcon;
    }
    
}
