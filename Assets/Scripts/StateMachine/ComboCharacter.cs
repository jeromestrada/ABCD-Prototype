using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCharacter : MonoBehaviour
{

    [SerializeField] private CombatStateMachine combatStateMachine;

    [SerializeField] public Collider2D hitbox;
    [SerializeField] public GameObject Hiteffect;
    private bool hasWeapon = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && combatStateMachine.CurrentState.GetType() == typeof(IdleCombatState) && !MouseItemData.IsPointerOverUIObjects() && !MouseItemData.inUI && hasWeapon)
        {
            Debug.Log("CC has detected an attack");
            combatStateMachine.SetNextState(new AttackStringState(0, combatStateMachine.PlaceAttackPoint(), combatStateMachine.AttackRadius));
        }
    }

    public void SetMaxCombo(Equipment weapon)
    {
        hasWeapon = true;
        combatStateMachine.SetNumOfStates(weapon.StringAttacksCount);
    }
}
