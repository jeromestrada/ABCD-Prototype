using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DynamicCardSystemDisplay : CardSystemDisplay
{
    [SerializeField] protected CardSlot_UI slotPrefab;

    private KeyCode[] hotKeys = new KeyCode[] // default hot keys for the hand are the numbers on top of the keyboard
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9,
        KeyCode.Alpha0
    };

    private int selectedHotKeyIndex;
    private int previousHotKeyPressed;

    protected override void Start()
    {
        previousHotKeyPressed = -1;
        base.Start();
    }

    private void Update()
    {
        HandleHotKeys();
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

    public void HandleHotKeys()
    {
        if (CardSystemDisplayType == CardSystemDisplayType.HandInventory)
        {
            if(CardSystem.CardSystemSize > 0)
            {
                for (int i = 0; i < hotKeys.Length; i++)
                {
                    if (Input.GetKeyDown(hotKeys[i]))
                    {
                        Debug.Log($"Selecting {hotKeys[i]} in hand");
                        selectedHotKeyIndex = i;
                        if (previousHotKeyPressed == selectedHotKeyIndex) // if a hotkey is pressed twice in a row, use the item.
                        {
                            var cardSlot = cardSystem.CardSlots[i];
                            Debug.Log($"Confirmed use of {hotKeys[i]} in hand");
                            cardSlot.UseCardInSlot();
                            RefreshDynamicInventory(cardSystem);
                        }
                        previousHotKeyPressed = selectedHotKeyIndex;
                    }
                }
            } 
        }
    }
}
