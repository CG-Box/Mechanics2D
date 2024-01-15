using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameData_SO", menuName = "SriptableObjects/GameData_SO", order = 2)]
public class GameData_SO: ScriptableObject
{
    [SerializeField]
    [Tooltip("Load data from store or use values from SO")]
    private bool loadData = true;

    [SerializeField]
    private string slotId = "default";
    public string SlotId
    {
        get { return slotId; }
    }


    [Space (10)]
    [Header ("======Data variables======")]
    [Space (20)]


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

    public void Init()
    {
        if(loadData)
            SetCorrectData();
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

    public void SetCorrectData()
    {
        //Health
        if(PlayerPrefs.HasKey(nameof(health)))
        {
            GetHealthFromPrefs();
        }
        else
        {
            InitDefaultHealth();
        }
        //Inventory
        if(PlayerPrefs.HasKey(nameof(ownedItemsAmount)))
        {
            GetInventoryItemsFromPrefs();
        }
        else
        {
            InitDefaultInventory();
        }
    }

    public void GetHealthFromPrefs()
    {
        health = PlayerPrefs.GetInt(nameof(health));
    }
    public void GetGameDataFromPrefs()
    {
        GetHealthFromPrefs();
        GetInventoryItemsFromPrefs();
    }
    public void SaveGameDataToPrefs()
    {
        PlayerPrefs.SetInt(nameof(health), health);

        SaveInventoryToPrefs();

        PlayerPrefs.Save();
    }

    public void SaveInventoryToPrefs()
    {
        for (int i = 0; i < ownedItemsId.Count; i++) {
            PlayerPrefs.SetString($"{nameof(ownedItemsAmount)}_{i}", ownedItemsId[i]);
        }
        PlayerPrefs.SetInt(nameof(ownedItemsAmount), ownedItemsId.Count);
    }
    public void GetInventoryItemsFromPrefs()
    {
        ownedItemsAmount = PlayerPrefs.GetInt(nameof(ownedItemsAmount));
        ownedItemsId = new List<string>();
        for (int i = 0; i < ownedItemsAmount; i++) {
            ownedItemsId.Add(PlayerPrefs.GetString($"{nameof(ownedItemsAmount)}_{i}"));
        }
    }

    public void AddHealth(int amount)
    {
        health += amount;
        SaveGameDataToPrefs();
        UpdateHealthUI();
    }
    public void RemoveHealth(int amount)
    {
        health -= amount;
        SaveGameDataToPrefs();
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
        SaveInventoryToPrefs();
    }
    public void LoseInventoryItem(string id)
    {
        for (int i = 0; i < ownedItemsId.Count; i++)
        {
            if (ownedItemsId[i].Equals(id)) ownedItemsId.RemoveAt(i);   
        }
        ownedItemsAmount = ownedItemsId.Count;
        SaveInventoryToPrefs();
    }
    public void ResetToDefault()
    {
        DeleteAllPlayerPrefs();
        InitDefaultGameData();
    }
    
    [ContextMenu("DeleteAllPlayerPrefs")]
    public void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void GetData()
    {

    }
    public void SetData()
    {

    }

    //void OnValidate(){} //Every change in SO
}
