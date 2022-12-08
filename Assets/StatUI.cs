using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatUI : MonoBehaviour
{
    [SerializeField] private Image statIcon;
    [SerializeField] private TextMeshProUGUI statValue;
    [SerializeField] private Stat _stat;
    [SerializeField] private HoverTip hoverTip;

    private void Awake()
    {
        
    }

    public void Init(Stat stat)
    {
        _stat = stat;
        UpdateStatUI(stat);
    }

    public void UpdateStatUI(Stat stat)
    {
        if (_stat != null)
        {

            statValue.text = stat.GetValue().ToString();

        }
    }
}
