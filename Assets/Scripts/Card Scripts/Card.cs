using UnityEngine;

[CreateAssetMenu(fileName ="New Card", menuName ="Cards/Card")]
public class Card : ScriptableObject
{
    public int ID = -1;
    new public string name;
    public Sprite cardIcon;
    [TextArea(4,4)]
    public string description;
    public CardType cardType;
    [SerializeField] private int numOfUses = 1; // a card can be used atleast once
    [SerializeField] private bool exhaustable;
    private int remainingUses;

    public int NumOfUses => numOfUses;

    private void Awake()
    {
        remainingUses = numOfUses;
    }

    public virtual bool Use()
    {
        return true;
    }
}

public enum CardType { ItemCard, AttackCard, DefendCard, UtilityCard}
