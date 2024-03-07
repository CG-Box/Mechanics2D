using UnityEngine;
using System;

public enum EffectType
{
    Empty,
    Heal,
    Damage,
    AddMoney,
    AddItem,
    AddStatPoint,
    StatIncrease,
    AddNote,
}
[Serializable]
public struct EffectData
{
    public EffectType type;
    public int amount;
    public float duration;
}

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/Effect Manager", order = 99)]
public class EffectManager : DescriptionBaseSO
{
    [SerializeField]private Effect[] allEffects;

    public void UseEffect(EffectData data)
    {
        if(data.type == EffectType.Empty) return;

        Effect targetEffect =  allEffects[0];
        targetEffect.SetData(data);
        targetEffect.Apply();
    }
}
