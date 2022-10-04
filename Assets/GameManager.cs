using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // TODO: transfer logic that doesn't necessarily get managed by an object alone.
    CharacterAnimator characterAnimator;
    GameObject playerObj;
    private void Awake()
    {
        playerObj = GameObject.Find("Player");

        characterAnimator = playerObj.GetComponent<CharacterAnimator>();
    }
}
