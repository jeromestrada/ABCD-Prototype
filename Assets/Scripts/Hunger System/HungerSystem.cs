using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HungerSystem
{
    private float _maxSatiety;
    private float _currentSatiety;
    private float _metabolism;

    private HungerState _hungerStatus;

    public float CurrentSatiety => _currentSatiety;
    public HungerState HungerStatus => _hungerStatus;
    public float SatietyPercent => _currentSatiety / _maxSatiety;

    public HungerSystem(float maxSatiety, float metabolism)
    {
        _maxSatiety = maxSatiety;
        _currentSatiety = maxSatiety;

        _hungerStatus = HungerState.Full;
        _metabolism = metabolism;
    }

    public void Absorb(Ration _ration)
    {
        if(_ration != null)
        {
            _currentSatiety += _ration.Satiation;
            UpdateSatietyPercent();
        }
    }
    public void Digest()
    {
        if (_currentSatiety > 0f) _currentSatiety -= _metabolism;
        UpdateSatietyPercent();
    }

    public void UpdateSatietyPercent()
    {
        var _satietyPercentage = SatietyPercent; // caching calculation for slight optimization
        if (_satietyPercentage > 0.8f) _hungerStatus = HungerState.Full;
        else if (_satietyPercentage <= 0.5f && _satietyPercentage > 0f) _hungerStatus = HungerState.Hungry;
        else if (_satietyPercentage == 0f) _hungerStatus = HungerState.Starving;
    }
}

public enum HungerState { Starving, Hungry, Full }
