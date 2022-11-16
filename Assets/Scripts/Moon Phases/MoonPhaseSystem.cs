using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoonPhaseSystem : MonoBehaviour
{
    [SerializeField] private Moon _moon;
    [SerializeField] private Light _light;
    [SerializeField] private float _transitionSpeed;
    private float startTime;
    private bool changeColor = false;
    private Color startColor;
    private Color endColor;

    public Moon Moon => _moon;
    public Light Light => _light;

    protected virtual void Awake()
    {
        _moon = new Moon();
        startColor = Color.red;
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
            ChangeLight();
        }
        if (changeColor)
        {
            float t = Mathf.Sin(Time.time - startTime * _transitionSpeed);
            LerpColor(startColor, endColor, t);
        }
    }

    private void ChangeLight()
    {
        changeColor = true;
        startColor = _light.color;
        
        switch (_moon.CurrentMoon)
        {
            case MoonPhase.New:
                endColor = Color.red;
                break;
            case MoonPhase.WaxingCrescent:
                endColor = Color.magenta;
                break;
            case MoonPhase.FirstHalf:
                endColor = Color.grey;
                break;
            case MoonPhase.Full:
                endColor = Color.white;
                break;
            case MoonPhase.SecondHalf:
                endColor = Color.grey;
                break;
            case MoonPhase.WaningCrescent:
                endColor = Color.magenta;
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
