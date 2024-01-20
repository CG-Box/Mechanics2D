using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Mechanics2D
{}
public class SlotGroup : MonoBehaviour
{
    [Header("Content")]
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;
    [SerializeField] private TextMeshProUGUI percentageCompleteText;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Buttons")]
    
    [SerializeField] private Button emptySlotButton;
    [SerializeField] private Button saveSlotButton;
    [SerializeField] private Button clearButton;

    [Header("Profile")]
            
    [SerializeField] private GameData_SO connectedSlot;

    public bool hasData { get; private set; } = false;

    public event EventHandler<string> OnSelectSlot;
    public event EventHandler<string> OnClearSlot;
    public event EventHandler<string> OnNewGameSlot;

    //Double click fix
    float clickRateTime = 0.5f;
    float nextClickTime = 0;

    #region EventsBinding
    void OnEnable()
    {
        saveSlotButton.onClick.AddListener(SlotButtonClick);
        clearButton.onClick.AddListener(ClearButtonClick);
    }
    void OnDisable()
    {
        saveSlotButton.onClick.RemoveListener(SlotButtonClick);
        clearButton.onClick.RemoveListener(ClearButtonClick);
    }
    #endregion

    public void UpdateVisualByData(GameData data) 
    {
        // there's no data for this profileId
        if (data == null) 
        {
            ShowEmptyUI();
        }
        // there is data for this profileId
        else 
        {
            ShowContentUI();

            percentageCompleteText.text = data.GetPercentageComplete() + "% COMPLETE";
            healthText.text = "Health:  " + data.globals.playerHealth;
        }
    }

    void ShowEmptyUI()
    {
        hasData = false;
        noDataContent.SetActive(true);
        hasDataContent.SetActive(false);
        clearButton.gameObject.SetActive(false);
    }
    void ShowContentUI()
    {
        hasData = true;
        noDataContent.SetActive(false);
        hasDataContent.SetActive(true);
        clearButton.gameObject.SetActive(true);
    }

    public string GetProfileId() 
    {
        return this.connectedSlot.SlotId;
    }

    public void SetInteractable(bool interactable)
    {
        saveSlotButton.interactable = interactable;
        clearButton.interactable = interactable;
        emptySlotButton.interactable = interactable;
    }

    public void SelectSlot(bool invokeEvent = true)
    {
        if(invokeEvent)
            OnSelectSlot?.Invoke(this, connectedSlot.SlotId);
    }
    public void SlotButtonClick()
    {
        TryButtonClick(() => { SelectSlot(); });
    }
    public void ClearSlot(bool invokeEvent = true)
    {
        if(invokeEvent)
            OnClearSlot?.Invoke(this, connectedSlot.SlotId);
    }
    public void ClearButtonClick()
    {
        TryButtonClick(() => { ClearSlot(); });
    }

    public void NewGameSlot(bool invokeEvent = true)
    {
        if(invokeEvent)
            OnNewGameSlot?.Invoke(this, connectedSlot.SlotId);
    }
    public void EmptyButtonClick()
    {
        TryButtonClick(() => { NewGameSlot(); });
    }


    public bool TryButtonClick(Action voidFunction)
    {
        if(Time.time > nextClickTime)
        {
            nextClickTime = Time.time + clickRateTime;
            voidFunction();
            return true;
        }
        return false;
    }
}