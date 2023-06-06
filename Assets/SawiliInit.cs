using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawiliInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Initialize Sawili")]
    private void InitializeObject()
    {
        gameObject.AddComponent<CharacterController>();
        gameObject.AddComponent<PlayerMovement>();
        
        gameObject.AddComponent<AnimationEventReciever>();
        gameObject.AddComponent<InteractableScanner>();
    }
}
