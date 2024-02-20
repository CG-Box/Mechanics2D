using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Effects/Stat Increase")]
public class StatIncreaseEffect : Effect
{
    [SerializeField]private StatDataEventChannelSO statsChangeRequest = default;
    [SerializeField]private StatType statType;
    [SerializeField]private int addAmount;

    public override void Apply()
    {
        base.Apply();

        statsChangeRequest.RaiseEvent(new StatData(statType,addAmount));
    }
}

