using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSystem
{
    [SerializeField] private int _maxHeartCount; // max is for each run
    [SerializeField] private int _startingHeartCount; // starting could change but caps to max
    protected int _currentHeartCount;

    public int MaxHeartCount => _maxHeartCount;
    public int CurrentHeartCount => _currentHeartCount;

    public HeartSystem(int maxHeartCount, int startingHeartCount)
    {
        _maxHeartCount = maxHeartCount;
        _startingHeartCount = startingHeartCount;
        _currentHeartCount = _startingHeartCount;
    }

    public HeartSystem()
    {
        _maxHeartCount = 3;
        _startingHeartCount = 3;
        _currentHeartCount = _startingHeartCount;
    }

    public void AddHearts(int heartsToAdd)
    {
        for (int i = 0; i < heartsToAdd; i++)
            if (AddHeart() == false) break;
    }

    public bool AddHeart()
    {
        if(_currentHeartCount < _maxHeartCount)
        {
            _currentHeartCount++;
            return true;
        }
        return false;
    }

    public void RemoveHearts(int heartsToRemove)
    {
        for (int i = 0; i < heartsToRemove; i++)
            if (RemoveHeart() == false) break;
    }

    public bool RemoveHeart()
    {
        if (_currentHeartCount >= 0)
        {
            _currentHeartCount--;
            return true;
        }
        return false;
    }

}
