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


    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }
    public virtual void Interact()
    {
        // this is meant to be overwritten
        Debug.Log("Interacting with " + transform.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && !hasInteracted) // only process when this interactable is in range
        {
            
            if (interacting)
            {
                Interact();
                hasInteracted = true;
                Debug.Log("INTERACTED with " + transform.name + "!");
            }
        }
    }

    public void WhenInRange(Transform playerTransform)
    {
        float distance = Vector3.Distance(player.position, interactionTransform.position);
        if (player != null && !hasInteracted)
        {
            if(distance < radius)
            {
                Debug.Log("In range of " + transform.name);
                isInRange = true;
                player = playerTransform;
            }
        }
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
