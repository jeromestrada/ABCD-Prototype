using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCharacter : MonoBehaviour
{

    private StateMachine meleeStateMachine;

    [SerializeField] public Collider2D hitbox;
    [SerializeField] public GameObject Hiteffect;

    // Start is called before the first frame update
    void Start()
    {
        meleeStateMachine = GetComponent<StateMachine>();
        // Debug.Log($"CC started! {meleeStateMachine.gameObject.name}");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && meleeStateMachine.CurrentState.GetType() == typeof(IdleCombatState))
        {
            Debug.Log("CC has detected an attack");
            meleeStateMachine.SetNextState(new AttackStringState(0));
        }
    }

    public void SetMaxCombo(Equipment weapon)
    {
        meleeStateMachine.SetNumOfStates(weapon.StringAttacksCount);
    }
}
