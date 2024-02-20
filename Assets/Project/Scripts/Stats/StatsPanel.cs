using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using TMPro;

public class StatsPanel : MonoBehaviour
{
    [SerializeField]private Transform itemsContainer;
    [SerializeField]private Transform itemTemplate;

    [Header("Events Listen")]
    [SerializeField]private StatDataEventChannelSO statsChangeEvent = default;

    private StatsBehaviour statsBehaviour;

    const int slotsMaxAmount = 10;

    List<InventorySlot> slotsList = new List<InventorySlot>( new InventorySlot[slotsMaxAmount]);

    
    public event EventHandler OnAddPoint;
    public event EventHandler OnRemovePoint;

    public event EventHandler<StatSlotEventArgs> OnIncreaseStat;
    public event EventHandler<StatSlotEventArgs> OnReduceStat;

    public void SetStatsBehaviour(StatsBehaviour statsBehaviour)
    {
        this.statsBehaviour = statsBehaviour;
    }

    void OnEnable()
    {
        if (statsChangeEvent != null)
		    statsChangeEvent.OnEventRaised += StatsUpdateEvent_Handler;
    }
    void OnDisable()
    {
        if (statsChangeEvent != null)
		    statsChangeEvent.OnEventRaised -= StatsUpdateEvent_Handler;
    }


    void StatsUpdateEvent_Handler(StatData statData)
    {
        Refresh();
    }

    void Start()
    {
        Refresh();
    }

    void Refresh()
    {
        foreach(Transform child in itemsContainer)
        {
            StatSlot statSlotOld = child.gameObject.GetComponent<StatSlot>();
            RemoveSlotListeners(statSlotOld);
            Destroy(child.gameObject);
        }
        
        SerializableDictionary<StatType, int> statsDict = statsBehaviour.GetAllStats();

        foreach(KeyValuePair<StatType, int> stat in statsDict) 
        {
            RectTransform statRectTransform = Instantiate(itemTemplate, itemsContainer).GetComponent<RectTransform>();
            //statRectTransform.gameObject.SetActive(true);
            StatSlot statSlot = statRectTransform.gameObject.GetComponent<StatSlot>();
            statSlot.SetSlotData(new StatData(stat.Key, stat.Value));
            statSlot.UpdateSlotVisual();
            AddSlotListeners(statSlot);
        }
    }

    private void AddSlotListeners(StatSlot statSlot)
    {
        statSlot.OnIncreaseSlot += StatPanel_OnIncreaseSlot;
        statSlot.OnReduceSlot += StatPanel_OnReduceSlot;
    }
    private void RemoveSlotListeners(StatSlot statSlot)
    {
        statSlot.OnIncreaseSlot -= StatPanel_OnIncreaseSlot;
        statSlot.OnReduceSlot -= StatPanel_OnReduceSlot;
    }

    private void StatPanel_OnIncreaseSlot(object sender, StatSlotEventArgs eventArgs)
    {
        OnIncreaseStat?.Invoke(sender, new StatSlotEventArgs(eventArgs.statData));
    }
    private void StatPanel_OnReduceSlot(object sender, StatSlotEventArgs eventArgs)
    {
        OnReduceStat?.Invoke(sender, new StatSlotEventArgs(eventArgs.statData));
    }


}
