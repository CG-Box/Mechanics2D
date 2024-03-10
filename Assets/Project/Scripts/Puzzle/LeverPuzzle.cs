using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPuzzle : Puzzle
{
    [SerializeField] int[] correctOrder = {1, 2, 3};
    [SerializeField] int[] currentOrder  = default;

    int lastIndex = 0;

    void Start()
    {   
        Init();
    }

    void Init()
    {
        currentOrder = new int[correctOrder.Length];
        for(int i = 0; i < correctOrder.Length; i++) currentOrder[i] = 0;
    }

    [ContextMenu("Reset")]
    public override void Reset()
    {
        base.Reset();
        Init();
    }

    public bool Check()
    {
        if(isComplete) return true;

        isComplete = true;
        for(int i = 0; i < correctOrder.Length; i++)
        {
            if(currentOrder[i] == correctOrder[i])
            {
                continue;
            }
            else
            {
                isComplete = false;
                break;
            }
        }

        if(isComplete)
        {
            OnComplete?.Invoke();
        }
        else
        {
            Debug.Log("incorrect");
        }

        return isComplete;
    }

    public void CheckClick()
    {
        Debug.Log("checked : "+Check());
    }

    public void LeverClick(int index)
    {
        currentOrder[lastIndex] = index;

        lastIndex++;
        if(lastIndex >= correctOrder.Length) lastIndex = 0;
    }
}
