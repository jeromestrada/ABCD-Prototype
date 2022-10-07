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
    private CardSystemUIController cardSystemUIController;
    [SerializeField] private CardSystemHolder handOfCards;


    private void Start()
    {
        cardSystemUIController = GetComponentInParent<CardSystemUIController>();
    }

    private void Awake()
    {
        ItemSprite.color = Color.clear;
        ItemCount.text = "";
    }
    public void UpdateMouseSlot(CardSlot cardSlot)
    {
        AssignedCardSlot.AssignCard(cardSlot); // system slot assignment
        ItemSprite.sprite = cardSlot.Card.cardIcon; //following deals with UI
        ItemSprite.color = Color.white;
        ItemCount.text = cardSlot.RemainingUses.ToString();
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
            {// left click
                AssignedCardSlot.UseCardInSlot();
                if (AssignedCardSlot.RemainingUses > 0) ReturnToSlot();
                else
                {   // remove the slot from the card system, refresh the hand display and clear the mouse slot.
                    handOfCards.CardSystem.RemoveCardSlot(pickedFromSlot.AssignedInventorySlot);
                    cardSystemUIController.HandPanel.RefreshDynamicInventory(handOfCards.CardSystem);
                    ClearSlot();
                }
            }
            else if (Input.GetMouseButtonDown(1) && !IsPointerOverUIObjects())
            {// right click
                ReturnToSlot();
            }
        }
        else if(Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) inUI = false; // only set inUI state to false when the mouse is released
    }

    public void ReturnToSlot() // returns the card to the slot
    {
        pickedFromSlot.AssignedInventorySlot.AssignCard(AssignedCardSlot);
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
