using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : Interactable
{
    public Item item;
    public Card card;
    
    public override void Interact()
    {
        base.Interact();
        Pickup();
    }

    void Pickup()
    {
        var inventory = player.transform.GetComponent<InventoryHolder>();
        if(inventory != null)
        {
            if (inventory.InventorySystem.AddToInventory(card, 1))
            {
                Destroy(gameObject); // destroy the game object
            }
        }
    }
}
