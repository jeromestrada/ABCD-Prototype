using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CardSystemHolder : MonoBehaviour
{
    [SerializeField] private int cardSystemSize;
    [SerializeField] protected CardSystem cardSystem;

    public CardSystem CardSystem => cardSystem;

    public static UnityAction<CardSystem> OnDynamicCardSystemDisplayRequested;

    protected virtual void Awake()
    {
        cardSystem = new CardSystem(cardSystemSize);
    }
}

[System.Serializable]
public struct CardSystemHolderSaveData
{
    public CardSystem cardSystem;

    public CardSystemHolderSaveData(CardSystem _cardSystem)
    {
        cardSystem = _cardSystem;
    }
}