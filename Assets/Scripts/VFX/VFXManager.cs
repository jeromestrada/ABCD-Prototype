using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _shieldEffects;

    private void OnEnable()
    {
        BlockAbility.OnBlock += StartShieldEffect;
        BlockAbility.OnBlockEnd += EndShieldEffect;
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
}
