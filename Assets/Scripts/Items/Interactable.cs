using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 2f;
    public Transform interactionTransform; // so we can set a variable location when we want to

    bool isInRange = false;
    bool hasInteracted = false;
    public Transform player;
    public bool interacting = false;

    float isInRangeResetTimer = 0.5f;


    private void Awake()
    {
        
    }
    public virtual void Interact()
    {
        // this is meant to be overwritten
        //Debug.Log("Interacting with " + transform.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && !hasInteracted) // only process when this interactable is in range
        {                                // and we haven't interacted with it
            if (interacting) // wait until the player triggers the interaction
            {
                Interact();
                hasInteracted = true;
                Debug.Log("INTERACTED with " + transform.name + "!");

                // disable collider once interacted with. this is just a test
                // since classes that will inherit this class will be responsible for the interaction limit.
                // i.e infinitely interactable objects won't get disabled at all
                GetComponent<SphereCollider>().enabled = false;
                this.enabled = false;
            }
        }
    }

    public void WhenInRange(Transform playerTransform) // activates the interactable by setting isInRange to true
    {
        player = playerTransform;
        if (player != null && !hasInteracted)
        {
            isInRange = true;
            Invoke(nameof(ResetInRange), isInRangeResetTimer); // reset isInRange after timer in case player moves out of range for too long.
        }
    }

    private void ResetInRange()
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
