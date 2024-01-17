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


    [SerializeField]
    private int health;
    public int Health
    {
        get { return health; }
    }

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
        this.data = gameData;
    }

    public void Init()
    {
        UpdateHealthUI();
    }

    public void InitDefaultGameData() 
    {
        InitDefaultHealth();
        InitDefaultInventory();
    }
    public void InitDefaultHealth()
    {
        health = 100;
    }
    public void InitDefaultInventory()
    {
        ownedItemsAmount = 0;
        ownedItemsId = new List<string>(ownedItemsAmount);
    }

    public void AddHealth(int amount)
    {
        health += amount;
        UpdateHealthUI();
    }
    public void RemoveHealth(int amount)
    {
        health -= amount;
        UpdateHealthUI();
    }
    public void UpdateHealthUI()
    {
        //StaticEvents.Tickets.UpdateUI?.Invoke(health);
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
