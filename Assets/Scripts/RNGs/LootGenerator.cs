using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Loot System/Loot Generator")]
public class LootGenerator : ScriptableObject
{
    [SerializeField] private List<Card> cardPool;
    [SerializeField] private int totalWeight;
    [SerializeField] private int randomNumber;

    [Header("Loot Rarity Distribution Table")]
    [SerializeField] private int[] table;

    private void Awake()
    {
        CalculateTotalWeight();
    }

    [ContextMenu("Reset table values")]
    private void SetUpTable()
    {
        table = new int[cardPool.Count];
        //Debug.LogWarning("Make sure to set the weights of the Loot Generator!");
    }

    [ContextMenu("Calculate Total Weight")]
    private void CalculateTotalWeight()
    {
        totalWeight = 0;
        foreach (var weight in table)
        {
            totalWeight += weight;
        }
    }

    public Card GenerateLoot()
    {
        randomNumber = Random.Range(0, totalWeight);
        Card card = null;
        for (int i = 0; i <= table.Length; i++)
        {
            if (randomNumber <= table[i])
            {
                return cardPool[i];
            }
            else
            {
                randomNumber -= table[i];
            }
            card = cardPool[i];
        }
        return card;
    }
}
