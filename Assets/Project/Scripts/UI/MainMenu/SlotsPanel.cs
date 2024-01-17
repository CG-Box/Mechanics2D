using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlotsPanel : MonoBehaviour
{
    [Header("References components")]
    [SerializeField] private SlotManager slotManager;
    [SerializeField] private ConfirmationPopup confirmationPopup;

    [SerializeField] private SlotGroup[] saveSlots;

    private bool isLoadingGame = false;

    void OnEnable()
    {
        CheckForSlots();
        AddSlotsListeners();
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
    }
    private void ClearSlotEvents(SlotGroup slotGroup)
    {
        slotGroup.OnClearSlot -= OnClearClicked;
        slotGroup.OnSelectSlot -= OnSaveSlotClicked;
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
            slotManager.SetActiveSlot(saveSlotId);
            SaveGameAndLoadScene();
        }
        // case - new game, but the save slot has data
        else if (saveSlot.hasData) 
        {
            confirmationPopup.ActivateMenu(
                "Starting a New Game with this slot will override the currently saved data. Are you sure?",
                // function to execute if we select 'yes'
                () => {
                    slotManager.SetActiveSlot(saveSlotId);
                    slotManager.NewGame();
                    SaveGameAndLoadScene();
                },
                // function to execute if we select 'cancel'
                () => {
                    this.ActivateMenu(isLoadingGame);
                }
            );
        }
        // case - new game, and the save slot has no data
        else 
        {
            slotManager.SetActiveSlot(saveSlotId);
            slotManager.NewGame();
            SaveGameAndLoadScene();
        }
    }

    private void SaveGameAndLoadScene() 
    {
        // save the game anytime before loading a new scene
        //slotManager.SaveActiveSlot();
        // load the scene

        /*
        string sceneName = DataPersistenceManager.instance.FirstSceneName;
        if(isLoadingGame)
        {
            //loading scene from file
            sceneName = slotManager.GetMostRecentSceneName();
        }
        SceneManager.LoadSceneAsync(sceneName);*/
    }

    public void OnClearClicked(object sender, string saveSlotId) 
    {
        SlotGroup saveSlot = (SlotGroup)sender;

        DisableMenuButtons();

        confirmationPopup.ActivateMenu(
            "Are you sure you want to delete this saved data?",
            // function to execute if we select 'yes'
            () => {
                slotManager.DeleteProfileData(saveSlotId);
                ActivateMenu(isLoadingGame);
            },
            // function to execute if we select 'cancel'
            () => {
                ActivateMenu(isLoadingGame);
            }
        );
    }

    public void ActivateMenu(bool isLoadingGame) 
    {
        // set this menu to be active
        this.gameObject.SetActive(true);

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

    public void DeactivateMenu() 
    {
        this.gameObject.SetActive(false);
    }

    void DisableMenuButtons() 
    {
        foreach (SlotGroup saveSlot in saveSlots) 
        {
            saveSlot.SetInteractable(false);
        }
    }
}