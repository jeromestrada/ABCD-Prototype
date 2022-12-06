using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerSystemUI : MonoBehaviour
{
    [SerializeField] private HungerSystemHolder _hungerSystemHolder;
    [SerializeField] private Image _hungerMeter;

    private void FixedUpdate()
    {
        _hungerMeter.fillAmount = _hungerSystemHolder.HungerSystem.SatietyPercent;
    }
}
