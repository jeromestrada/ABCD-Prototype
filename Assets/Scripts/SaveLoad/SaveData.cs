using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public SerializableDictionary<string, CardSystemHolderSaveData> deckDictionary;

    public SaveData()
    {
        deckDictionary = new SerializableDictionary<string, CardSystemHolderSaveData>();
    } 
}
