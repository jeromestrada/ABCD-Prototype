using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSystemHolder : MonoBehaviour
{
    [SerializeField] private protected HeartSystem _heartSystem;
    [SerializeField] private int _maxHearts;
    [SerializeField] private int _startingHearts;

    public static event System.Action OnHeartsChanged;
    public static event System.Action<HeartSystem> OnHeartsDisplayRequested;

    public HeartSystem HeartSystem => _heartSystem;

    protected virtual void Awake()
    {
        _heartSystem = new HeartSystem(_maxHearts, _startingHearts);
        OnHeartsDisplayRequested?.Invoke(_heartSystem);
    }

    private void LoseHeart()
    {
        if(_heartSystem.CurrentHeartCount > 0)
        {
            _heartSystem.RemoveHeart();
            OnHeartsChanged?.Invoke();
            OnHeartsDisplayRequested?.Invoke(_heartSystem);
        }
    }

    private void GainHeart()
    {
        if (_heartSystem.CurrentHeartCount < _maxHearts)
        {
            _heartSystem.AddHeart();
            OnHeartsChanged?.Invoke();
            OnHeartsDisplayRequested?.Invoke(_heartSystem);
        }
    }

}
