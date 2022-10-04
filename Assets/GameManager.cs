using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    CharacterAnimator characterAnimator;
    GameObject playerObj;
    private void Awake()
    {
        playerObj = GameObject.Find("Player");

        characterAnimator = playerObj.GetComponent<CharacterAnimator>();

        //UpdateAnimationSet();
    }

    /*public void UpdateAnimationSet()
    {
        if (characterAnimator != null)
        {
            foreach (Card c in deck.cards)
            {
                if(c.cardType == CardType.ItemCard)
                {

                }
            }
        }
    }*/
    // will manage the available weapon animations based on the weapons in the deck

    // will also manage deck shuffling and draw,
    // manages game state, is dungeon clear, should the main loot chest appear, etc...
    // handles pausing, restart and reload, maybe even save(will look into manipulating/recording a game state for this).
}
