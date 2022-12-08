using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPanelUIController : MonoBehaviour
{
    public StatUIDisplay StatsPanel;

    private void Awake()
    {
        StatsPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        PlayerStats.OnStatsDisplayUpdateRequested += UpdateStatsPanel;
    }

    private void UpdateStatsPanel(List<Stat> statList)
    {
        StatsPanel.gameObject.SetActive(true);
        StatsPanel.RefreshStatUI(statList);
    }
}
