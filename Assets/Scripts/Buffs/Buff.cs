using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buff", fileName = "New Buff")]
public class Buff : ScriptableObject
{
    [SerializeField] private string _buffName;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;
    [SerializeField] private List<Modifier> _modifiers;

    private void Awake()
    {
        if (_modifiers == null) _modifiers = new List<Modifier>();
    }

    public void Apply(CharacterStats target)
    {
        foreach (var m in _modifiers)
        {
            target.AddStatModifier(m.StatToModify, m);
        }
    }

    public void Purge(CharacterStats target)
    {
        foreach (var m in _modifiers)
        {
            target.RemoveStatModifier(m.StatToModify, m);
        }
    }
}
