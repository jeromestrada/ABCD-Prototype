using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public SerializableDictionary<string, CardSystemHolderSaveData> viewOnlyDeckDictionary;
    public SerializableDictionary<string, CardSystemHolderSaveData> deckDictionary;
    public SerializableDictionary<string, CardSystemHolderSaveData> handDictionary;
    public SerializableDictionary<string, CardSystemHolderSaveData> discardPileDictionary;

    public SaveData()
    {
        viewOnlyDeckDictionary = new SerializableDictionary<string,CardSystemHolderSaveData>();
        deckDictionary = new SerializableDictionary<string, CardSystemHolderSaveData>();
        handDictionary = new SerializableDictionary<string, CardSystemHolderSaveData>();
        discardPileDictionary = new SerializableDictionary<string, CardSystemHolderSaveData>();
    } 
}
