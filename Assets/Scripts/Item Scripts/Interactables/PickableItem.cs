using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : Interactable
{
    public Item item;
    
    public override void Interact()
    {
        base.Interact();
        Pickup();
    }

    void Pickup()
    {
        if (hasInteracted)
        {
            Destroy(gameObject);
        }
    }
}
