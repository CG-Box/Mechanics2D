using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Quest Count Test")]
public class QuestCountEffect : Effect
{
    // Delete script and scriptableObject
    [SerializeField] private int amount;
    //[SerializeField] private IntEventChannelSO HealEvent;

    const string valueKey = "surviveQuestTakenTimes";

    public override void Apply()
    {
        base.Apply();

        //var InkRuntimeObject = DialogueManager.GetInstance().GetVariableState(valueKey);

        DialogueManager.GetInstance().SetVariableState(valueKey, amount);

        //HealEvent.RaiseEvent(amount);
    }
}

