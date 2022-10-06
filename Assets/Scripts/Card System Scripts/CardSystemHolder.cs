using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CardSystemHolder : MonoBehaviour
{
    [SerializeField] protected int _cardSystemSize;
    [SerializeField] protected CardSystem _cardSystem;

    public CardSystem CardSystem => _cardSystem;

    public static UnityAction<CardSystem> OnDynamicCardSystemDisplayRequested;

    protected virtual void Awake()
    {
        _cardSystem = new CardSystem(_cardSystemSize);
    }

    protected virtual void Update()
    {
        _cardSystemSize = _cardSystem.CardSystemSize;
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