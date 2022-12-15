using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Modifier
{
    [SerializeField] private string _modifierName;
    [SerializeField] private int _modifierValue;
    [SerializeField] private StatType _statToModifiy;

    public string ModifierName => _modifierName;
    public int ModifierValue => _modifierValue;
    public StatType StatToModify => _statToModifiy;

    public Modifier(string name = "New Modifier", int value = 0)
    {
        _modifierName = name;
        _modifierValue = value;
    }
}
