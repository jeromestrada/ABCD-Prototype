using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableScanner : MonoBehaviour
{

    public float interactionRange = 3f;
    public LayerMask interactableMask;
    public Interactable closestInteractable = null;
    float interactableScanInterval = 0.5f;
    bool alreadyScanned = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // can have a state that dictates if we scan or not
        // or what type of interactable to look out for.
        ScanForInteractables(interactableMask);
    }

    public void ScanForInteractables(LayerMask interactableTypeMask)
    {
        if (!alreadyScanned)
        {
            Collider[] detected = Physics.OverlapSphere(transform.position, interactionRange, interactableTypeMask);
            if (detected.Length == 0) // null the closest when nothing is detected
            {
                closestInteractable = null;
                return;
            }
            float closestDist = float.MaxValue;
            foreach (Collider c in detected) // finds the closest interactable detected
            {
                Interactable temp = c.GetComponent<Interactable>();
                float distance = Vector3.Distance(temp.interactionTransform.position, transform.position);
                if (distance < closestDist)
                {
                    closestDist = distance;
                    closestInteractable = temp;
                }
                temp.WhenInRange(transform); // we're in range, so we activate the interactable.
            }
            alreadyScanned = true;
            Invoke(nameof(ResetScan), interactableScanInterval); // reset scan for interactables after given seconds lapsed.
        }
    }

    private void ResetScan()
    {
        alreadyScanned = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
