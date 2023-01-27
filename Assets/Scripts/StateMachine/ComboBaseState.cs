using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComboBaseState : State
{
    protected static bool shouldCombo = false; // replaces the canStringAttack in the player combat?
    protected static bool attackFinished = false;
    protected static float attackEndTime = int.MaxValue;

    protected int attackIndex; // will contain which attack string the combo is currently on?
    public Vector3 attackPoint;
    public float attackRadius = 3f;
    public int attackDamage;
    public float gracePeriod;
    public LayerMask targetMask;

    public static event System.Action<int> OnAttackAnimationPlayRequest;
    public static event System.Action<int> OnEnterAttackString;


    public ComboBaseState(int _attackIndex, Vector3 _attackPoint, float _attackRadius, int _attackDamage, float _gracePeriod, LayerMask _targetMask)
    {
        attackIndex = _attackIndex;
        attackPoint = _attackPoint;
        attackRadius = _attackRadius;
        attackDamage = _attackDamage;
        gracePeriod = _gracePeriod;
        targetMask = _targetMask;
    }

    public override void OnEnter(StateMachine _stateMachine)
    {
        attackFinished = false;
        base.OnEnter(_stateMachine);
        shouldCombo = false;
        OnEnterAttackString?.Invoke(attackIndex);
        OnAttackAnimationPlayRequest?.Invoke(attackIndex); // the character animator will listen to this and will fire an animation based on the passed attackIndex
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Input.GetMouseButtonDown(0)) shouldCombo = true;
    }

    public override void OnExit()
    {
        Debug.Log($"Exiting: {attackIndex}");
        base.OnExit();
    }

    // Hit scan
    public void AttackHit_AnimationEvent()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint, attackRadius, targetMask);
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyStats>().TakeDamage(attackDamage);
        }
    }

    // Animation end logic
    public static void AttackFinished()
    {
        attackEndTime = fixedtime;
        attackFinished = true;
        shouldCombo = false;
    }
}
