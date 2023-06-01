using System.Collections.Generic;
using UnityEngine;


//[RequireComponent(typeof(CharacterController))]
public class InteractableScanner : MonoBehaviour
{
    [SerializeField] private float scannerRadius;
    [SerializeField] InteractionPromptUI _promptUI;
    [SerializeField] CharacterController characterController;
    public Interactable closestInteractable = null;
    [SerializeField] private float interactableScanInterval = 0.2f;
    bool alreadyScanned = false;



    public bool IsInteracting { get; private set; }

    private List<Interactable> interactables;

    private void Awake()
    {
        if(characterController == null) characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        characterController = GetComponentInParent<CharacterController>();
        characterController.radius = scannerRadius;
        interactables = new List<Interactable>();
    }

    void Update() 
    {
        if(interactables.Count > 0) // scanning for closest when there's atleast 1 interactable
        {
            ScanForClosestInteractable();
            if (closestInteractable != null)
            {
                if (!_promptUI.isDisplayed) _promptUI.SetUp(closestInteractable.Prompt);

                if (Input.GetKeyDown(KeyCode.E)) StartInteraction(closestInteractable);
            }
        }
        else
        {
            if (closestInteractable != null) closestInteractable = null;
            if(_promptUI.isDisplayed) _promptUI.Close();
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
            // find the closest interactable detected
            foreach (Interactable i in interactables) 
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
                    _promptUI.SetUp(closestInteractable.Prompt);
                }
                i.WhenInRange(transform); // we're in range if we haven't exited the trigger, so we ready the interactable.
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
            interactableObj.WhenNotInRange(); // reset the interactables state when it exits the players scanner radius
            interactables.Remove(interactableObj);
        }
        if (interactables.Count == 0) // null the closest if the interactables list is empty
        {
            ResetClosestInteractable();
        }
    }
}
