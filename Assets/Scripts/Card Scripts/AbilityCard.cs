using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability Card", menuName = "Cards/Ability Card")]
public class AbilityCard : Card
{
    public Ability ability;

    private void Awake()
    {
        _cardType = CardType.AbilityCard;
    }
    public override bool Use()
    {
        ability.Activate();
        return base.Use();
    }
}
