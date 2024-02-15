using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlotsPanel : MenuPanel
{
    [Header("References components")]
    [SerializeField] private SlotManager slotManager;
    [SerializeField] private ConfirmationPopup confirmationPopup;

    [Header("Events Raise")]
	[SerializeField] private VoidEventChannelSO fileDeletedEventChannel = default;

    [SerializeField] private SlotGroup[] saveSlots;

    private bool isLoadingGame = false;


    void OnEnable()
    {
        CheckForSlots();
        AddSlotsListeners();

        ShowAnimation();
        SelectDefaultButton();
    }
    void OnDisable()
    {
        RemoveSlotsListeners();
    }

    void CheckForSlots()
    {
        if(saveSlots.Length == 0)
        {
            Debug.LogError("Slots must be connected to slots panel");
        }
    }

    #region EventsBinding
    void AddSlotsListeners()
    {
        foreach(SlotGroup slotGroup in saveSlots)
        {
            BindSlotEvents(slotGroup);
        }
    }
    void RemoveSlotsListeners()
    {
        foreach(SlotGroup slotGroup in saveSlots)
        {
            ClearSlotEvents(slotGroup);
        }
    }
    private void BindSlotEvents(SlotGroup slotGroup)
    {
        slotGroup.OnClearSlot += OnClearClicked;
        slotGroup.OnSelectSlot += OnSaveSlotClicked;
        slotGroup.OnNewGameSlot += OnSaveSlotClicked;
    }
    private void ClearSlotEvents(SlotGroup slotGroup)
    {
        slotGroup.OnClearSlot -= OnClearClicked;
        slotGroup.OnSelectSlot -= OnSaveSlotClicked;
        slotGroup.OnNewGameSlot -= OnSaveSlotClicked;
    }
    #endregion

    public void OnSaveSlotClicked(object sender, string saveSlotId) 
    {
        SlotGroup saveSlot = (SlotGroup)sender;

        // disable all buttons
        DisableMenuButtons();

        // case - loading game
        if (isLoadingGame) 
        {
            LoadFromSlot(saveSlotId);
        }
        // case - new game, but the save slot has data
        else if (saveSlot.hasData) 
        {
            TryOverwriteSlot(saveSlotId);
        }
        // case - new game, and the save slot has no data
        else 
        {
            WriteToSlot(saveSlotId);
        }
    }

    void LoadFromSlot(string slotId)
    {
        slotManager.LoadGame(slotId);
    }
    void TryOverwriteSlot(string slotId)
    {
        confirmationPopup.ActivateMenu(
            //"Starting a New Game with this slot will override the currently saved data. Are you sure?",
            Translator.Instance["SLOT_OVERRIDE"],
            // function to execute if we select 'yes'
            () => {
                WriteToSlot(slotId);
            },
            // function to execute if we select 'cancel'
            () => {
                this.ActivateMenu(isLoadingGame);
            }
        );
    }
    void WriteToSlot(string slotId)
    {
        slotManager.NewGame(slotId);
    }

    public void OnClearClicked(object sender, string saveSlotId) 
    {
        SlotGroup saveSlot = (SlotGroup)sender;

        DisableMenuButtons();

        confirmationPopup.ActivateMenu(
            //"Are you sure you want to delete this saved data?",
            Translator.Instance["SLOT_DELETE"],
            // function to execute if we select 'yes'
            () => {
                slotManager.DeleteProfileData(saveSlotId);
                ActivateMenu(isLoadingGame);
                fileDeletedEventChannel.RaiseEvent();
            },
            // function to execute if we select 'cancel'
            () => {
                ActivateMenu(isLoadingGame);
            }
        );
    }

    public void ActivateMenu(bool isLoadingGame) 
    {
        // set mode
        this.isLoadingGame = isLoadingGame;

        // load all of the profiles that exist
        Dictionary<string, GameData> profilesGameData = slotManager.GetAllProfilesGameData();

        foreach (SlotGroup saveSlot in saveSlots) 
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);
            saveSlot.UpdateVisualByData(profileData);
            if (profileData == null && isLoadingGame) 
            {
                saveSlot.SetInteractable(false);
            }
            else 
            {
                saveSlot.SetInteractable(true);
            }
        }
    }

    void DisableMenuButtons() 
    {
        foreach (SlotGroup saveSlot in saveSlots) 
        {
            saveSlot.SetInteractable(false);
        }
    }
}