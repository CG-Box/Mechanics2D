using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class StatSlot : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI slotInfoText;

    [SerializeField]
    private Button reduceButton;

    [SerializeField]
    private Button increaseButton;

    StatData statData;

    public event EventHandler<StatSlotEventArgs> OnIncreaseSlot;
    public event EventHandler<StatSlotEventArgs> OnReduceSlot;


    void OnEnable()
    {
        reduceButton.onClick.AddListener(OnReduceClick);
        increaseButton.onClick.AddListener(OnIncreaseClick);
    }
    void OnDisable()
    {
        reduceButton.onClick.RemoveListener(OnReduceClick);
        increaseButton.onClick.RemoveListener(OnIncreaseClick);
    }

    void OnReduceClick()
    {
        OnReduceSlot?.Invoke(this, new StatSlotEventArgs(this.statData));
    }
    void OnIncreaseClick()
    {
        OnIncreaseSlot?.Invoke(this, new StatSlotEventArgs(this.statData));
    }

    public void SetSlotData(StatData statData)
    {
        this.statData = statData;
    }

    public void UpdateSlotVisual()
    {
        slotInfoText.SetText($"{statData.type} : {statData.value}");
    }

    public void DisableButtons()
    {
        increaseButton.interactable = false;
        reduceButton.interactable = false;
        //increaseButton.enabled = false;
        //reduceButton.enabled = false;

    }
    public void EnableButtons()
    {
        increaseButton.interactable = true;
        reduceButton.interactable = true;
        //increaseButton.enabled = true;
        //reduceButton.enabled = true;
    }
}

public class StatSlotEventArgs : EventArgs
{
    public StatData statData;
    public StatSlotEventArgs(StatData statData)
    {
        this.statData = statData;
    }
}

