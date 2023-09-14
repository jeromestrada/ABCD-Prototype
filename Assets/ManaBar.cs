using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public PlayerStats playerStats;
    public Image[] manaPoints;
    private void OnEnable()
    {
        playerStats.GetComponent<CharacterStats>().OnManaChanged += OnManaChanged;
    }

    private void OnDisable()
    {
        playerStats.GetComponent<CharacterStats>().OnManaChanged -= OnManaChanged;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnManaChanged(int currentMana, int maxMana)
    {
        for (int i = 0; i < manaPoints.Length; i++)
        {
            manaPoints[i].enabled = i < currentMana ? true : false;
        }
    }
}
