using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UniqueID))]
public class ShopKeeper : Interactable // Shop system holder...
{

    [SerializeField] private ShopStockList _shopStockList; // stock list of this shop
    [SerializeField] private ShopSystem _shopSystem;
    public static UnityAction<ShopSystem, DeckOfCards> OnShopWindowRequested;

    private void Awake()
    {
        _shopSystem = new ShopSystem(_shopStockList.CardsInStock.Count, _shopStockList.MaxGold, _shopStockList.BuyMarkUp, _shopStockList.SellMarkUp);

        foreach(var stockCard in _shopStockList.CardsInStock)
        {
            _shopSystem.AddToShop(stockCard.Card, stockCard.StockAmount);
        }
    }

    public override void Interact(InteractableScanner scanner, out bool interactSuccessful)
    {
        // display the shop when interacted with
        var deck = scanner.GetComponentInChildren<DeckOfCards>(); // get the deck from the scanner

        if(deck != null)
        {
            OnShopWindowRequested?.Invoke(_shopSystem, deck);
            interactSuccessful = true;
        }
        else
        {
            interactSuccessful = false;
            Debug.LogError("Player inventory not found!");
        }
    }
}
