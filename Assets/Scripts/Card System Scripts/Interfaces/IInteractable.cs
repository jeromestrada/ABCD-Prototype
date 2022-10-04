 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IInteractable
{
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }
    public void Interact(InteractableScanner scanner, out bool interactSuccessful);

    public void EndInteraction();
}
