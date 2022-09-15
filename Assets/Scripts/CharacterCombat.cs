using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    private bool canAttack = true;

    public event System.Action OnAttack;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && canAttack)
        {
            if(OnAttack != null)
            {
                canAttack = false;
                OnAttack();
            }
        }
    }

    public void AttackHit_AnimationEvent()
    {
        Debug.Log("Animation Hit!");
        canAttack = true;
    }
}
