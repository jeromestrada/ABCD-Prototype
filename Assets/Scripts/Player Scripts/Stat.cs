using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable, CreateAssetMenu(fileName = "New Stat", menuName = "Stats/Stat")]
public class Stat : ScriptableObject
{
    [SerializeField] private string _statName;
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

    public void ClearModifiers()
    {
        modifiers.Clear();
    }
}
