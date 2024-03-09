using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/Heal")]
public class HealEffect : Effect
{
    //[SerializeField] private IntEventChannelSO HealEvent;
    [SerializeField] private int amount;
    public int Amount { get {return amount;} }

    public override void Apply()
    {
        base.Apply();
        //HealEvent.RaiseEvent(amount);
        EffectManager.Heal_Static(this);
    }
}
