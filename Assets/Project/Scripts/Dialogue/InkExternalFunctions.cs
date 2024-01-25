using Ink.Runtime;
using UnityEngine;
using System;

public class InkExternalFunctions
{
    enum Function
    {
        Log,
        Loggy
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

        /*
        story.BindExternalFunction(nameof(Function.Loggy), (int number) => {
            Debug.Log($"Number is {number}");
        });*/
    }
    public void Unbind(Story story)
    {
        story.UnbindExternalFunction(nameof(Function.Log));
        //story.UnbindExternalFunction(nameof(Function.Loggy));
    }

    void Log(string str)
    {
        Debug.Log($"story log: {str}");
    }
}
