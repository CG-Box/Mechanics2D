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
    Intelligence,
    Appearance,
    Manipulation,
    Perception,
    Charisma
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
    [SerializeField]private StringEventChannelSO infoShowTranslateRequest = default;
    [SerializeField]private NoteEventChannelSO addNoteRequest = default;

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

    void Start()
    {
        //TODO: it's can be race condition if Dialogue not exist or not init
        SendDataToDialogue();
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

        SendDataToDialogue();

        //add note to chronicles
        addNoteRequest.RaiseEvent(new Note(NoteType.Stat, $"You increase {type} by {amount} points"));
    }
    public void Reduce(StatType type)
    {
        if(stats.dict[type] > 0)
        {
            stats.dict[type] -= 1;
            statsChangeEvent.RaiseEvent(new StatData(type, stats.dict[type]));

            SendDataToDialogue();
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

    public void AddPoint()
    {
        AddPoint(1);
    }
    public void AddPoint(int amount)
    {
        stats.points += amount;
        pointsChangeEvent.RaiseEvent(stats.points);

        //add note to chronicles
        addNoteRequest.RaiseEvent(new Note(NoteType.Stat, $"You get {amount} stats points"));
    }

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
            infoShowTranslateRequest.RaiseEvent("NOT_ENOUGH_POINTS");
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

    //TODO: need rework for something more robust
    void SendDataToDialogue()
    {
        Dictionary<string, object> dialogueStats = new Dictionary<string, object>();
        foreach(KeyValuePair<StatType, int> variable in stats.dict)
        {
            dialogueStats[variable.Key.ToString()] = stats.dict[variable.Key];

            //dialogueStats["pokemon_name"] = "Pikachu";
        }
        DialogueManager.GetInstance().SetVariableState(dialogueStats);
    }


    //ITakeFromFile
    public void LoadData(GameData data)
    {
        this.stats = data.globals.stats;
    }
}
