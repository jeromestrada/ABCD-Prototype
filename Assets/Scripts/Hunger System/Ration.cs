using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ration", menuName = "Inventory/Ration")]
public class Ration : Item
{
    [SerializeField] private float _satiation;

    public float Satiation => _satiation;

    public static System.Action<Ration> OnRationUse;

    public override void Use()
    {
        base.Use();
        OnRationUse?.Invoke(this);
    }
}
