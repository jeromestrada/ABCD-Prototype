using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatePlatform : Interactable
{
    // this will serve as an interaction point that allows the player to move between rooms,
    // this will involve triggering the Player's nav mesh agent to move the player through an off mesh link
    // the player's controller should be temporarily disabled when the interaction is happening.
    // use the interactSuccessful to indicate the end of the interaction, maybe?

    public static event System.Action<GatePlatform> OnPass;
    public override void Interact(InteractableScanner interactor, out bool interactSuccessful)
    {
        Pass();
        base.Interact(interactor, out interactSuccessful);
    }

    void Pass()
    {
        OnPass?.Invoke(this);
    }
}
