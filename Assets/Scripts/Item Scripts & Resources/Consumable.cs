using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]
public class Consumable : Item
{
    [SerializeField] private int _healAmount;

    public int HealAmount => _healAmount;

    public ConsumableType Type;
    public static System.Action<Consumable> OnConsumableUse;

    public override void Use()
    {
        Debug.Log($"Using {this.name}");
        OnConsumableUse?.Invoke(this);
        base.Use();
    }
}

public enum ConsumableType { Healing, Utility}
