using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    private ParticleSystem _shieldEffect;
    private bool _shielded;
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
        CharacterStats.OnTakeDamage -= PlayBloodSpray;
    }

    private void StartShieldEffect()
    {
        var shieldEffects = GetComponentsInChildren<ParticleSystem>();
        foreach (var s in shieldEffects)
        {
            if (s.CompareTag("Shield") && s != null)
            {
                _shieldEffect = s;
                s?.Play();
                _shielded = true;
                break;
            }
        }
    }

    private void EndShieldEffect()
    {
        _shieldEffect?.Stop();
        _shielded = false;
    }

    private void PlayBloodSpray(CharacterStats character)
    {
        var bloodspray = character.GetComponentsInChildren<ParticleSystem>();
        foreach(var b in bloodspray)
        {
            if (b.CompareTag("Blood"))
            {
                if(!_shielded)b?.Play();
                break;
            }
        }
    }
}
