using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public float radius = 2f;
    public Transform interactionTransform; // so we can set a variable location when we want to

    bool isInRange = false;
    public bool hasInteracted = false;
    public Transform player;

    private SphereCollider myCollider;

    public UnityAction<IInteractable> OnInteractionComplete { get; set; }
    public string Prompt => _prompt;

    protected virtual void Awake()
    {
        myCollider = GetComponent<SphereCollider>();
        myCollider.radius = radius;
    }

    public virtual void Interact(InteractableScanner interactor, out bool interactSuccessful)
    {
        hasInteracted = true;
        interactSuccessful = true;
    }

    public virtual void EndInteraction()
    {

    }

    public void WhenInRange(Transform playerTransform) // readies the interactable by setting isInRange to true
    {
        if (!isInRange)
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
