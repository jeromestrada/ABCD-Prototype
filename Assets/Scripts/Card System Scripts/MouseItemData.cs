using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MouseItemData : MonoBehaviour
{
    public Image ItemSprite;
    public PlayerCardSlot AssignedCardSlot;
    private CardSlot_UI pickedFromSlot;
    public static bool inUI;
    private CardSystemUIController cardSystemUIController;
    [SerializeField] private CardSystemHolder handOfCards;
    [SerializeField] private DiscardedCards discardPile;


    private void Start()
    {
        cardSystemUIController = GetComponentInParent<CardSystemUIController>();
    }

    private void Awake()
    {
        ItemSprite.color = Color.clear;
    }
    public void UpdateMouseSlot(PlayerCardSlot cardSlot)
    {
        AssignedCardSlot.AssignCard(cardSlot); // system slot assignment
        ItemSprite.sprite = cardSlot.Card.cardIcon; //following deals with UI
        ItemSprite.color = Color.white;
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
                var tempCard = AssignedCardSlot.Card;
                AssignedCardSlot.UseCardInSlot();
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
