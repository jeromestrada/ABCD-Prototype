using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop System/Shop Stock List")]
public class ShopStockList : ScriptableObject
{
    [SerializeField] private List<ShopCard> _cardsInStock;
    [SerializeField] private int _maxGold;
    [SerializeField] private float _sellMarkUp;
    [SerializeField] private float _buyMarkUp;

    public List<ShopCard> CardsInStock => _cardsInStock;
    public int MaxGold => _maxGold;
    public float SellMarkUp => _sellMarkUp;
    public float BuyMarkUp => _buyMarkUp;

}

[System.Serializable]
public struct ShopCard
{
    public Card Card;
    public int StockAmount;
}
