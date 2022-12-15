using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buff", fileName = "New Buff")]
[System.Serializable]
public class Buff : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField, TextArea(3, 5)] private string _description;
    [SerializeField] private Sprite _icon;
    [SerializeField] private List<Modifier> _effects;

    public string Name => _name;

    private void Awake()
    {
        if (_effects == null) _effects = new List<Modifier>();
    }

    public void Apply(CharacterStats target)
    {
        foreach(Modifier m in _effects)
        {
            target.Stat(m.StatToModify.ToString()).AddModifier(m);
        }
    }

    public void Purge(CharacterStats target)
    {
        foreach (Modifier m in _effects)
        {
            target.Stat(m.StatToModify.ToString()).RemoveModifier(m);
        }
    }
}
