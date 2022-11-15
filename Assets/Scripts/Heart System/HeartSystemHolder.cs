using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSystemHolder : MonoBehaviour
{
    [SerializeField] private protected HeartSystem _heartSystem;
    [SerializeField] private int _maxHearts;
    [SerializeField] private int _startingHearts;

    public static event System.Action<int> OnHeartsChanged;
    public static event System.Action<HeartSystem> OnHeartsDisplayRequested;

    public HeartSystem HeartSystem => _heartSystem;

    private void OnEnable()
    {
        PlayerStats.OnPlayerDying += LoseHeart;
    }

    private void OnDisable()
    {
        PlayerStats.OnPlayerDying -= LoseHeart;
    }

    protected virtual void Awake()
    {
        _heartSystem = new HeartSystem(_maxHearts, _startingHearts);
        OnHeartsDisplayRequested?.Invoke(_heartSystem);
    }

    private void LoseHeart()
    {
        if(_heartSystem.CurrentHeartCount >= 0)
        {
            _heartSystem.RemoveHeart();
            OnHeartsDisplayRequested?.Invoke(_heartSystem);
        }
        OnHeartsChanged?.Invoke(_heartSystem.CurrentHeartCount);
    }

    private void GainHeart()
    {
        if (_heartSystem.CurrentHeartCount < _maxHearts)
        {
            _heartSystem.AddHeart();
            OnHeartsDisplayRequested?.Invoke(_heartSystem);
        }
        OnHeartsChanged?.Invoke(_heartSystem.CurrentHeartCount);
    }

}
