using UnityEngine;
using System;

public abstract class Condition : DescriptionBaseSO
{
    [SerializeField]private bool isDone = false;
    [SerializeField]private bool isFailed = false;
    public bool IsDone 
    { 
        get {return isDone;}
        protected set {isDone = value;}
    }
    public bool IsFailed 
    { 
        get {return isFailed;}
        protected set {isFailed = value;}
    }
    public event Action OnDone;
    public event Action OnFailed;
    public abstract bool Check();

    public virtual void Reset()
    {
        IsDone = false;
        IsFailed = false;
    }
    public virtual void ForceComplete()
    {
        IsDone = true;
        IsFailed = false;
        OnDone?.Invoke();
    }

    protected void InvokeDone()
    {
        OnDone?.Invoke();
    }
    protected void InvokeFailed()
    {
        OnFailed?.Invoke();
    }

    public virtual void SubscribeEvents()
    {}
    public virtual void UnsubscribeEvents()
    {}
}
