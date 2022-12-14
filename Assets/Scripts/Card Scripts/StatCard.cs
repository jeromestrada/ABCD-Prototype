using UnityEngine;

[CreateAssetMenu(fileName = "New Stat Card", menuName = "Cards/Stat Card")]
public class StatCard : Card
{
    [SerializeField] private StatCardType _statCardType;
    [SerializeField] private Buff _statBuff;
    
    public Buff StatBuff => _statBuff;
    public StatCardType StatCardType => _statCardType;
    public static event System.Action<StatCard, bool> OnStatCardUsed;
    public override bool Use()
    {
        // have a bonus effect when using the stat card,
        // using a stat card is equivalent to discarding it, making space for other cards in the hand to be drawn
        // player loses the stat benefit but will be rewarded with a very helpful effect i.e. next attacks deal 2x damage or something similar
        OnStatCardUsed?.Invoke(this, false);
        return base.Use();
    }
}

public enum StatCardType { DamageStat, ArmorStat }