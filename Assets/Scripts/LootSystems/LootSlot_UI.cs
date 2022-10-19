using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LootSlot_UI : Slot_UI
{
    [SerializeField] private Image _cardSprite;
    [SerializeField] private Text _cardName;
    [SerializeField] private Button _cardButton;
    [SerializeField] private LootSlot _assignedLootSlot;

    public LootSlot AssignedLootSlot => _assignedLootSlot;

    private void Awake()
    {
        ClearSlot();
        _cardButton?.onClick.AddListener(OnUISlotClick);
        SetParentDisplay();
    }
    public void Update()
    {
        if (_assignedLootSlot.StackSize <= 0)
        {
            _cardSprite.color = Color.gray;
            _cardButton.onClick.RemoveAllListeners();
        }
    }
    public void Init(LootSlot slot)
    {
        _assignedLootSlot = slot;
        UpdateUISlot();
    }
    private void UpdateUISlot()
    {
        if (_assignedLootSlot.Card != null)
        {
            _cardSprite.sprite = _assignedLootSlot.Card.cardIcon;
            _cardSprite.color = Color.white;
            _cardName.text = _assignedLootSlot.Card.name.ToString();
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
    }
}
