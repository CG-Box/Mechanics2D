using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameData_SO", menuName = "ScriptableObjects/GameData_SO", order = 2)]
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
        AddDefaultItems();
    }
    public void InitDefaultHealth()
    {
        //health = 100;
    }
    public void AddDefaultItems()
    {
        Debug.Log("AddDefaultItems");
        data.globals.itemList.Add(ItemsLibrary.GetItem(ItemType.Jam));
        ItemData cucakes = ItemsLibrary.GetItem(ItemType.Cupkake);
        cucakes.amount = 5;
        data.globals.itemList.Add(cucakes);
    }

    public void OwnInventoryItem(ItemType type)
    {
        data.globals.itemList.Add(ItemsLibrary.GetItem(type));
    }
    public void LoseInventoryItem(ItemType type)
    {
        for (int i = 0; i <  data.globals.itemList.Count; i++)
        {
            ItemData currentItem = data.globals.itemList[i];
            if (currentItem.type.Equals(type)) data.globals.itemList.RemoveAt(i);   
        }
    }
    public void ResetToDefault()
    {
        InitDefaultGameData();
    }

    //void OnValidate(){} //Every change in SO
}
