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
        var deck = player.GetComponentInChildren<DeckInventory>();
        if(deck != null)
        {
            if (deck.CardSystem.AddToCardSystem(card))
            {
                Destroy(gameObject); // destroy the game object
            }
        }
    }
}
