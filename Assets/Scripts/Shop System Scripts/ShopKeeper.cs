using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UniqueID))]
public class ShopKeeper : MonoBehaviour , IInteractable
{

    [SerializeField] private ShopStockList _shopStockList; // stock list of this shop
    private ShopSystem _shopSystem;
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }


    private void Awake()
    {
        _shopSystem = new ShopSystem(_shopStockList.CardsInStock.Count, _shopStockList.MaxGold, _shopStockList.BuyMarkUp, _shopStockList.SellMarkUp);

        foreach(var stockCard in _shopStockList.CardsInStock)
        {
            _shopSystem.AddToShop(stockCard.Card, stockCard.StockAmount);
        }
    }

    public void EndInteraction()
    {
        throw new System.NotImplementedException();
    }

    public void Interact(InteractableScanner scanner, out bool interactSuccessful)
    {
        throw new System.NotImplementedException();
    }
}
