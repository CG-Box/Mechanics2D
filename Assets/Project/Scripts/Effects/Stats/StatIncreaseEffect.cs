using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/Stat Increase")]
public class StatIncreaseEffect : Effect
{
    //[SerializeField]private StatDataEventChannelSO statsChangeRequest = default;
    [SerializeField]private StatType statType;
    [SerializeField]private int addAmount;

    public StatType Type { get {return statType;} }
    public int Amount { get {return addAmount;} }

    public override void Apply()
    {
        base.Apply();

        //statsChangeRequest.RaiseEvent(new StatData(statType,addAmount));
        EffectManager.StatIncrease_Static(this);
    }
}

