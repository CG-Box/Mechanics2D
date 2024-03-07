using UnityEngine;
using UnityEngine.Events;

public class IntCounter : MonoBehaviour
{
    [SerializeField] private int target;
    [SerializeField] int current = 0;
    [SerializeField] bool repeat = false;

    bool reachTarget = false;

    public UnityEvent OnReachTarget;

    public int Current { get {return current;} }

    public void Increase()
    {
        if(reachTarget) return;
        current++;

        if(current >= target)
        {
            reachTarget = true;
            OnReachTarget.Invoke();
            if(repeat == true)
            {
                Reset();
            }
        }
    }
    public void Decrease()
    {
        if(reachTarget) return;
        current--;
        if(current < 0 ) current = 0;
    }

    public void Reset()
    {   
        current = 0;
        reachTarget = false;
    }
}
