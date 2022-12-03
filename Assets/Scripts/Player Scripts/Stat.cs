using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private int baseValue;
    [SerializeField] private List<Modifier> modifiers = new List<Modifier>();
    public int GetValue()
    {
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x.ModifierValue);
        return finalValue;
    }

    public void AddModifier(Modifier modifier)
    {
        if (modifier.ModifierValue != 0)
        {
            modifiers.Add(modifier);
        }
    }

    public void RemoveModifier(Modifier modifier)
    {
        if (modifier == null) return;
        if (modifier.ModifierValue != 0)
        {
            modifiers.Remove(modifier);
        }
    }
}
