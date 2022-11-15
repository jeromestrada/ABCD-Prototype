using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;

public class HeartSystemDisplay : MonoBehaviour
{
    [SerializeField] protected Image _heartPrefab;
    protected HeartSystem _heartSystem;


    public void RefreshHeartSystemDisplay(HeartSystem heartSystem)
    {
        ClearHearts();
        _heartSystem = heartSystem;
        PlaceHearts(heartSystem);
    }
    public void PlaceHearts(HeartSystem heartSysToDisplay)
    {
        if (heartSysToDisplay == null) return;

        for(int i = 0; i < heartSysToDisplay.CurrentHeartCount; i++)
        {
            Instantiate(_heartPrefab, transform);
        }
    }

    public void ClearHearts()
    {
        foreach(var h in transform.Cast<Transform>())
        {
            Destroy(h.gameObject);
        }
    }
}
