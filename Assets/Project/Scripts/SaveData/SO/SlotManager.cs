using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameData_SO", menuName = "SriptableObjects/SlotManager", order = 3)]
public class SlotManager: ScriptableObject
{
    [SerializeField]
    private string activeSlotId;
    public List<GameData_SO> dataSlots;

    [SerializeField]
    private GameData_SO defaultSlot;

    [SerializeField]
    private GameData_SO activeSlot;


    void OnEnable()
    {
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
}
