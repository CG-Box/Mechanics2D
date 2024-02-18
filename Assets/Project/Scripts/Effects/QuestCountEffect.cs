using System;
using UnityEngine;
using System.Collections.Generic;

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

        if(amount != 0)
        {   
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary[valueKey] = amount;
            dictionary["pokemon_name"] = "Pikachu";
            DialogueManager.GetInstance().SetVariableState(dictionary);

            //DialogueManager.GetInstance().SetVariableState(valueKey, amount);
            //DialogueManager.GetInstance().SetVariableState("pokemon_name", "Pikachu");
             
        }
        DialogueManager.GetInstance().PrintGlobalVariables();

        //HealEvent.RaiseEvent(amount);
    }
}

