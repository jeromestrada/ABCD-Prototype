using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StatUIDisplay : MonoBehaviour
{
    [SerializeField] protected StatUI statPrefab;

    public void RefreshStatUI(List<Stat> statList)
    {
        ClearStats();
        InitStats(statList);
    }

    public void InitStats(List<Stat> statList)
    {
        if (statList == null) return;

        for(int i = 0; i < statList.Count; i++)
        {
            var uiStat = Instantiate(statPrefab, transform);
            uiStat.Init(statList[i]);
        }
    }

    public void ClearStats()
    {
        foreach (var item in transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }
    }
}
