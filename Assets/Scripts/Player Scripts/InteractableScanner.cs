using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO: refactor this class to use the refactored Interactable class.
public class InteractableScanner : MonoBehaviour
{
    public Interactable closestInteractable = null;
    float interactableScanInterval = 0.5f;
    bool alreadyScanned = false;

    public bool IsInteracting { get; private set; }

    private List<Interactable> interactables;
    // Start is called before the first frame update
    void Start()
    {
        interactables = new List<Interactable>();
    }

    void Update()
    {
        if(interactables.Count > 0)
        {
            ScanForClosestInteractable();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (closestInteractable != null) StartInteraction(closestInteractable);
        }
    }

    void StartInteraction(IInteractable interactable)
    {
        interactable.Interact(this, out bool hasInteracted);
        IsInteracting = true;
    }

    public void ScanForClosestInteractable()
    {
        if (!alreadyScanned)
        {
            ResetClosestInteractable();
            float closestDist = float.MaxValue;
            foreach (Interactable i in interactables) // finds the closest interactable detected
            {
                if(i == null) // since some of interactables can Destroy themselves, we check for nulls
                {
                    interactables.Remove(i);
                    return;
                }
                float distance = Vector3.Distance(i.interactionTransform.position, transform.position);
                if (distance < closestDist)
                {
                    closestDist = distance;
                    closestInteractable = i;
                }
                i.WhenInRange(transform); // we're in range if we haven't exited the trigger, so we activate the interactable.
            }
            alreadyScanned = true;
            Invoke(nameof(ResetScan), interactableScanInterval); // reset scan for interactables after given seconds lapsed.
        }
    }

    private void ResetScan()
    {
        alreadyScanned = false;
        ResetClosestInteractable();
    }

    private void ResetClosestInteractable()
    {
        closestInteractable = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactableObj = other.transform.GetComponent<Interactable>();
        if (interactableObj != null)
        {
            interactables.Add(interactableObj);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var interactableObj = other.transform.GetComponent<Interactable>();
        if (interactableObj != null)
        {
            interactableObj.WhenNotInRange();
            interactables.Remove(interactableObj);
        }
        if (interactables.Count == 0) // null the closest if the interactables list is empty
        {
            ResetClosestInteractable();
        }
    }
}
