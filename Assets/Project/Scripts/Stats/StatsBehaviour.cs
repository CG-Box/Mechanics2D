using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// using in GameData when new save is creating
public enum StatType
{
    Empty,
    Agility,
    Strength,
    Intelligence
}

public struct StatData
{
    public readonly StatType type;
    public readonly int value;
    public StatData(StatType type, int value)
    {
        this.type = type;
        this.value = value;
    }
}

public class StatsBehaviour : MonoBehaviour, ITakeFromFile
{
    [SerializeField] private int points = 0; // free points

    [SerializeField]private StatDataEventChannelSO statsChangeEvent = default;

    [SerializeField]private StatDataEventChannelSO statsChangeRequest = default;

    SerializableDictionary<StatType, int> statsData;

    void OnEnable()
    {
        if (statsChangeRequest != null)
			statsChangeRequest.OnEventRaised += StatsChangeRequest_Handler;
    }
    void OnDisable()
    {
        if (statsChangeRequest != null)
			statsChangeRequest.OnEventRaised -= StatsChangeRequest_Handler;
    }

    void StatsChangeRequest_Handler(StatData statData)
    {
        TryChangeStat(statData);
    }
    public bool TryChangeStat(StatData statData)
    {
        if(statsData.ContainsKey(statData.type))
        {   
            Increase(statData.type, statData.value);
            return true;
        }
        else
        {
            Debug.LogWarning($"stat name : {statData.type} doesn't exist in the statsData");
            return false;
        }
    }


    //Delete this region
    #region DemoInit
    void Start()
    {
        DemoInit();
    }

    void DemoInit()
    {
        statsData = new SerializableDictionary<StatType, int>();
        statsData[StatType.Agility] = 1;
        statsData[StatType.Strength] = 2;
        statsData[StatType.Intelligence] = 3;
        Debug.Log("Demo stats init, not use it in actual game");
    }
    #endregion

    public int GetStat(StatType type)
    {
        int stat;
        if(!statsData.TryGetValue(type, out stat))
        {   
            Debug.LogWarning($"stat name : {type} doesn't exist in the statsData");
            stat = 0;
        }
        return stat;
    }

    public void Increase(StatType type, int amount = 1)
    {
        statsData[type] += amount;
        statsChangeEvent.RaiseEvent(new StatData(type,statsData[type]));
    }
    public void Reduce(StatType type)
    {
        if(statsData[type] > 0)
        {
            statsData[type] -= 1;
            statsChangeEvent.RaiseEvent(new StatData(type,statsData[type]));
        } 
    }


    [ContextMenu("Print")]
    public void Print()
    {
        if(statsData == null)
        {
            Debug.LogWarning("stats is not inited yet");
            return;
        }
        foreach(KeyValuePair<StatType, int> variable in statsData) 
        {
            Debug.Log($"{variable.Key} : {variable.Value}");
        }
    }


    [ContextMenu("AddPoint")]
    public void AddPoint()
    {
        AddPoint(1);
    }
    public void AddPoint(int amount)
    {
        points += amount;
    }

    [ContextMenu("RemovePoint")]
    public void RemovePoint()
    {
        if(points > 0)
            points--;
    }


    //ITakeFromFile
    public void LoadData(GameData data)
    {
        this.statsData = data.globals.statsData;
        Debug.Log("Stats LoadData");
    }
}
