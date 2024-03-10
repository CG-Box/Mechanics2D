using UnityEngine;
using UnityEngine.Events;

public class Puzzle : MonoBehaviour
{
    protected bool isComplete = false;
    public bool IsComplete { get { return isComplete; } }
    public UnityEvent OnEnterPuzzle, OnExitPuzzle;
    public UnityEvent OnComplete, OnFail;

    public virtual void Reset()
    {
        isComplete = false;

        //Debug.Log("Puzzle Reset");
    }
}
