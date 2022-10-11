using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopKeeperDisplay : CardSystemDisplay
{
    [SerializeField] private ShopSlotUI _shopSlotPrefab;
    [SerializeField] private Button _closeButton;

    [SerializeField] private GameObject _cardListShopWindow;

    private ShopSystem _shopSystem;
    private DeckOfCards _playerDeck;
    private Dictionary<ShopSlot, int> _shopList = new Dictionary<ShopSlot, int>();
    private Dictionary<ShopSlot, CardSlot_UI> _shopListUI = new Dictionary<ShopSlot, CardSlot_UI>();
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
