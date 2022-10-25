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
    {   // TODO: consider refactoring to Action pattern. Will trigger an OnPickUp action and deck will be listening to it for adding.
        var deck = player.GetComponentInChildren<DeckOfCards>(); // refactoring will eliminate the need for this object to refer to the deck this way
        if(deck != null)
        {
            if (deck.AddCardToDeck(card))
            {   // destroy the game object, we can pass in the object into the action to destroy it in DeckOfCards when successfully adding it in.
                Destroy(gameObject); 
            }
        }
    }
}
