using UnityEngine;

[CreateAssetMenu(fileName ="New Card", menuName ="Cards/Card")]
public class Card : ScriptableObject
{
    new public string name;
    public string description;
    public CardType cardType;
    public int numOfUses = 1; // a card can be used atleast once
    [SerializeField] private bool exhaustable;
}

public enum CardType { ItemCard, AttackCard, DefendCard, UtilityCard}
