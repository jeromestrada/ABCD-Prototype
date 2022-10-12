using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShopKeeperDisplay : CardSystemDisplay
{
    [SerializeField] private ShopSlot_UI _shopSlotPrefab;
    [SerializeField] private Button _closeButton;

    [SerializeField] private GameObject _cardListShopWindow;
    [SerializeField] private BuyConfirmWindow _buyConfirmationWindow;

    private ShopSystem _shopSystem;
    private DeckOfCards _playerDeck;
    private Dictionary<ShopSlot, int> _shopList = new Dictionary<ShopSlot, int>();
    private Dictionary<ShopSlot, CardSlot_UI> _shopListUI = new Dictionary<ShopSlot, CardSlot_UI>();

    public static UnityAction OnBuyConfirmWindowRequested;

    public BuyConfirmWindow BuyConfirmationWindow => _buyConfirmationWindow;
    private void RefreshShopDisplay()
    {
        ClearSlots();
        DisplayShopInventory();
    }
    public override void ClearSlots()
    {
        _shopList = new Dictionary<ShopSlot, int>();
        _shopListUI = new Dictionary<ShopSlot, CardSlot_UI>();
        foreach (var card in _cardListShopWindow.transform.Cast<Transform>())
        {
            Destroy(card.gameObject);
        }
    }

    public static void DisplayBuyConfirmWindow(ShopSlot_UI shopSlot)
    {
        // update the buy confirm window's sprite and texts here 
        SetBuyConfirmDetails(shopSlot);
        OnBuyConfirmWindowRequested?.Invoke();
    }
    public static void SetBuyConfirmDetails(ShopSlot_UI shopSlot)
    {
        BuyConfirmationWindow.UpdateConfirmImage(shopSlot.AssignedShopSlot);
    }
    public void DisplayShopWindow(ShopSystem shopSystem, DeckOfCards deck)
    {
        _shopSystem = shopSystem;
        _playerDeck = deck;
        RefreshShopDisplay();
    }
    public void DisplayShopInventory()
    {
        foreach(var shopSlot in _shopSystem.ShopInventory)
        {
            if (shopSlot.Card == null) continue;

            var slot = Instantiate(_shopSlotPrefab, _cardListShopWindow.transform);
            slot.Init(shopSlot, _shopSystem.BuyMarkUp);
        }
    }

    public override void AssignSlots(CardSystem invToDisplay)
    {
        
    }
}
