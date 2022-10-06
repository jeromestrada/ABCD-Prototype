using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public SerializableDictionary<string, CardSystemHolderSaveData> deckDictionary;
    public SerializableDictionary<string, CardSystemHolderSaveData> handDictionary;

    public SaveData()
    {
        deckDictionary = new SerializableDictionary<string, CardSystemHolderSaveData>();
        handDictionary = new SerializableDictionary<string, CardSystemHolderSaveData>();
    } 
}
