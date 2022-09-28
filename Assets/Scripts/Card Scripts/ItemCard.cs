using UnityEngine;

[CreateAssetMenu(fileName = "New Item Card", menuName = "Cards/Item Card")]
public class ItemCard : Card
{
    public Item item;

    public override bool Use()
    {
        Debug.Log("Using the item in card");
        numOfUses--;
        return base.Use();
    }
}
