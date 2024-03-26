using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum RoadID
{
    AB,
    AC,
    CB,
    BD
}


public class Road : MapSpace
{
    [SerializeField] private RoadID roadID;
    public RoadID RoadID { get { return roadID; } }

    [SerializeField] private int length;
    public int Length { get { return length; } }

    public event EventHandler<int> OnSelectRoad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Select()
    {
        OnSelectRoad?.Invoke(this, length);
    }



    public void Bounce()
    {
        Bounce(0.1f);
    }
    public void Bounce(float delay)
    {
        gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 1f).setDelay(delay).setEase(LeanTweenType.easeOutElastic);
    }
}
