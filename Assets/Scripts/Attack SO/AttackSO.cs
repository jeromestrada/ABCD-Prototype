using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Combos/Attack")]
public class AttackSO : ScriptableObject
{
    [SerializeField] private AnimationClip _attackAnimation;
    [SerializeField] private AnimationClip _recoveryAnimation;
    [SerializeField] private float _attackDamage;
    [SerializeField] private Vector3 _attackPoint;

    public AnimationClip AttackAnimation => _attackAnimation;
    public AnimationClip RecoveryAnimation => _recoveryAnimation;
    public float AttackDamage => _attackDamage;
    public Vector3 AttackPoint => _attackPoint;
}
