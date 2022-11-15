using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungrySystemHolder : MonoBehaviour
{
    [SerializeField] private HungerSystem _hungerSystem;
    [SerializeField] private float _satietyLimit;
    [SerializeField] private float _metabolism;

    [SerializeField] private List<Ration> _rations;
    [SerializeField] private int _maxRations;
    [SerializeField] HungerState _state;
    [SerializeField] float _satiety;

    private int _digestionInterval = 5;
    private bool digesting;

    protected virtual void Awake()
    {
        _hungerSystem = new HungerSystem(_satietyLimit, _metabolism);
        if(_rations == null) _rations = new List<Ration>();
        digesting = false;
    }

    private void Update()
    {
        if (!digesting)
        {
            Invoke(nameof(Digest), _digestionInterval);
            digesting = true;
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("Eating...");
            Eat();
        }
        _state = _hungerSystem.HungerStatus;
        _satiety = _hungerSystem.CurrentSatiety;
    }

    public void StoreRations(Ration ration)
    {
        if (_rations.Count < _maxRations) _rations.Add(ration);
    }

    public void Eat()
    {
        if(_rations.Count > 0)
        {
            _hungerSystem.Absorb(_rations[0]);
            _rations.RemoveAt(0);
        }
    }

    private void Digest()
    {
        _hungerSystem.Digest();
        digesting = false;
    }
}
