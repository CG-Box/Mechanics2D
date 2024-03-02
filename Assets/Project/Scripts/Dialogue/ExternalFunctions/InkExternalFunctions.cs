using Ink.Runtime;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "InkExternalFunctions", menuName = "ScriptableObjects/Dialogue/InkExternalFunctions")]
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
        AddPoints,
        PlaySound,
    }

    Action<string> LogAction;

	[Header("Events Raise")]
    [SerializeField] private ItemDataEventChannelSO addItemRequest = default;
    [SerializeField] private ItemDataEventChannelSO removeItemRequest = default;
    [SerializeField] private StringEventChannelSO infoShowRequest = default;
    [SerializeField] private IntEventChannelSO addPointsRequest = default;
    [SerializeField] private IntEventChannelSO moneyChangeRequest = default;

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
            moneyChangeRequest.RaiseEvent(amount);
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
        story.BindExternalFunction(nameof(Function.PlaySound), (string name) => {
            Debug.Log($"Playing sound: {name}");
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
        story.UnbindExternalFunction(nameof(Function.PlaySound));
    }

    void RemoveOrAddItem(int itemId, ItemDataEventChannelSO itemDataEventChannelSO)
    {
        ItemType itemType = (ItemType)itemId;
        ItemData itemData = ItemsLibrary.GetItem(itemType); //new ItemData(itemType);

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
