using UnityEngine;

[CreateAssetMenu(fileName ="New Card", menuName ="Cards/Card")]
public class Card : ScriptableObject
{
    public int ID = -1;
    [SerializeField] private CardRarity _cardRarity;
    new public string name;
    public Sprite cardIcon;
    [TextArea(4,4)]
    public string description;
    [SerializeField] private CardType _cardType;
    [SerializeField] private int _cardPrice;
    [SerializeField] private int numOfUses = 1; // a card can be used atleast once
    [SerializeField] private bool exhaustable;

    public int CardPrice => _cardPrice;
    public int NumOfUses => numOfUses;
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
