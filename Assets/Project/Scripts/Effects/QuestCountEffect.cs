using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Effects/Quest Count Test")]
public class QuestCountEffect : Effect
{
    // Delete script and scriptableObject
    [SerializeField] private int amount;

    [SerializeField] private int playerCharisma;
    [SerializeField] private int playerManipulation;
    [SerializeField] private int playerAppearance;
    [SerializeField] private int playerPerception;
    [SerializeField] private int playerIntelligence;
    //[SerializeField] private IntEventChannelSO HealEvent;

    const string valueKey = "surviveQuestTakenTimes";

    public override void Apply()
    {
        base.Apply();

        if(amount != 0)
        {   
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            //dictionary[valueKey] = amount;
            //dictionary["pokemon_name"] = "Pikachu";

            if(true)
            {
                dictionary[nameof(playerCharisma)] = playerCharisma;
                dictionary[nameof(playerManipulation)] = playerManipulation;
                dictionary[nameof(playerAppearance)] = playerAppearance;
                dictionary[nameof(playerPerception)] = playerPerception;
                dictionary[nameof(playerIntelligence)] = playerIntelligence;
            }


            DialogueManager.GetInstance().SetVariableState(dictionary);

            //DialogueManager.GetInstance().SetVariableState(valueKey, amount);             
        }
        //DialogueManager.GetInstance().PrintGlobalVariables();

        //HealEvent.RaiseEvent(amount);
    }
}

