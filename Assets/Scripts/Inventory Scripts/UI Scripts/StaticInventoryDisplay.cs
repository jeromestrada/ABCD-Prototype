using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private InventorySlot_UI[] slotsUI;
    [SerializeField] private InventoryDisplayType displayType;

    protected override void Start()
    {
        base.Start();
        if(inventoryHolder != null)
        {
            inventorySystem = inventoryHolder.InventorySystem;
            inventorySystem.OnInventorySlotChanged += UpdateSlot;
        }
        else
        {
            Debug.LogWarning($"No inventory assigned to {this.gameObject}");
        }
        AssignSlots(inventorySystem);
        AssignInventoryType(displayType); // we only assign the type once by using the provided display type from the inspector
    }
    public override void AssignSlots(InventorySystem invToDisplay) // ties the UI slot and the System slot together in a Dictionary<UI,System>
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        if (slotsUI.Length != inventorySystem.InventorySize) Debug.Log($"Inventory UI slots out of sync on {this.gameObject}");

        for(int i = 0; i < inventorySystem.InventorySize; i++)
        {
            slotDictionary.Add(slotsUI[i], inventorySystem.InventorySlots[i]); // adds the key-value in the dictionary
            slotsUI[i].Init(inventorySystem.InventorySlots[i]); // Init will assign the system slot to the UI slot
        }
    }
}
