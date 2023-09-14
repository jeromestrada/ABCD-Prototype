using UnityEngine;

[CreateAssetMenu(fileName ="New Card", menuName ="Cards/Card")]
public class Card : ScriptableObject
{
    public int ID = -1;
    [SerializeField] private CardRarity _cardRarity;
    new public string name;
    public int manaCost;
    public Sprite cardIcon;
    [TextArea(4,4)]
    public string description;
    [SerializeField] protected CardType _cardType;
    [SerializeField] protected int _cardPrice; // TODO: have a modifiable card price in the shop slot based on this

    public int CardPrice => _cardPrice;
    public CardRarity CardRarity => _cardRarity;
    public CardType CardType => _cardType;


    private void SetRarity(CardRarity rarity)
    {
        _cardRarity = rarity;
    }

    public virtual bool Use()
    {
        return true;
    }
}

public enum CardType { ItemCard, AttackCard, DefendCard, AbilityCard, StatCard }
public enum CardRarity { Legendary, Mythical, Rare, Uncommon, Common }
