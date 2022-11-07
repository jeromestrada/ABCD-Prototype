using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    private ParticleSystem _shieldEffect;
    [SerializeField] private bool _shielded;
    [SerializeField] private CharacterStats _characterStats;

    private void OnEnable()
    {
        BlockAbility.OnBlock += StartShieldEffect;
        BlockAbility.OnBlockEnd += EndShieldEffect;
        _characterStats.OnTakeDamage += PlayBloodSpray;
    }

    private void OnDisable()
    {
        BlockAbility.OnBlock -= StartShieldEffect;
        BlockAbility.OnBlockEnd -= EndShieldEffect;
        _characterStats.OnTakeDamage -= PlayBloodSpray;
    }

    private void StartShieldEffect()
    {
        var shieldEffects = GetComponentsInChildren<ParticleSystem>();
        foreach (var s in shieldEffects)
        {
            if (s.CompareTag("Shield") && s != null)
            {
                _shieldEffect = s;
                _shieldEffect.Play();
                _shielded = true;
                break;
            }
        }
    }

    private void EndShieldEffect()
    {
        _shieldEffect?.Stop();
        Debug.Log($"Setting {gameObject.name}'s {_shielded} to false");
        _shielded = false;
    }

    private void PlayBloodSpray(CharacterStats character)
    {
        Debug.Log($"{gameObject.name} has shielded set to {_shielded}");
        var bloodspray = character.GetComponentsInChildren<ParticleSystem>();
        foreach(var b in bloodspray)
        {
            if (b.CompareTag("Blood"))
            {
                if(!_shielded)
                {
                    b?.Play();
                    break;
                }
            }
        }
    }
}
