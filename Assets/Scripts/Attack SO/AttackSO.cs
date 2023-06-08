using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Combos/Attack")]
public class AttackSO : ScriptableObject
{
    [SerializeField] Animation attackAnimation;
    [SerializeField] Animation recoveryAnimation;
    [SerializeField] float attackDamage;
}
