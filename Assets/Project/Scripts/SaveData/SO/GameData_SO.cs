using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameData_SO", menuName = "SriptableObjects/GameData_SO", order = 2)]
public class GameData_SO: ScriptableObject
{
    [SerializeField]
    private string slotId = "default";
    public string SlotId
    {
        get { return slotId; }
    }

    [Space (10)]
    [Header ("======Data variables======")]
    [Space (20)]

    public GameData data;

    int ownedItemsAmount;
    public List<string> ownedItemsId;

    void OnEnable()
    {
        //StaticEvents.Tickets.Added += AddHealth;
        //StaticEvents.LevelUI.LevelCompleted += TrySetHighestCompletedLevel;

        //Init();
    }
    void OnDisable()
    {
        //StaticEvents.Tickets.Added -= AddHealth;
        //StaticEvents.LevelUI.LevelCompleted -= TrySetHighestCompletedLevel;
    }

    public GameData GetData()
    {
        return data;
    }
    public void SetData(GameData gameData)
    {
        //Debug.LogWarning("Don't use set data if you are not loading from file - use copy data to avoid errors");
        this.data = gameData;
    }
    public void CopyData(GameData gameData)
    {
        this.data = gameData.Copy();
    }

    public void Init()
    {}

    public void InitDefaultGameData() 
    {
        InitDefaultHealth();
        InitDefaultInventory();
    }
    public void InitDefaultHealth()
    {
        //health = 100;
    }
    public void InitDefaultInventory()
    {
        ownedItemsAmount = 0;
        ownedItemsId = new List<string>(ownedItemsAmount);
    }

    public void OwnInventoryItem(string id)
    {
        ownedItemsId.Add(id);
        ownedItemsAmount = ownedItemsId.Count;
    }
    public void LoseInventoryItem(string id)
    {
        for (int i = 0; i < ownedItemsId.Count; i++)
        {
            if (ownedItemsId[i].Equals(id)) ownedItemsId.RemoveAt(i);   
        }
        ownedItemsAmount = ownedItemsId.Count;
    }
    public void ResetToDefault()
    {
        InitDefaultGameData();
    }

    //void OnValidate(){} //Every change in SO
}
