using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UniqueID))]
public class DeckInventory : CardSystemHolder
{
    [SerializeField] List<Card> startingCards;
    [SerializeField] List<Weapon> weaponsInDeck;

    public event System.Action<Weapon> OnWeaponAdd;

    public List<Weapon> WeaponsInDeck => weaponsInDeck;

    protected override void Awake()
    {
        base.Awake();
        SaveLoad.OnLoadGame += LoadDeck;
    }

    private void LoadDeck(SaveData data)
    {
        // check the save data for this deck, if it exists load it in
        if(data.deckDictionary.TryGetValue(GetComponent<UniqueID>().ID, out DeckSaveData deckData))
        {
            cardSystem = deckData.cardSystem;
        }
    }

    public void AddWeapon(Weapon weaponToAdd)
    {
        if (weaponToAdd != null)
        {
            weaponsInDeck.Add(weaponToAdd);
            OnWeaponAdd?.Invoke(weaponToAdd);
        }
    }

    public void UpdateWeaponList()
    {
        foreach(CardSlot cSlot in cardSystem.CardSlots)
        {
            if(cSlot.Card.cardType == CardType.ItemCard)
            {
                var temp = (ItemCard)cSlot.Card;
                if(temp.item is Weapon) weaponsInDeck.Add((Weapon)temp.item);
            }
        }
    }

    private void Start()
    {
        // add the starting cards into the deck
        foreach (Card card in startingCards)
        {
            cardSystem.AddToCardSystem(card);
        }
        Debug.Log("Starting cards loaded");
        var deckSaveData = new DeckSaveData(cardSystem);
        Debug.Log(GetComponent<UniqueID>().ID);
        SaveGameManager.data.deckDictionary.Add(GetComponent<UniqueID>().ID, deckSaveData);

        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // open deck of cards
            OnDynamicCardSystemDisplayRequested?.Invoke(cardSystem);
        }
    }

}

[System.Serializable]
public struct DeckSaveData
{
    public CardSystem cardSystem;
    
    public DeckSaveData(CardSystem _cardSystem)
    {
        cardSystem = _cardSystem;
    }
}
