using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LootSystem
{
    [SerializeField] private List<LootSlot> _lootInventory;
    public List<LootSlot> LootInventory => _lootInventory;
    
    public LootSystem(int size)
    {
        SetLootSize(size);
    }


    private void SetLootSize(int size)
    {
        _lootInventory = new List<LootSlot>();
        for (int i = 0; i < size; i++)
        {
            _lootInventory.Add(new LootSlot());
        }
    }

    public void AddToLoot(Card cardToAdd)
    { // a loot box will have separate slots for each instance of a card(loot)
        var freeSlot = GetFreeSlot();
        freeSlot.AssignCard(cardToAdd, 1);
    }

    public bool TakeFromLoot(LootSlot lootSlot, DeckOfCards deck)
    {
        if (lootSlot.StackSize <= 0)
        {
            Debug.Log($"{lootSlot.Card.name} is out of stock!");
            return false;
        }
        deck.AddCardToDeck(lootSlot.Card);
        lootSlot.RemoveFromStack(1);
        return true;
    }

    private LootSlot GetFreeSlot()
    {
        var freeSlot = _lootInventory.FirstOrDefault(i => i.Card == null);
        if (freeSlot == null)
        {
            freeSlot = new LootSlot();
            _lootInventory.Add(freeSlot);
        }
        return freeSlot;
    }
    // keeping this for checking if the loot box already contains a certain card
    public bool ContainsCard(Card cardToAdd, out LootSlot lootSlot)
    {
        lootSlot = _lootInventory.Find(i => i.Card == cardToAdd);
        return lootSlot != null;
    }
}
