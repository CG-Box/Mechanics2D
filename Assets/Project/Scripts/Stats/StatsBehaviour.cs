using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

public class StatsBehaviour : MonoBehaviour //, ITakeFromFile
{
    public int Points { get { return stats.points; } }

    [SerializeField]private SlotManager slotManager;

    [SerializeField]private StatsPanel statsPanel;

	[Header("Events Raise")]
    [SerializeField]private IntEventChannelSO pointsChangeEvent = default;
    [SerializeField]private StatDataEventChannelSO statsChangeEvent = default;

    GameData.Stats stats;

    void OnEnable()
    {
        LoadData(slotManager.GetActiveSlot().data);

        AddPanelListeners();
    }
    void OnDisable()
    {
        RemovePanelListeners();
    }

    void Awake()
    {
        statsPanel.SetStatsBehaviour(this);
    }

    void AddPanelListeners()
    {
        statsPanel.OnAddPoint += StatsBehaviour_OnAddPoint;
        statsPanel.OnRemovePoint += StatsBehaviour_OnRemovePoint;

        statsPanel.OnIncreaseStat += StatsBehaviour_OnIncreaseStat;
        statsPanel.OnReduceStat += StatsBehaviour_OnReduceStat;
    }
    void RemovePanelListeners()
    {
        statsPanel.OnAddPoint -= StatsBehaviour_OnAddPoint;
        statsPanel.OnRemovePoint -= StatsBehaviour_OnRemovePoint;

        statsPanel.OnIncreaseStat -= StatsBehaviour_OnIncreaseStat;
        statsPanel.OnReduceStat -= StatsBehaviour_OnReduceStat;
    }


    public void StatsChangeRequest_Handler(StatData statData)
    {
        TryChangeStat(statData);
    }
    public bool TryChangeStat(StatData statData)
    {
        if(stats.dict.ContainsKey(statData.type))
        {   
            Increase(statData.type, statData.value);
            return true;
        }
        else
        {
            Debug.LogWarning($"stat name : {statData.type} doesn't exist in the stats.dict");
            return false;
        }
    }


    public int GetStat(StatType type)
    {
        int stat;
        if(!stats.dict.TryGetValue(type, out stat))
        {   
            Debug.LogWarning($"stat name : {type} doesn't exist in the stats dictionary");
            stat = 0;
        }
        return stat;
    }

    public SerializableDictionary<StatType, int> GetAllStats()
    {
        return stats.dict;
    }

    public void Increase(StatType type, int amount = 1)
    {
        stats.dict[type] += amount;
        statsChangeEvent.RaiseEvent(new StatData(type, stats.dict[type]));
    }
    public void Reduce(StatType type)
    {
        if(stats.dict[type] > 0)
        {
            stats.dict[type] -= 1;
            statsChangeEvent.RaiseEvent(new StatData(type, stats.dict[type]));
        } 
    }


    [ContextMenu("Print")]
    public void Print()
    {
        if(stats.dict == null)
        {
            Debug.LogWarning("stats is not inited yet");
            return;
        }
        foreach(KeyValuePair<StatType, int> variable in stats.dict) 
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
        stats.points += amount;
        pointsChangeEvent.RaiseEvent(stats.points);
    }

    [ContextMenu("RemovePoint")]
    public void RemovePoint()
    {
        if(stats.points > 0)
        {
            stats.points--;
            pointsChangeEvent.RaiseEvent(stats.points);
        }
    }

    void StatsBehaviour_OnIncreaseStat(object sender, StatSlotEventArgs eventArgs)
    {
        if(Points > 0)
        {
            RemovePoint();
            Increase(eventArgs.statData.type);
        }
        else
        {
            Debug.Log("not enough free points");
        }
    }
    void StatsBehaviour_OnReduceStat(object sender, StatSlotEventArgs eventArgs)
    {
        Reduce(eventArgs.statData.type);
    }

    void StatsBehaviour_OnAddPoint(object sender, EventArgs eventArgs)
    {
        AddPoint();
    }
    void StatsBehaviour_OnRemovePoint(object sender, EventArgs eventArgs)
    {
        RemovePoint();
    }


    //ITakeFromFile
    public void LoadData(GameData data)
    {
        this.stats = data.globals.stats;
    }
}
