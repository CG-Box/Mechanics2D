using Ink.Runtime;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "InkExternalFunctions", menuName = "SriptableObjects/InkExternalFunctions", order = 7)]
public class InkExternalFunctions: DescriptionBaseSO
{
    enum Function
    {
        Log,
        AddItem,
        RemoveItem,
        AddMoney,
        TakeQuest,
        ShowInfo,
        AddPoints
    }

    Action<string> LogAction;

	[Header("Events Raise")]
    [SerializeField] private ItemDataEventChannelSO addItemRequest = default;
    [SerializeField] private ItemDataEventChannelSO removeItemRequest = default;
    [SerializeField] private StringEventChannelSO infoShowRequest = default;
    [SerializeField] private IntEventChannelSO addPointsRequest = default;

    public void Bind(Story story)
    {
        LogAction = Log;

        //story.BindExternalFunction(nameof(Function.Log), (string text) => Log(text));

        story.BindExternalFunction(nameof(Function.Log), LogAction);

        story.BindExternalFunction(nameof(Function.AddItem), (int itemId) => {
            RemoveOrAddItem(itemId, addItemRequest);
            Debug.Log($"Add item {(ItemType)itemId} from dialogue");
        });
        story.BindExternalFunction(nameof(Function.RemoveItem), (int itemId) => {
            RemoveOrAddItem(itemId, removeItemRequest);
            Debug.Log($"Remove item {(ItemType)itemId} from dialogue");
        });
        story.BindExternalFunction(nameof(Function.AddMoney), (int amount) => {
            Debug.Log($"Add money {amount}");
        });
        story.BindExternalFunction(nameof(Function.TakeQuest), (string questName) => {
            Debug.Log($"Take quest {questName}");
        });
        story.BindExternalFunction(nameof(Function.ShowInfo), (string text) => {
            infoShowRequest.RaiseEvent(text);
        });
        story.BindExternalFunction(nameof(Function.AddPoints), (int amount) => {
            addPointsRequest.RaiseEvent(amount);
        });
    }
    public void Unbind(Story story)
    {
        story.UnbindExternalFunction(nameof(Function.Log));
        story.UnbindExternalFunction(nameof(Function.AddItem));
        story.UnbindExternalFunction(nameof(Function.RemoveItem));
        story.UnbindExternalFunction(nameof(Function.AddMoney));
        story.UnbindExternalFunction(nameof(Function.TakeQuest));
        story.UnbindExternalFunction(nameof(Function.ShowInfo));
    }

    void RemoveOrAddItem(int itemId, ItemDataEventChannelSO itemDataEventChannelSO)
    {
        ItemType itemType = (ItemType)itemId;
        ItemData itemData = new ItemData(itemType); //ItemsLibrary.GetItem(itemType);

        // TO DO: change to work with any InventoryBehaviour (not only player)
        InventoryBehaviour playerInventory = FindObjectOfType<PlayerMovement>().GetComponent<InventoryBehaviour>();
        ItemEventArg itemEventArg = new ItemEventArg(itemData, playerInventory);
        itemDataEventChannelSO.RaiseEvent(itemEventArg);
    }

    void Log(string str)
    {
        Debug.Log($"story log: {str}");
    }
}
