using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoonPhaseSystem : MonoBehaviour
{
    [SerializeField] private Moon _moon;

    public Moon Moon => _moon;

    protected virtual void Awake()
    {
        _moon = new Moon();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            _moon.Transition();
        }
    }
}
