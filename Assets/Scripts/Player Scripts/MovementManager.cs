using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementManager : MonoBehaviour
{
    [SerializeField] PlayerMovement manualMovement;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] CharacterController controller;
    public float duration;

    public static event System.Action<float> OnGateInteraction;

    private void Awake()
    {
        if(manualMovement == null) manualMovement = GetComponentInParent<PlayerMovement>();
        if(navMeshAgent == null) navMeshAgent = GetComponentInParent<NavMeshAgent>();
        if(controller == null) controller = GetComponentInParent<CharacterController>();
        navMeshAgent.enabled = false;
    }

    private void OnEnable()
    {
        GatePlatform.OnPass += Switch;
    }

    private void OnDisable()
    {
        GatePlatform.OnPass -= Switch;
    }

    void Switch(GatePlatform gate)
    {
        Debug.Log("Switching to navmesh agent mode...");
        manualMovement.speed = 0;
        manualMovement.enabled = false;
        controller.enabled = false;
        navMeshAgent.enabled = true;
        OnGateInteraction?.Invoke(duration);
        navMeshAgent.SetDestination(gate.interactionTransform.position);
        Invoke(nameof(SwitchBack), duration);
    }

    void SwitchBack()
    {
        manualMovement.enabled = true;
        controller.enabled = true;
        navMeshAgent.SetDestination(transform.position);
        navMeshAgent.enabled = false;
        Debug.Log("Switched back to manual!");
    }
}
