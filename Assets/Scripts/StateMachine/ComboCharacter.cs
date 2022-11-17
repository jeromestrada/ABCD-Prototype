using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCharacter : MonoBehaviour
{

    [SerializeField] private StateMachine meleeStateMachine;

    [SerializeField] public Collider2D hitbox;
    [SerializeField] public GameObject Hiteffect;
    private bool hasWeapon = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && meleeStateMachine.CurrentState.GetType() == typeof(IdleCombatState) && !MouseItemData.IsPointerOverUIObjects() && !MouseItemData.inUI && hasWeapon)
        {
            Debug.Log("CC has detected an attack");
            meleeStateMachine.SetNextState(new AttackStringState(0));
        }
    }

    public void SetMaxCombo(Equipment weapon)
    {
        hasWeapon = true;
        meleeStateMachine.SetNumOfStates(weapon.StringAttacksCount);
    }
}
