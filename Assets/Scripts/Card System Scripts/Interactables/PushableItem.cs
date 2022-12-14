using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableItem : Interactable
{
    public override void Interact(InteractableScanner scanner, out bool interactionSuccessful)
    {
        base.Interact(scanner, out interactionSuccessful);
        Push();
    }

    void Push()
    {
        Vector3 direction = (transform.position - player.transform.position).normalized;
        direction.y = 0;
        transform.position += direction * 3;
        // reset interaction status so we can do it infinitely
        hasInteracted = false;
    }
}
