using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Protection", menuName = "Inventory/Protection")]
public class Protection : Item
{
    [SerializeField] private int _armor;

    public int Armor => Armor; 
    private void Awake()
    {
        _itemType = ItemType.Protection;
    }
}
