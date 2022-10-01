using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCardSystemDisplay : CardSystemDisplay
{
    [SerializeField] private CardSystemHolder cardSysytemHolder;
    [SerializeField] private CardSlot_UI[] slotsUI;
    [SerializeField] private CardSystemDisplayType displayType;

    protected override void Start()
    {
        base.Start();
        if(cardSysytemHolder != null)
        {
            cardSystem = cardSysytemHolder.CardSystem;
            cardSystem.OnInventorySlotChanged += UpdateSlot;
        }
        else
        {
            Debug.LogWarning($"No inventory assigned to {this.gameObject}");
        }
        AssignSlots(cardSystem);
        AssignInventoryType(displayType); // we only assign the type once by using the provided display type from the inspector
    }
    public override void AssignSlots(CardSystem invToDisplay) // ties the UI slot and the System slot together in a Dictionary<UI,System>
    {
        slotDictionary = new Dictionary<CardSlot_UI, CardSlot>();

        if (slotsUI.Length != cardSystem.CardSystemSize) Debug.Log($"Inventory UI slots out of sync on {this.gameObject} --- Length: {slotsUI.Length} SystemSize: {cardSystem.CardSystemSize}");

        for(int i = 0; i < cardSystem.CardSystemSize; i++)
        {
            slotDictionary.Add(slotsUI[i], cardSystem.CardSlots[i]); // adds the key-value in the dictionary
            slotsUI[i].Init(cardSystem.CardSlots[i]); // Init will assign the system slot to the UI slot
        }
    }
}
