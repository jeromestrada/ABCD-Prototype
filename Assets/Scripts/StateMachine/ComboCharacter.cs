using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCharacter : MonoBehaviour
{

    [SerializeField] private CombatStateMachine csm;

    [SerializeField] public Collider2D hitbox;
    [SerializeField] public GameObject Hiteffect;
    private bool hasWeapon = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && csm.CurrentState.GetType() == typeof(IdleCombatState) 
            && !MouseItemData.IsPointerOverUIObjects() && !MouseItemData.inUI && hasWeapon)
        {
            csm.SetNextState(new AttackStringState(0, csm.PlaceAttackPoint(), csm.AttackRadius, csm.GracePeriodExtensions[0]));
        }
    }

    public void SetMaxCombo(Equipment weapon)
    {
        hasWeapon = true;
        csm.SetNumOfStates(weapon.StringAttacksCount);
    }
}
