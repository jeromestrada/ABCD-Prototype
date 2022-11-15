using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSystemUIController : MonoBehaviour
{
    public HeartSystemDisplay HeartsPanel;


    private void Awake()
    {
        HeartsPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        HeartSystemHolder.OnHeartsDisplayRequested += DisplayHearts;
    }

    private void OnDisable()
    {
        HeartSystemHolder.OnHeartsDisplayRequested -= DisplayHearts;
    }

    private void DisplayHearts(HeartSystem obj)
    {
        HeartsPanel.gameObject.SetActive(true);
    }
}
