using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The bubble defines an area where the enemy slows down the player's mana regeneration.
 * This forces the player to use hit and run tactics, employ ranged attacks, 
 * or utilize mechanics that benefit from being within an engagement bubble
 * 
 * */
public class EngagementBubble : MonoBehaviour
{

    [SerializeField] private float radius;
    [SerializeField] private float potency; // defines how many seconds it adds to the regen rate of the player's mana
    private SphereCollider sphereCollider;

    public static event System.Action<float> OnEngage;
    // Start is called before the first frame update
    void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = radius;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnEngage?.Invoke(potency);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnEngage?.Invoke(-potency);
        }
    }
}
