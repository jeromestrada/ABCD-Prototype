using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Loot System/Loot Generator")]
public class LootGenerator : ScriptableObject // this can be hooked into some enemies so they can drop loot, 
{
    [SerializeField] private List<Card> cardPool; // holds what cards can be generated for the loot generation
    [SerializeField] private int totalWeight;
    [SerializeField] private int randomNumber;

    [Header("Loot Rarity Distribution Table")] //TODO: refactor loot generation to follow a rarity system instead of individual cards
    [SerializeField] private int[] table; // using a weight table for the drop rate distribution

    private void Awake()
    {
        ResetTable();
    }

    [ContextMenu("Reset table values")]
    private void ResetTable()
    {
        table = new int[cardPool.Count];
        int tempTotal = table.Length * 10;
        for (int i = 0; i < cardPool.Count; i++) // this sets up a decreasing weight distribution for the table.
        {
            table[i] = tempTotal;
            tempTotal -= 10;
        }
        CalculateTotalWeight();
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
            {   // if the random number happens to be smaller than this weight, return the corresponding card
                return cardPool[i];
            }
            else
            {   // subtract the weight from the random number so we can check for the next weight in the distribution table
                randomNumber -= table[i];
            }
            card = cardPool[i];
        }
        return card;
    }
}
