using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ShopSystem
{
    [SerializeField] private List<ShopSlot> _shopInventory;

    [SerializeField] private int _availableGold;
    [SerializeField] private float _buyMarkUp;
    [SerializeField] private float _sellMarkUp;

    public List<ShopSlot> ShopInventory => _shopInventory;
    public float BuyMarkUp => _buyMarkUp;
    public float SellMarkUp => _sellMarkUp;

    public ShopSystem(int size, int gold, float buyMarkUp, float sellMarkUp)
    {
        _availableGold = gold;
        _buyMarkUp = buyMarkUp;
        _sellMarkUp = sellMarkUp;

        SetShopSize(size);
    }


    private void SetShopSize(int size)
    {
        _shopInventory = new List<ShopSlot>();
        for(int i = 0; i < size; i++)
        {
            _shopInventory.Add(new ShopSlot());
        }
    }

    public void AddToShop(Card cardToAdd, int amount)
    {
        if(ContainsCard(cardToAdd, out ShopSlot shopSlot))
        {
            shopSlot.AddToStack(amount);
        }

        var freeSlot = GetFreeSlot();
        freeSlot.AssignCard(cardToAdd, amount);
    }

    public bool BuyFromShop(ShopSlot shopSlot, DeckOfCards deck)
    {
        if (shopSlot.StackSize <= 0)
        {
            Debug.Log($"{shopSlot.Card.name} is out of stock!");
            return false;
        }
        deck.AddCardToDeck(shopSlot.Card);
        shopSlot.RemoveFromStack(1);
        return true;
    }

    private ShopSlot GetFreeSlot()
    {
        var freeSlot = _shopInventory.FirstOrDefault(i => i.Card == null);
        if(freeSlot == null)
        {
            freeSlot = new ShopSlot();
            _shopInventory.Add(freeSlot);
        }
        return freeSlot;
    }
    public bool ContainsCard(Card cardToAdd, out ShopSlot shopSlot)
    {
        shopSlot = _shopInventory.Find(i => i.Card == cardToAdd);
        return shopSlot != null;
    }
}
