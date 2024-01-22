using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameData_SO", menuName = "SriptableObjects/SlotManager", order = 3)]
public class SlotManager: ScriptableObject
{
    [Header("Debugging")]
    [Tooltip("Load data from store or use values from SO")]
    [SerializeField] private bool disableSavingFiles = false;
    [SerializeField] private bool disableLoadingFiles = false;

    [Header("Slots")]
    [SerializeField]
    private string activeSlotId;
    public List<GameData_SO> dataSlots;

    [SerializeField]
    private GameData_SO defaultSlot;

    [SerializeField]
    private GameData_SO activeSlot;

    private FileDataHandler dataHandler;

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;
    
    [Header("Events Raise")]
	public TransitionPointEventChannelSO sceneRequestEventChannel = default;

    void OnEnable()
    {}
    void OnDisable()
    {}

    //Called one by EventsBinder every time the game starts
    public void PrepareSlots()
    {
        InitFileHandler();
        UnbindActiveSlot();
        CheckForSlots();
        LoadAllSlots();
    }

    void CheckForSlots()
    {
        if(defaultSlot == null) Debug.LogError($"defaultSlot must be defined");
        if(dataSlots.Count == 0) Debug.LogError($"At least one slot must be in collection");
    }

    void RestoreActiveSlotToDefault()
    {
        activeSlot.CopyData(defaultSlot.GetData());
    }
    public void NewGame(string slotId)
    {
        GameData_SO newGameSlot = GetSlotById(slotId);
        SetActiveSlot(newGameSlot.SlotId);
        RestoreActiveSlotToDefault();

        activeSlot.AddDefaultItems();

        sceneRequestEventChannel.RaiseEvent(
			new Mechanics2D.TransitionPointData(activeSlot.data.globals.lastSceneName)
		);
    }
    public void ContinueGame()
    {
        string recentSlotId = dataHandler.GetMostRecentlyUpdatedProfileId();

        //GameData_SO lastUpdatedSlot = GetLastUpdatedSlot();
        if(recentSlotId == null)
        {
            Debug.LogError("Now saved slots found, may be new game need to be started");
            return;
        }
        LoadGame(recentSlotId);
    }
    public void LoadGame(string slotId)
    {
        SetActiveSlot(slotId);
        sceneRequestEventChannel.RaiseEvent(
			new Mechanics2D.TransitionPointData(activeSlot.data.globals.lastSceneName)
		);
    }

    public void SetActiveSlot(string slotId)
    {
        bool slotFound = false;
        for (int i = 0; i < dataSlots.Count; i++)
        {
            if (dataSlots[i].SlotId.Equals(slotId))
            {
                activeSlot = dataSlots[i];
                activeSlotId = slotId;
                slotFound = true;
                activeSlot.Init();
            }
        }
        if(!slotFound) Debug.LogError($"Slot with id : {slotId} doesn't exist ");
    }

    public void UnbindActiveSlot()
    {
        activeSlot = null;
        activeSlotId = null;
    }

    public GameData_SO GetSlotById(string slotId)
    {
        GameData_SO targetSlot = null;
        for (int i = 0; i < dataSlots.Count; i++)
        {
            if(slotId == dataSlots[i].SlotId)
            {
                targetSlot = dataSlots[i];
                break;
            } 
        }
        if(targetSlot == null)
            Debug.LogError($"slot with id: {slotId} wasn't find");
        return targetSlot;
    }

    public GameData_SO GetLastUpdatedSlot()
    {
        Debug.LogError("not working change it");
        // CHANGE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        long lastUpdate = 0;
        GameData_SO lastUpdatedSlot = null;
        for (int i = 0; i < dataSlots.Count; i++)
        {
            if(dataSlots[i].data == null) continue;
            if(lastUpdate > dataSlots[i].data.globals.lastUpdated)
            {
                lastUpdatedSlot = dataSlots[i];
                lastUpdate = dataSlots[i].data.globals.lastUpdated;
            } 
        }

        return lastUpdatedSlot;// ?? defaultSlot;
    }
    public GameData_SO GetActiveSlot()
    {
        return activeSlot;
    }


