using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private ShopKeeperDisplay _shopKeeperDisplay;

    private void Awake()
    {
        _shopKeeperDisplay.gameObject.SetActive(false);
    }

    private void Update()
    {
        
    }
    private void OnEnable()
    {
        ShopKeeper.OnShopWindowRequested += DisplayShopWindow;
        ShopKeeperDisplay.OnBuyConfirmWindowRequested += DisplayBuyConfirmWindow;
    }

    private void OnDisable()
    {
        ShopKeeper.OnShopWindowRequested -= DisplayShopWindow;
        ShopKeeperDisplay.OnBuyConfirmWindowRequested -= DisplayBuyConfirmWindow;
    }

    private void DisplayShopWindow(ShopSystem shopSystem, DeckOfCards deck)
    {
        _shopKeeperDisplay.gameObject.SetActive(true);
        _shopKeeperDisplay.DisplayShopWindow(shopSystem, deck);
    }

    private void DisplayBuyConfirmWindow()
    {
        _shopKeeperDisplay.BuyConfirmationWindow.WindowPanel.SetActive(true);
    }
}
