using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Heal")]
public class HealEffect : Effect
{
    [SerializeField] private int amount;
    [SerializeField] private IntEventChannelSO HealEvent;

    public override void Apply()
    {
        base.Apply();
        HealEvent.RaiseEvent(amount);
    }
}
