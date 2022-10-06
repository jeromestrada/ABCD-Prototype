using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandOfCards : CardSystemHolder
{
    [SerializeField] private int maxHandSize;
    [SerializeField] private DeckOfCards deck;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (Input.GetKey(KeyCode.X))
        {
            // Deal a card to the hand
        }
    }

    public void DrawCard(int amountToDraw)
    {

    }
}
