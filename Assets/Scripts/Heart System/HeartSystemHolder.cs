using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSystemHolder : MonoBehaviour
{
    [SerializeField] private protected HeartSystem _heartSystem;
    [SerializeField] private int _maxHearts;
    [SerializeField] private int _startingHearts;

    public HeartSystem HeartSystem => _heartSystem;

    protected virtual void Awake()
    {
        _heartSystem = new HeartSystem(_maxHearts, _startingHearts);
    }

}
