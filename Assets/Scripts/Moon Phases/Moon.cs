using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon
{
    private MoonPhase _currentMoon;

    public MoonPhase CurrentMoon => _currentMoon;
    

    public Moon()
    {
        _currentMoon = MoonPhase.New;
    }
    public Moon(MoonPhase startingMoon)
    {
        _currentMoon = startingMoon;
    }

    public void Transition()
    {
        switch( _currentMoon)
        {
            case MoonPhase.New:
                _currentMoon = MoonPhase.WaxingCrescent;
                break;
            case MoonPhase.WaxingCrescent:
                _currentMoon = MoonPhase.FirstHalf;
                break;
            case MoonPhase.FirstHalf:
                _currentMoon = MoonPhase.Full;
                break;
            case MoonPhase.Full:
                _currentMoon = MoonPhase.SecondHalf;
                break;
            case MoonPhase.SecondHalf:
                _currentMoon = MoonPhase.WaningCrescent;
                break;
            case MoonPhase.WaningCrescent:
                _currentMoon = MoonPhase.New;
                break;
            default:
                break;
        }
    }

    public void SetPhase(MoonPhase phase)
    {
        _currentMoon = phase;
    }
}

public enum MoonPhase { New, WaxingCrescent, FirstHalf, Full, SecondHalf, WaningCrescent }
