using UnityEngine;

[CreateAssetMenu(fileName = "New Item Card", menuName = "Cards/Item Card")]
public class ItemCard : Card
{
    public Item item;

    private void Awake()
    {
        _cardType = CardType.ItemCard;
    }
    public override bool Use()
    {
        item.Use(); // can this be refactored to an Action pattern? i.e. calling an OnItemUse action then passing in this card, 
        return base.Use();
    }
}
