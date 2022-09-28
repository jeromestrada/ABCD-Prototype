using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MouseItemData : MonoBehaviour
{
    public Image ItemSprite;
    public TextMeshProUGUI ItemCount;
    public InventorySlot AssignedInventorySlot;
    private InventorySlot_UI pickedFromSlot;

    private void Awake()
    {
        ItemSprite.color = Color.clear;
        ItemCount.text = "";
    }
    public void UpdateMouseSlot(InventorySlot invSlot)
    {
        AssignedInventorySlot.AssignItem(invSlot); // system
        // UI
        ItemSprite.sprite = invSlot.Card.cardIcon;
        ItemSprite.color = Color.white;
        ItemCount.text = invSlot.StackSize.ToString();
        if (invSlot.StackSize > 1) ItemCount.text = invSlot.StackSize.ToString();
        else ItemCount.text = "";
        transform.SetAsLastSibling(); // makes sure that the mouse icon is drawn last
    }

    public void SavePickedFrom(InventorySlot_UI _pickedFromSlot)
    {
        pickedFromSlot = _pickedFromSlot;
    }

    private void Update()
    {
        if (AssignedInventorySlot.Card != null)
        {
            transform.position = Input.mousePosition;

            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObjects())
            {
                Debug.Log("Using card in mouse");
                AssignedInventorySlot.Card.Use();
                if(AssignedInventorySlot.Card.numOfUses <= 0) ClearSlot(); // once the card is used up, we should remove it from the mouse.
                else ReturnToSlot();
            }
            else if (Input.GetMouseButtonDown(1) && !IsPointerOverUIObjects())
            {
                ReturnToSlot();
            }
        }
    }

    public void ReturnToSlot() // returns the card to the slot
    {
        pickedFromSlot.AssignedInventorySlot.AssignItem(AssignedInventorySlot);
        pickedFromSlot.UpdateUISlot();
        ClearSlot();
    }
    public void ClearSlot()
    {
        AssignedInventorySlot.ClearSlot();
        ItemCount.text = "";
        ItemSprite.color = Color.clear;
        ItemSprite.sprite = null;
        pickedFromSlot = null;
    }

    public static bool IsPointerOverUIObjects()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
