using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private ShopKeeperDisplay _shopKeeperDisplay;
    [SerializeField] private BuyConfirmWindow _buyConfirmationWindow;
    [SerializeField] private LootSystemDisplay _lootSystemDisplay;

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
        Chest.OnLootWindowRequested += DisplayLootWindow;
        ShopKeeperDisplay.OnBuyConfirmWindowRequested += DisplayBuyConfirmWindow;
    }
    private void OnDisable()
    {
        ShopKeeper.OnShopWindowRequested -= DisplayShopWindow;
        Chest.OnLootWindowRequested += DisplayLootWindow;
        ShopKeeperDisplay.OnBuyConfirmWindowRequested -= DisplayBuyConfirmWindow;
    }

    private void DisplayLootWindow(LootSystem lootSystem, DeckOfCards deck)
    {
        _lootSystemDisplay.gameObject.SetActive(true);
        _lootSystemDisplay.DisplayLootWindow(lootSystem, deck);
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
