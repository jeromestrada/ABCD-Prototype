using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot_UI : Slot_UI
{
    [SerializeField] private Image _cardSprite;
    [SerializeField] private Text _cardName;
    [SerializeField] private ShopSlot _assignedShopSlot;
    [SerializeField] private TextMeshProUGUI _cardPrice;
    [SerializeField] private Button _buyButton;
    private bool isSold;

    public float MarkUp { get; private set; }
    public ShopSlot AssignedShopSlot => _assignedShopSlot;

    private void Awake()
    {
        isSold = false;
        ClearSlot();
        _buyButton?.onClick.AddListener(OnUISlotClick);
        SetParentDisplay();
    }
    public void Init(ShopSlot slot, float markUp)
    {
        _assignedShopSlot = slot;
        MarkUp = markUp;
        UpdateUISlot();
    }
    private void UpdateUISlot()
    {
        if(_assignedShopSlot.Card != null)
        {
            _cardSprite.sprite = _assignedShopSlot.Card.cardIcon;
            _cardSprite.color = Color.white;
            _cardName.text = _assignedShopSlot.Card.name.ToString();
            _cardPrice.text = _assignedShopSlot.Card.CardPrice.ToString();
        }
        else
        {
            ClearSlot();
        }
    }

    public void ClearSlot()
    {
        _cardSprite.sprite = null;
        _cardSprite.preserveAspect = true;
        _cardSprite.color = Color.clear;
        _cardName.text = "";
        _cardPrice.text = "";
    }
}
