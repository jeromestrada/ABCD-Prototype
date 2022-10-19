using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LootSystemDisplay : CardSystemDisplay
{
    [SerializeField] private LootSlot_UI _lootSlotPrefab;
    [SerializeField] private Button _closeButton;

    [SerializeField] private GameObject _lootOptionsWindow;

    private LootSystem _lootSystem;
    private DeckOfCards _playerDeck;
    private Dictionary<LootSlot, int> _lootList = new Dictionary<LootSlot, int>();
    private Dictionary<LootSlot, LootSlot_UI> _lootListUI = new Dictionary<LootSlot, LootSlot_UI>();

    public static UnityAction<LootSlot_UI> OnLootConfirmWindowRequested;

    public override void AssignSlots(CardSystem invToDisplay)
    {
        
    }

    private void RefreshLootDisplay()
    {
        ClearSlots();
        DisplayLootInventory();
    }
    public override void ClearSlots()
    {
        _lootList = new Dictionary<LootSlot, int>();
        _lootListUI = new Dictionary<LootSlot, LootSlot_UI>();
        foreach (var card in _lootOptionsWindow.transform.Cast<Transform>())
        {
            Destroy(card.gameObject);
        }
    }

    public void DisplayLootConfirmWindow(LootSlot_UI shopSlot)
    {
        // update the buy confirm window's sprite and texts here
        OnLootConfirmWindowRequested?.Invoke(shopSlot);
    }

    public void DisplayLootWindow(LootSystem lootSystem, DeckOfCards deck)
    {
        _lootSystem = lootSystem;
        _playerDeck = deck;
        RefreshLootDisplay();
    }
    public void DisplayLootInventory()
    {
        foreach (var lootSlot in _lootSystem.LootInventory)
        {
            if (lootSlot.Card == null) continue;

            var slot = Instantiate(_lootSlotPrefab, _lootOptionsWindow.transform);
            slot.Init(lootSlot);
        }
    }

    public void CloseLootWindow()
    {
        gameObject.SetActive(false);
    }
}
