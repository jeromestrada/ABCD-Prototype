using UnityEngine;

[CreateAssetMenu(fileName ="New Card", menuName ="Cards/Card")]
public class Card : ScriptableObject
{
    new public string name;
    public Sprite cardIcon;
    [TextArea(4,4)]
    public string description;
    public CardType cardType;
    public int numOfUses = 1; // a card can be used atleast once
    private int stackSize;
    [SerializeField] private bool exhaustable;

    public int MaxStackSize => stackSize;
}

public enum CardType { ItemCard, AttackCard, DefendCard, UtilityCard}
