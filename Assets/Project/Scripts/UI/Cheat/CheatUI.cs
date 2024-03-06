using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheatUI : TogglePanel
{
    [Header("Items")]
    [SerializeField]private GameObject itemsGameObject;
    [SerializeField]private GameObject itemButtonGO;

    [Header("Channels")]
    [SerializeField] private ItemDataEventChannelSO addItemRequest = default;

    InventoryBehaviour playerInventory;

    void Start()
    {
        GetPlayerInventory();
        FillItems();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Toggle();
        }
    }

    void GetPlayerInventory()
    {
        playerInventory = FindObjectOfType<PlayerMovement>().GetComponent<InventoryBehaviour>();
    }
    void FillItems()
    {
        foreach (string name in System.Enum.GetNames(typeof(ItemType)))
        {
            Button button = Instantiate(itemButtonGO, itemsGameObject.transform).GetComponent<Button>();
            button.GetComponentInChildren<TextMeshProUGUI>(true).text = name;
            button.onClick.AddListener(() => {
                ItemType type = (ItemType)System.Enum.Parse(typeof(ItemType), name);
                ItemEventArg itemEventArg = new ItemEventArg(new ItemData(type), playerInventory);
                addItemRequest.RaiseEvent(itemEventArg);
            });
        }
        itemButtonGO.SetActive(false);
    }
    
}
