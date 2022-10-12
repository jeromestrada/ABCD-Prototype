using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private ShopKeeperDisplay _shopKeeperDisplay;
    [SerializeField] private BuyConfirmWindow _buyConfirmationWindow;

    public BuyConfirmWindow BuyConfirmationWindow => _buyConfirmationWindow;
    public ShopKeeperDisplay ShopKeeperDisplay => _shopKeeperDisplay;

    private void Awake()
    {
        _shopKeeperDisplay.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (ShopKeeperDisplay.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape)) ShopKeeperDisplay.gameObject.SetActive(false);
        if (BuyConfirmationWindow.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Z)) BuyConfirmationWindow.gameObject.SetActive(false);
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

    private void DisplayBuyConfirmWindow(ShopSlot_UI shopSlot)
    {
        BuyConfirmationWindow.UpdateConfirmImage(shopSlot.AssignedShopSlot);
        BuyConfirmationWindow.WindowPanel.SetActive(true);
    }
}
