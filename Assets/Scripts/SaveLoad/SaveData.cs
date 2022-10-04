using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public SerializableDictionary<string, DeckSaveData> deckDictionary;

    public SaveData()
    {
        deckDictionary = new SerializableDictionary<string, DeckSaveData>();
    } 
}
