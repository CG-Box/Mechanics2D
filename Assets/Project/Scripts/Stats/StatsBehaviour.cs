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

public class StatsBehaviour : MonoBehaviour //, ITakeFromFile
{
    [SerializeField]private int points = 0; // free points

    [SerializeField]private SlotManager slotManager;

    [SerializeField]private StatsPanel statsPanel;


	[Header("Events Raise")]
    [SerializeField]private StatDataEventChannelSO statsChangeEvent = default;

    [Header("Events Listen")]
    [SerializeField]private StatDataEventChannelSO statsChangeRequest = default;

    SerializableDictionary<StatType, int> statsData;

    void OnEnable()
    {
        LoadData(slotManager.GetActiveSlot().data);

        AddPanelListeners();

        if (statsChangeRequest != null)
			statsChangeRequest.OnEventRaised += StatsChangeRequest_Handler;
    }
    void OnDisable()
    {
        RemovePanelListeners();

        if (statsChangeRequest != null)
			statsChangeRequest.OnEventRaised -= StatsChangeRequest_Handler;
    }

    void Awake()
    {
        statsPanel.SetStatsBehaviour(this);
    }

    void AddPanelListeners()
    {
        statsPanel.OnIncreaseStat += StatsBehaviour_OnIncreaseStat;
        statsPanel.OnReduceStat += StatsBehaviour_OnReduceStat;
    }
    void RemovePanelListeners()
    {
        statsPanel.OnIncreaseStat -= StatsBehaviour_OnIncreaseStat;
        statsPanel.OnReduceStat -= StatsBehaviour_OnReduceStat;
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


    public int GetStat(StatType type)
    {
        int stat;
        if(!statsData.TryGetValue(type, out stat))
        {   
            Debug.LogWarning($"stat name : {type} doesn't exist in the stats dictionary");
            stat = 0;
        }
        return stat;
    }

    public SerializableDictionary<StatType, int> GetAllStats()
    {
        return statsData;
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

    private void StatsBehaviour_OnIncreaseStat(object sender, StatSlotEventArgs eventArgs)
    {
        Increase(eventArgs.statData.type);
    }
    private void StatsBehaviour_OnReduceStat(object sender, StatSlotEventArgs eventArgs)
    {
        Reduce(eventArgs.statData.type);
    }


    //ITakeFromFile
    public void LoadData(GameData data)
    {
        this.statsData = data.globals.statsData;
    }
}
