using UnityEngine;

[CreateAssetMenu(fileName = "New Item Card", menuName = "Cards/Item Card")]
public class ItemCard : Card
{
    public Item item;

    public override bool Use()
    {
        item.Use();
        return base.Use();
    }
}
