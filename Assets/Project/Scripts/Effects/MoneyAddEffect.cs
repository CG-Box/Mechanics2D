using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/Money Add")]
public class MoneyAddEffect : Effect
{
    [SerializeField]private IntEventChannelSO moneyChangeRequest = default;
    [SerializeField]private int amount;

    public override void Apply()
    {
        base.Apply();

        moneyChangeRequest.RaiseEvent(amount);
    }
}

