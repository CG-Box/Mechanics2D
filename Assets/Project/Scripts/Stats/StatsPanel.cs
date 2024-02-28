using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using TMPro;

public class StatsPanel : MonoBehaviour
{
    [SerializeField]private GameObject panelGameObject;
    [SerializeField]private Transform itemsContainer;
    [SerializeField]private Transform itemTemplate;

    [Header("Free Points")]
    [SerializeField]private TextMeshProUGUI pointsText;
    [SerializeField]private Button addPointButton;
    [SerializeField]private Button removePointButton;

    [Header("Events Raise")]
    [SerializeField]private ControlDataEventChannelSO inputControlChannel;

    StatsBehaviour statsBehaviour;
    
    public event EventHandler OnAddPoint;
    public event EventHandler OnRemovePoint;

    public event EventHandler<StatSlotEventArgs> OnIncreaseStat;
    public event EventHandler<StatSlotEventArgs> OnReduceStat;

    bool canOpen = true;

    public void SetStatsBehaviour(StatsBehaviour statsBehaviour)
    {
        this.statsBehaviour = statsBehaviour;
    }

    void OnEnable()
    {
        addPointButton.onClick.AddListener(OnAddPointClick);
        removePointButton.onClick.AddListener(OnRemovePointClick);
    }
    void OnDisable()
    {
        addPointButton.onClick.RemoveListener(OnAddPointClick);
        removePointButton.onClick.RemoveListener(OnRemovePointClick);
    }

    void OnAddPointClick()
    {
        OnAddPoint?.Invoke(this, EventArgs.Empty);
    }
    void OnRemovePointClick()
    {
        OnRemovePoint?.Invoke(this, EventArgs.Empty);
    }

    /*
    void StatsUpdateEvent_Handler(StatData statData)
    {
        Refresh();
    }*/

    void Start()
    {
        Refresh();
    }

    public void Show()
    {
        if(canOpen)
        {
            FreezePlayer();
            panelGameObject.SetActive(true);
        }
    }
    public void Hide()
    {
        panelGameObject.SetActive(false);
        UnfreezePlayer();
    }
    public void TogglePanel()
    {
        // if open
        if(panelGameObject.activeSelf)
        {
            Hide();
        }
        else // if close
        {
            Show();
        }
    }

    public void LockOpening()
    {
        /*CanvasGroup dialogCanvasGroup;
        dialogCanvasGroup.interactable = false;*/

        canOpen = false;
    }
    public void UnlockOpening()
    {
        canOpen = true;
    }

    public void UpdatePoints()
    {
        UpdatePoints(statsBehaviour.Points);
    }
    public void UpdatePoints(int pointsAmount)
    {
        pointsText.SetText($"Unused points : {pointsAmount}");
    }
    public void UpdateStats()
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
    public void Refresh()
    {
        UpdatePoints();
        UpdateStats();
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

    void FreezePlayer()
    {
        inputControlChannel.RaiseEvent(new ControlData(InputType.MovementAndUse,false));
    }
    void UnfreezePlayer()
    {
        inputControlChannel.RaiseEvent(new ControlData(InputType.MovementAndUse,true));
    }

    public void TryTogglePanel()
	{
        if(!canOpen) return;
        TogglePanel();
	}
}
