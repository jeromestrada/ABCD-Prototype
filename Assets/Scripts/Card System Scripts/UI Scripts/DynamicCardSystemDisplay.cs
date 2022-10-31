using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DynamicCardSystemDisplay : CardSystemDisplay
{
    [SerializeField] protected CardSlot_UI slotPrefab;
    

    protected override void Start()
    {
        base.Start();
    }

    public void RefreshDynamicInventory(CardSystem sysToDisplay)
    {
        ClearSlots();
        cardSystem = sysToDisplay;
        AssignSlots(sysToDisplay);
    }

    public override void AssignSlots(CardSystem sysToDisplay)
    {
        slotDictionary = new Dictionary<CardSlot_UI, PlayerCardSlot>();

        if (sysToDisplay == null) return;

        for(int i = 0; i < sysToDisplay.CardSystemSize; i++)
        {
            var uiSlot = Instantiate(slotPrefab, transform);
            slotDictionary.Add(uiSlot, sysToDisplay.CardSlots[i]);
            uiSlot.Init(sysToDisplay.CardSlots[i]);
            // uiSlot.UpdateUISlot();
        }
    }

    public override void ClearSlots()
    {
        foreach (var item in transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }

        if(slotDictionary != null) slotDictionary.Clear();
    }
}
