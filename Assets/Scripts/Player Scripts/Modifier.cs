using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Modifier
{
    [SerializeField] private string _modifierName;
    [SerializeField] private int _modifierValue;
    [SerializeField] private string _statToModifiy;

    public string ModifierName => _modifierName;
    public int ModifierValue => _modifierValue;
    public string StatToModify => _statToModifiy;

    public Modifier(string name, int value)
    {
        _modifierName = name;
        _modifierValue = value;
    }
}
