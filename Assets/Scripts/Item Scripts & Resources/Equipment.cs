using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    [SerializeField] private EquipmentType _type;
    [SerializeField] private Buff _equipmentBuff;

    [SerializeField] private int _stringAttacksCount;
    //[SerializeField] private Transform[] _attackPoints; // an array of attack points that we can access to resolve attack hits
    [SerializeField] private List<AttackSO> _attacks;
    /*[SerializeField] private WeaponAnimations _weaponAnimations;
    [SerializeField] private WeaponAnimations _weaponTransitionAnims;*/
    [Tooltip("If certain weapon transitions take time, extensions can be provided so the combo state machine can adjust accordingly - floats")]
    [SerializeField] private float[] gracePeriodExtensions;

    public Buff EquipmentBuff => _equipmentBuff;
    public int StringAttacksCount => _stringAttacksCount;
    //public Transform[] AttackPoints => _attackPoints;

    public List<AttackSO> Attacks => _attacks;
    /*public WeaponAnimations WeaponAnimations => _weaponAnimations;
    public WeaponAnimations WeaponTransitions => _weaponTransitionAnims;*/
    public float[] GracePeriodExtensions => gracePeriodExtensions;

    public static System.Action<Equipment> OnEquipmentUse;
    
    public override void Use()
    {
        OnEquipmentUse?.Invoke(this);
        base.Use();
    }
}
