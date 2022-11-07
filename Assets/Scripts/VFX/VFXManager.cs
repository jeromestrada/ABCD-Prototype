using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _shieldEffects;
    //[SerializeField] private ParticleSystem _bloodSpray;

    private void OnEnable()
    {
        BlockAbility.OnBlock += StartShieldEffect;
        BlockAbility.OnBlockEnd += EndShieldEffect;
        CharacterStats.OnTakeDamage += PlayBloodSpray;
    }

    private void OnDisable()
    {
        BlockAbility.OnBlock -= StartShieldEffect;
        BlockAbility.OnBlockEnd -= EndShieldEffect;
    }

    private void StartShieldEffect()
    {
        _shieldEffects?.Play();
    }

    private void EndShieldEffect()
    {
        _shieldEffects?.Stop();
    }

    private void PlayBloodSpray(CharacterStats character)
    {
        var bloodspray = character.GetComponentsInChildren<ParticleSystem>();
        foreach(var b in bloodspray)
        {
            if (b.CompareTag("Blood"))
            {
                b.Play();
                break;
            }
        }
    }
}
