using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PlayerStats playerStats;
    public Image[] healthPoints;
    private void OnEnable()
    {
        playerStats.GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        playerStats.GetComponent<CharacterStats>().OnHealthChanged -= OnHealthChanged;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHealthChanged(int currentHealth, int maxHealth)
    {
        for(int i = 0; i < healthPoints.Length; i++)
        {
            healthPoints[i].enabled = i < currentHealth ? true : false;
        }
    }
}
