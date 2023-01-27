using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCharacter : MonoBehaviour
{

    [SerializeField] private CombatStateMachine csm;

    [SerializeField] public Collider2D hitbox;
    [SerializeField] public GameObject HitEffect;
    private bool hasWeapon = false;

    public static event System.Action<int> OnFirstAttack;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && csm.CurrentState.GetType() == typeof(IdleCombatState) 
            && !MouseItemData.IsPointerOverUIObjects() && !MouseItemData.inUI && hasWeapon)
        {
            Debug.Log("First attack!");
            OnFirstAttack?.Invoke(0);
        }
    }

    public void SetMaxCombo(Equipment weapon)
    {
        hasWeapon = true;
        csm.SetNumOfStates(weapon.StringAttacksCount);
    }
}
