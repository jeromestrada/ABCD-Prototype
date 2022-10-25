using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableCard : Interactable
{
    public Card card;

    public static System.Action<PickableCard> OnCardPickup;
    
    public override void Interact(InteractableScanner scanner, out bool interactionSuccessful)
    {
        base.Interact(scanner, out interactionSuccessful);
        Pickup();
    }

    void Pickup()
    {
        OnCardPickup?.Invoke(this);
    }
}
