using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotUI : MonoBehaviour
{
    [SerializeField] private Image _cardSprite;
    [SerializeField] private Text _cardName;
    [SerializeField] private ShopSlot _assignedShopSlot;
    [SerializeField] private TextMeshProUGUI _cardPrice;
    [SerializeField] private Button _buyButton;

    public ShopKeeperDisplay ParentDisplay { get; private set; }
    public float MarkUp { get; private set; }

    private void Awake()
    {
        _cardSprite.sprite = null;
        _cardSprite.preserveAspect = true;
        _cardSprite.color = Color.clear;
        _cardName.text = "";

        _cardPrice.text = "";
        _buyButton?.onClick.AddListener(OpenConfirmWindow);
        ParentDisplay = transform.parent.GetComponent<ShopKeeperDisplay>();
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
            _cardSprite.sprite = null;
            _cardSprite.preserveAspect = true;
            _cardSprite.color = Color.clear;
            _cardName.text = "";

            _cardPrice.text = "";
        }
    }

    private void OpenConfirmWindow()
    {
        // Open the purchase confirm window.
        Debug.Log($"Opening up confirmation window for {_cardName.text}");
    }

    
}
