using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableCard : Interactable
{
    public Card card;
    
    public override void Interact(InteractableScanner scanner, out bool interactionSuccessful)
    {
        base.Interact(scanner, out interactionSuccessful);
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
