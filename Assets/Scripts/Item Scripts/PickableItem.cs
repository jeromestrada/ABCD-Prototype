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
        Debug.Log("Picking up " + hasInteracted);
        if (hasInteracted)
        {
            Destroy(gameObject);
        }
    }
}
