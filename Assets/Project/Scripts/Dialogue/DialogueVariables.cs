using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DialogueVariables
{
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

    private Story globalVariablesStory;

    public DialogueVariables(TextAsset loadGlobalsJSON, string dialogueData) 
    {
        // create the story
        globalVariablesStory = new Story(loadGlobalsJSON.text);
        // if we have saved data, load it
        UpdateVariables(dialogueData);
    }

    public Story GetGlobalStory()
    {
        return globalVariablesStory;
    }

    // _inkStory.variablesState["player_health"] = 100  // set var

    // int health = (int) _inkStory.variablesState["player_health"]  // get var

    /*
    _inkStory.ObserveVariable ("health", (string varName, object newValue) => {
        SetHealthInUI((int)newValue);
    }); */

    void FillDictionary()
    {
        // initialize the dictionary
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            
            //Debug.Log("Initialized global dialogue variable: " + name + " = " + value);
        }
    }

    public void UpdateVariables(string jsonState)
    {
        LoadVariables(jsonState);
        FillDictionary();
    }

    void LoadVariables(string jsonState)
    {
        if(!string.IsNullOrEmpty(jsonState))
        {
            globalVariablesStory.state.LoadJson(jsonState);
        }
    }
    public string GetVariables() 
    {
        if (globalVariablesStory != null) 
        {
            // Load the current state of all of our variables to the globals story
            VariablesToStory(globalVariablesStory);

            return  globalVariablesStory.state.ToJson();
        }
        else
        {
            return null;
        }
    }

    public void StartListening(Story story) 
    {
        // it's important that VariablesToStory is before assigning the listener!
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story) 
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    private void VariableChanged(string name, Ink.Runtime.Object value) 
    {
        // only maintain variables that were initialized from the globals ink file
        if (variables.ContainsKey(name)) 
        {
            variables.Remove(name);
            variables.Add(name, value);
        }
    }

    private void VariablesToStory(Story story) 
    {
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in variables) 
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }

}