using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Modifier
{
    [SerializeField] private string _modifierName;
    [SerializeField] private int _modifierValue;

    public string ModifierName => _modifierName;
    public int ModifierValue => _modifierValue;

    public Modifier(string name, int value)
    {
        _modifierName = name;
        _modifierValue = value;
    }
}
