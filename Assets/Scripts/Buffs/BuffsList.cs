using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuffsList : MonoBehaviour
{
    [SerializeField] private List<Buff> _buffs;

    public List<Buff> Buffs => _buffs;

    private void Awake()
    {
        if( _buffs  == null ) _buffs = new List<Buff>();
    }
}
