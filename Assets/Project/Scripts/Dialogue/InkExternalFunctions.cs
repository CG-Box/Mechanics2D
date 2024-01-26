using Ink.Runtime;
using UnityEngine;
using System;

public class InkExternalFunctions
{
    enum Function
    {
        Log,
        AddItem,
        RemoveItem,
        AddMoney,
        TakeQuest,
    }

    Action<string> SomeFunc;

    public InkExternalFunctions()
    {
        SomeFunc = Log;
    }

    public void Bind(Story story)
    {
        //story.BindExternalFunction(nameof(Function.Log), (string text) => Log(text));

        story.BindExternalFunction(nameof(Function.Log), SomeFunc);

        story.BindExternalFunction(nameof(Function.AddItem), (int itemId) => {
            ItemType itemType = (ItemType)itemId;
            Debug.Log($"Add item {itemType}");
        });
        story.BindExternalFunction(nameof(Function.RemoveItem), (int itemId) => {
            ItemType itemType = (ItemType)itemId;
            Debug.Log($"Remove item {itemType}");
        });
        story.BindExternalFunction(nameof(Function.AddMoney), (int amount) => {
            Debug.Log($"Add money {amount}");
        });
        story.BindExternalFunction(nameof(Function.TakeQuest), (string questName) => {
            Debug.Log($"Take quest {questName}");
        });
    }
    public void Unbind(Story story)
    {
        story.UnbindExternalFunction(nameof(Function.Log));
        story.UnbindExternalFunction(nameof(Function.AddItem));
        story.UnbindExternalFunction(nameof(Function.RemoveItem));
        story.UnbindExternalFunction(nameof(Function.AddMoney));
        story.UnbindExternalFunction(nameof(Function.TakeQuest));
    }

    void Log(string str)
    {
        Debug.Log($"story log: {str}");
    }
}
