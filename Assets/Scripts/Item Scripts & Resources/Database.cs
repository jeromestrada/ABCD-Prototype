using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

[CreateAssetMenu(menuName = "Card System/Card Database")]
public class Database : ScriptableObject
{
    [SerializeField] private List<Card> _cardDatabase;

    [ContextMenu("Set IDs")]
    public void SetCardIDs()
    {
        _cardDatabase = new List<Card>();

        var foundCards = Resources.LoadAll<Card>("CardData").OrderBy(c => c.ID).ToList();

        // filter through the found cards
        var hasIDInRange = foundCards.Where(c => c.ID != -1 && c.ID < foundCards.Count).OrderBy(c => c.ID).ToList();
        var hasIDNotInRange = foundCards.Where(c => c.ID != -1 && c.ID >= foundCards.Count).OrderBy(c => c.ID).ToList();
        var noID = foundCards.Where(c => c.ID <= -1).ToList();

        var index = 0;
        for(int i = 0; i < foundCards.Count; i++)
        {
            Card cardToAdd;
            cardToAdd = hasIDInRange.Find(d => d.ID == i);

            if(cardToAdd != null)
            {
                _cardDatabase.Add(cardToAdd);
            }
            else if(index < noID.Count)
            {
                noID[index].ID = i;
                cardToAdd = noID[index];
                index++;
                _cardDatabase.Add(cardToAdd);
            }
#if UNITY_EDITOR
            if (cardToAdd) EditorUtility.SetDirty(cardToAdd);
#endif
        }

        foreach (var card in hasIDNotInRange)
        {
#if UNITY_EDITOR
            if (card) EditorUtility.SetDirty(card);
#endif
            _cardDatabase.Add(card);
        }
#if UNITY_EDITOR
        AssetDatabase.SaveAssets();
#endif
    }

    public Card GetCard(int id)
    {
        return _cardDatabase.Find(i => i.ID == id);
    }
}
