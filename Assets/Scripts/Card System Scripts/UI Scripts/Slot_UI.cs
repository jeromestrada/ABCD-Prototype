using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot_UI : MonoBehaviour
{
    public CardSystemDisplay ParentDisplay { get; private set; }

    private void Awake()
    {
        ParentDisplay = transform.parent.GetComponent<CardSystemDisplay>();
    }

    protected void OnUISlotClick()
    {
        ParentDisplay?.SlotClicked(this);
    }
}
