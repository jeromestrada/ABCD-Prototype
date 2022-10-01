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
    public CardSlot AssignedCardSlot;
    private CardSlot_UI pickedFromSlot;
    public bool inUI;

    private void Awake()
    {
        ItemSprite.color = Color.clear;
        ItemCount.text = "";
    }
    public void UpdateMouseSlot(CardSlot cardSlot)
    {
        AssignedCardSlot.AssignItem(cardSlot); // system
        // UI
        ItemSprite.sprite = cardSlot.Card.cardIcon;
        ItemSprite.color = Color.white;
        ItemCount.text = cardSlot.NumOfUses.ToString();
        if (cardSlot.NumOfUses > 1) ItemCount.text = cardSlot.NumOfUses.ToString();
        else ItemCount.text = "";
        transform.SetAsLastSibling(); // makes sure that the mouse icon is drawn last
    }

    public void SavePickedFrom(CardSlot_UI _pickedFromSlot)
    {
        pickedFromSlot = _pickedFromSlot;
    }

    private void Update()
    {
        if (AssignedCardSlot.Card != null)
        {
            transform.position = Input.mousePosition;
            inUI = true;
            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObjects())
            {
                AssignedCardSlot.Card.Use();
                if (AssignedCardSlot.Card.NumOfUses <= 0) ClearSlot(); // once the card is used up, we should remove it from the mouse.
                else ReturnToSlot();
            }
            else if (Input.GetMouseButtonDown(1) && !IsPointerOverUIObjects())
            {
                ReturnToSlot();
            }
        }
        else if(Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) inUI = false; // only set inUI state to false when the mouse is released
    }

    public void ReturnToSlot() // returns the card to the slot
    {
        pickedFromSlot.AssignedInventorySlot.AssignItem(AssignedCardSlot);
        pickedFromSlot.UpdateUISlot();
        ClearSlot();
    }
    public void ClearSlot()
    {
        AssignedCardSlot.ClearSlot();
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
