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
    public CardType cardType;
    [SerializeField] private int _cardPrice;
    [SerializeField] private int numOfUses = 1; // a card can be used atleast once
    [SerializeField] private bool exhaustable;
    private int remainingUses;

    public int CardPrice => _cardPrice;
    public int NumOfUses => numOfUses;
    public CardRarity CardRarity => _cardRarity;

    private void Awake()
    {
        remainingUses = numOfUses;
    }

    private void SetRarity(CardRarity rarity)
    {
        _cardRarity = rarity;
    }

    public virtual bool Use()
    {
        return true;
    }
}

public enum CardType { ItemCard, AttackCard, DefendCard, UtilityCard}
public enum CardRarity { Legendary, Mythical, Rare, Uncommon, Common }
