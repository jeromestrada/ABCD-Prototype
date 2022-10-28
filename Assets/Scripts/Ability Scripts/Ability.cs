using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public class Ability : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField, TextArea(3, 5)] private string _description;
    // [SerializeField] private Image icon; // will support having ability icons later
    [SerializeField] private float _cooldownTime;
    [SerializeField] private float _castCost;
    [SerializeField] private AbilityState _state;
    [SerializeField] private KeyCode _key;

    // Accessors
    public string Name => _name;
    public string Description => _description;
    public float CooldownTime => _cooldownTime;
    public float CastCost => _castCost;
    public AbilityState State => _state;
    public KeyCode Key => _key;

    public virtual void Activate() { }
}

public enum AbilityState
{
    Ready, Active, Cooldown
}