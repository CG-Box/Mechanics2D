using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameData_SO", menuName = "SriptableObjects/SlotManager", order = 3)]
public class SlotManager: ScriptableObject
{
    [Header("Debugging")]
    [Tooltip("Load data from store or use values from SO")]
    [SerializeField] private bool disableUseFiles = false;

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
    

    void OnEnable()
    {
        InitFileHandler();
        UnbindActiveSlot();
        CheckForSlots();
    }
    void OnDisable()
    {}

    void CheckForSlots()
    {
        if(defaultSlot == null) Debug.LogError($"defaultSlot must be defined");
        if(dataSlots.Count == 0) Debug.LogError($"At least one slot must be in collection");
    }

    void ChangeActiveSlotWithDefault()
    {
        activeSlot = defaultSlot;
    }

    public void SetActiveSlot(string slodId)
    {
        bool slotFound = false;
        for (int i = 0; i < dataSlots.Count; i++)
        {
            if (dataSlots[i].SlotId.Equals(slodId))
            {
                activeSlot = dataSlots[i];
                activeSlotId = slodId;
                slotFound = true;
                activeSlot.Init();
            }
        }
        if(!slotFound) Debug.LogError($"Slot with id : {slodId} doesn't exist ");
    }

    public void UnbindActiveSlot()
    {
        activeSlot = null;
        activeSlotId = null;
    }

    public GameData_SO GetLastUpdatedSlot()
    {
        long lastUpdate = 0;
        GameData_SO lastUpdatedSlot = null;
        for (int i = 0; i < dataSlots.Count; i++)
        {
            if(lastUpdate < dataSlots[i].Health)
            {
                lastUpdatedSlot = dataSlots[i];
                lastUpdate = dataSlots[i].Health;
            } 
        }

        return lastUpdatedSlot ?? defaultSlot;
    }
    public GameData_SO GetActiveSlot()
    {
        return activeSlot;
    }


    void InitFileHandler()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
    }
    void InitializeSelectedSlotId()
    {
        this.activeSlotId = dataHandler.GetMostRecentlyUpdatedProfileId();
    }

    void SaveSlot(GameData_SO slot)
    {
        if(disableUseFiles == true)
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
        if(disableUseFiles == true)
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


    public void SaveActiveSlot()
    {
        string name = activeSlot?.SlotId;
        SaveSlot(activeSlot);
    }
    public void LoadActiveSlot()
    {
        string name = activeSlot?.SlotId;
        LoadSlot(activeSlot);
    }
}
