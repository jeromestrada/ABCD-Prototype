using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MoonPhaseDisplay : MonoBehaviour
{
    public TextMeshProUGUI moonPhaseText;

    private void OnEnable()
    {
        MoonPhaseSystem.OnMoonPhaseChange += UpdateMoonPhaseText;
    }

    private void OnDisable()
    {
        MoonPhaseSystem.OnMoonPhaseChange -= UpdateMoonPhaseText;
    }

    private void UpdateMoonPhaseText(Moon moon)
    {
        moonPhaseText.text = moon.CurrentMoon.ToString();
    }
}
