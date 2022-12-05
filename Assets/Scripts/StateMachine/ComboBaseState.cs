using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComboBaseState : State
{
    protected static bool shouldCombo = false; // replaces the canStringAttack in the player combat?
    protected static bool attackFinished = false;
    protected static float attackEndTime;

    protected int attackIndex; // will contain which attack string the combo is currently on?
    public Vector3 attackPoint;
    public float attackRadius = 3f;

    protected CombatStateMachine csm;


    public static event System.Action<int> OnAttackAnimationPlayRequest;


    public ComboBaseState(int _attackIndex, Vector3 _attackPoint, float _attackRadius)
    {
        attackIndex = _attackIndex;
        attackPoint = _attackPoint;
        attackRadius = _attackRadius;
    }

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        shouldCombo = false;

        csm = (CombatStateMachine)stateMachine;
        if (csm.EquippedWeapon != null)
        {
            csm.UpdateAttackPoint(csm.EquippedWeapon.AttackPoints[attackIndex]);
            csm.TotalGracePeriod(attackIndex);
        }
        OnAttackAnimationPlayRequest?.Invoke(attackIndex); // the character animator will listen to this and will fire an animation based on the passed attackIndex
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        
        if (Input.GetMouseButtonDown(0)) shouldCombo = true;
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    // Hit scan
    public void AttackHit_AnimationEvent()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint, attackRadius, csm.enemyMask);
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyStats>().TakeDamage(csm.PlayerStats.Damage.GetValue());
        }
    }

    // Animation end logic
    public static void AttackFinished()
    {
        attackFinished = true;
        shouldCombo = false;
        attackEndTime = fixedtime;
    }
}
