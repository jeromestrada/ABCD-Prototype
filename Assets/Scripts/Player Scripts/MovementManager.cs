using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementManager : MonoBehaviour
{
    [SerializeField] PlayerMovement manualMovement;
    [SerializeField] NavMeshAgent navMeshAgent;
    public float duration;

    private void Awake()
    {
        manualMovement = GetComponent<PlayerMovement>();
        navMeshAgent = GetComponent<NavMeshAgent>();
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
        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(gate.interactionTransform.position);
        Invoke(nameof(SwitchBack), duration);
    }

    void SwitchBack()
    {
        manualMovement.enabled = true;
        navMeshAgent.SetDestination(transform.position);
        navMeshAgent.enabled = false;
        Debug.Log("Switched back to manual!");
    }
}
