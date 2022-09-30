using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//TODO:
/*
 Refactor this class to implement an IInteractable interface
 
 */

[RequireComponent(typeof(SphereCollider))]
public class Interactable : MonoBehaviour, IInteractable
{
    public float radius = 2f;
    public Transform interactionTransform; // so we can set a variable location when we want to

    bool isInRange = false;
    public bool hasInteracted = false;
    public Transform player;

    private SphereCollider myCollider;

    public UnityAction<IInteractable> OnInteractionComplete { get; set; }

    private void Awake()
    {
        myCollider = GetComponent<SphereCollider>();
        myCollider.radius = radius;
    }

    public virtual void Interact(InteractableScanner interactor, out bool interactSuccessful)
    {
        // start the interaction logic here.
        hasInteracted = true;
        interactSuccessful = true;
    }

    public void EndInteraction()
    {

    }

    public void WhenInRange(Transform playerTransform) // activates the interactable by setting isInRange to true
    {
        if (!isInRange) // if we're not in range already
        {
            player = playerTransform;
            if (player != null && !hasInteracted)
            {
                isInRange = true;
            }
        }
    }
    public void WhenNotInRange()
    {
        isInRange = false;
        player = null;
    }
    private void OnDrawGizmosSelected()
    {
        if(interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    
}