    void InitFileHandler()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
    }

    public bool HasFilesData() 
    {
        return GetRecentSlotId() != null;
    }

    public void DeleteProfileData(string profileId) 
    {
        SetActiveSlot(profileId);
        // delete the data for this profile id
        dataHandler.Delete(profileId);
        // initialize the selected profile id
        RestoreActiveSlotToDefault();
    }

    void SaveSlot(GameData_SO slot)
    {
        if(disableSavingFiles == true)
        {
            Debug.LogWarning("Slots not saving/loading to file, because disableUseFiles is active");
            return;
        }

        if(slot == null)
        {
            Debug.LogError("Can't save empty slot / slot is null");
            return;
        }

        // if we don't have any data to save, log a warning here
        if (slot.data == null) 
        {
            Debug.LogWarning($"No data was found in saving slot : {slot?.SlotId} ");
            return;
        }

        // timestamp the data so we know when it was last saved
        slot.data.globals.lastUpdated = System.DateTime.Now.ToBinary();

        // save that data to a file using the data handler
        dataHandler.Save(slot.data, slot.SlotId);
    }
    void LoadSlot(GameData_SO slot)
    {
        if(disableLoadingFiles == true)
        {
            Debug.LogWarning("Slots not saving/loading to file, because disableUseFiles is active");
            return;
        }

        if(slot == null)
        {
            Debug.LogError("Can't save empty slot / slot is null");
            return;
        }

        // load any saved data from a file using the data handler
        slot.SetData(dataHandler.Load(slot.SlotId));

        // start a new game if the data is null and we're configured to initialize data for debugging purposes
        if (slot.data == null) 
        {
            //NewGame();
            Debug.LogError($"loaded slot: {slot.SlotId} was never saved before");
            return;
        }

        // if no data can be loaded, don't continue
        if (slot.data == null) 
        {
            Debug.Log("No data was found. A New Game needs to be started before data can be loaded.");
            return;
        }


        //slot.data.scene.name = dataHandler.currentSceneName;
    }

    void LoadAllSlots()
    {
        foreach(GameData_SO currentSlot in dataSlots)
        {
            LoadSlot(currentSlot);
        }
    }

    public void SaveActiveSlot()
    {
        //string name = activeSlot?.SlotId;
        SaveSlot(activeSlot);
    }
    public void LoadActiveSlot()
    {
        //string name = activeSlot?.SlotId;
        LoadSlot(activeSlot);
    }
    public bool LoadRecentSlot()
    {
        string recentSlotId = GetRecentSlotId();
        if(recentSlotId == null)
        {
            Debug.LogError("Slots weren't saved before, the default slot needs to be loaded");
            return false;
        }
        Debug.Log($"recentSlotId: {recentSlotId}");
        SetActiveSlot(recentSlotId);
        LoadSlot(activeSlot);
        return true;
    }
    string GetRecentSlotId()
    {
        return dataHandler.GetMostRecentlyUpdatedProfileId();        
    }

    public Dictionary<string, GameData> GetAllProfilesGameData() 
    {
        return dataHandler.LoadAllProfiles();
    }

    public void ChangeActiveSlotSceneName(string sceneName)
    {
        activeSlot.data.globals.lastSceneName = sceneName;
        Debug.Log($"active slot {activeSlot.SlotId} new name {sceneName}");
    }

    public void ChangePlayerHealth(int newHealt)
    {
        activeSlot.data.globals.playerHealth = newHealt;
        Debug.Log($"newHealt : {newHealt}");
    }


    //Not used
    public string GetMostRecentSceneName()
    {
        return activeSlot.data.globals.lastSceneName;
    }
}
