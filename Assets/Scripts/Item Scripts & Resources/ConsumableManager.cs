using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableManager : MonoBehaviour
{

    public static event System.Action<Consumable> OnConsumableHandled;
    private void OnEnable()
    {
        Consumable.OnConsumableUse += Consume;
    }
    private void OnDisable()
    {
        Consumable.OnConsumableUse -= Consume;
    }

    public void Consume(Consumable consumable)
    {
        Debug.Log("In Consume");
        if (consumable == null) return;
        Debug.Log("Consumable isn't null");
        // mesh, particles, etc handling to be done here.
        // then we can trigger the actual consumable afterwards.
        OnConsumableHandled?.Invoke(consumable);
    }
}
