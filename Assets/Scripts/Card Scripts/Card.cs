using UnityEngine;

[CreateAssetMenu(fileName ="New Card", menuName ="Cards/Card")]
public class Card : ScriptableObject
{
    new public string name;
    public Sprite cardIcon;
    [TextArea(4,4)]
    public string description;
    public CardType cardType;
    [SerializeField] private int numOfUses = 1; // a card can be used atleast once
    [SerializeField] private bool exhaustable;
    public int NumOfUses => numOfUses;

    public virtual bool Use()
    {
        // use card and return if the card was used successfully
        numOfUses--;
        if (numOfUses <= 0) Debug.Log($"{this.name} is completely used up");
        return true;
    }
}

public enum CardType { ItemCard, AttackCard, DefendCard, UtilityCard}
