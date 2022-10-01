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
        var inventory = player.transform.GetComponent<CardSystemHolder>();
        if(inventory != null)
        {
            if (inventory.CardSystem.AddToInventory(card, card.NumOfUses))
            {
                Destroy(gameObject); // destroy the game object
            }
        }
    }
}
