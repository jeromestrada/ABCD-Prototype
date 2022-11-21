using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoonPhaseSystem : MonoBehaviour
{
    [SerializeField] private MoonPhase _startingPhase;
    [SerializeField] private Moon _moon;
    [SerializeField] private Light _light;
    [SerializeField] private float _transitionSpeed;
    private float startTime;
    private bool changeColor = false;
    private Color startColor;
    private Color endColor;

    public Moon Moon => _moon;
    public Light Light => _light;

    public static event System.Action<Moon> OnMoonPhaseChange;

    protected virtual void Awake()
    {
        _moon = new Moon(_startingPhase);
        startColor = Color.red;
        _light.color = startColor;
        ChangeLight();
        OnMoonPhaseChange?.Invoke(_moon);
    }

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            _moon.Transition();
            OnMoonPhaseChange?.Invoke(_moon);
            ChangeLight();
        }
        if (changeColor)
        {
            float t = Mathf.Sin((Time.time - startTime) * _transitionSpeed);
            LerpColor(startColor, endColor, t);
        }
    }

    private void ChangeLight()
    {
        changeColor = true;
        startColor = _light.color;
        startTime = Time.time;
        switch (_moon.CurrentMoon)
        {
            case MoonPhase.New:
                endColor = Color.red;
                break;
            case MoonPhase.WaxingCrescent:
                endColor = Color.grey;
                break;
            case MoonPhase.FirstHalf:
                endColor = Color.cyan;
                break;
            case MoonPhase.Full:
                endColor = Color.white;
                break;
            case MoonPhase.SecondHalf:
                endColor = Color.cyan;
                break;
            case MoonPhase.WaningCrescent:
                endColor = Color.grey;
                break;
            default:
                break;
        }
    }

    private void LerpColor(Color startColor, Color endColor, float time)
    {
        _light.color = Color.Lerp(startColor, endColor, time);
        if(_light.color == endColor) changeColor = false;
    }
}
