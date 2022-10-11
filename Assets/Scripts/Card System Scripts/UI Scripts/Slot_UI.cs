using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot_UI : MonoBehaviour
{
    public CardSystemDisplay ParentDisplay { get; private set; }

    protected void OnUISlotClick()
    {
        ParentDisplay?.SlotClicked(this);
    }
    protected void SetParentDisplay()
    {
        ParentDisplay = transform.parent.GetComponent<CardSystemDisplay>();
        if(ParentDisplay == null) ParentDisplay = transform.parent.parent.GetComponent<CardSystemDisplay>(); // check the parent's parent too
    }
}
