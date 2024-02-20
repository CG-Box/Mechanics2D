using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConditionHolder : MonoBehaviour
{
    [SerializeField] private int doneCounter = 0;
    [SerializeField] private bool checkOnStart = false;
    public UnityEvent OnDoneRaised;
    public UnityEvent OnFailedRaised;
    [SerializeField] private Condition[] conditions;

    void OnEnable()
    {
        ResetConditions();
        AddHandlers();
    }
    void OnDisable()
    {
        RemoveHandlers();
    }

    void Start()
    {
        if(checkOnStart)  CheckConditions();
    }

    void AddHandlers()
    {
        foreach(Condition condition in conditions)
        {
            condition.OnDone += DoneHandler;
            condition.OnFailed += FailedHandler;
            condition.SubscribeEvents();
        }
    }
    void RemoveHandlers()
    {
        foreach(Condition condition in conditions)
        {
            condition.OnDone -= DoneHandler;
            condition.OnFailed -= FailedHandler;
            condition.UnsubscribeEvents();
        }
    }

    [ContextMenu("Reset")]
    public void ResetConditions()
    {
        foreach(Condition condition in conditions)
        {
            condition.Reset();
        }
        doneCounter = 0;
    }

    void DoneHandler()
    {
        //Debug.Log("DoneHandler");
        doneCounter++;
        if(doneCounter == conditions.Length)
        {
            OnDoneRaised?.Invoke();
            Debug.Log("all done");
        }
    }
    void FailedHandler()
    {
        OnFailedRaised?.Invoke();
        Debug.Log("something failed");
        //RemoveHandlers();
    }

    [ContextMenu("Check")]
    public void CheckConditions()
    {
        foreach(Condition condition in conditions)
        {
            condition.Check();
        }
    }
}
