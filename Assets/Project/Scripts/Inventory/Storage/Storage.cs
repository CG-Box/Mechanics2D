using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO: need replacement to interface or abstract class
public class Storage : MonoBehaviour, ITakeFromFile
{
    private Inventory inventory;

    [SerializeField]
    private ItemsLibrary itemsDatabase;

    [SerializeField]
    private InventoryPanel inventoryPanel;

    [Header("Events Raise")]
	public GameObjectEventChannelSO inventoryUpdateEvent = default;

    [Header("Events Listen")]
    public ItemDataEventChannelSO addItemRequest = default;
    public ItemDataEventChannelSO removeItemRequest = default;

    void  Awake() 
    {
        inventory = new Inventory();   

        //TODO: rework to inteface or abstract
        //inventoryPanel.SetInventoryBehaviour(this);
        inventoryPanel.SetInventory(inventory);
    }

    void OnEnable()
    {
        AddPanelListeners();

        if (addItemRequest != null)
			addItemRequest.OnEventRaised += InventoryBehaviour_AddItemRequest;
        if (removeItemRequest != null)
			removeItemRequest.OnEventRaised += InventoryBehaviour_RemoveItemRequest;
    }  
    void OnDisable()
    {
        RemovePanelListeners();

        if (addItemRequest != null)
			addItemRequest.OnEventRaised -= InventoryBehaviour_AddItemRequest;
        if (removeItemRequest != null)
			removeItemRequest.OnEventRaised -= InventoryBehaviour_RemoveItemRequest;
    }

    void AddPanelListeners()
    {
        //inventoryPanel.OnUseItem += InventoryBehaviour_OnUseItem;
        inventoryPanel.OnRemoveItem += InventoryBehaviour_OnRemoveItem;
    }
    void RemovePanelListeners()
    {
        //inventoryPanel.OnUseItem -= InventoryBehaviour_OnUseItem;
        inventoryPanel.OnRemoveItem -= InventoryBehaviour_OnRemoveItem;
    }

    public void RemoveItem(ItemData item)
    {
        if(inventory.DoesNotContain(item))
        {
            Debug.Log($"cant remove item {item.type} because it is not exist in inventory");
            return;
        }

        inventory.RemoveItem(item);
        if(inventory.DoesNotContain(item))
        {
            //StaticEvents.Collecting.OnOutOfAmmo?.Invoke();
            Debug.Log($"OnOutOf {item.type} ???");
        }
        //DropItem(item, transform.position);

        //Debug.Log($"Remove {item.type}");
    }
    public void RemoveItem(ItemData item, GameObject targetObject)
    {
        if(gameObject == targetObject)
            RemoveItem(item);
    }

    public void AddItem(ItemData itemData)
    {
        inventory.AddItem(itemData);
        inventoryUpdateEvent.RaiseEvent(this.gameObject);
    }

    void InventoryBehaviour_AddItemRequest(ItemEventArg itemEventArg)
    {
        if(itemEventArg.inventoryBehaviour == this)
        {
            foreach(ItemData itemData in itemEventArg.items)
            {
                AddItem(itemData);
            }
        }
    }
    void InventoryBehaviour_RemoveItemRequest(ItemEventArg itemEventArg)
    {
        if(itemEventArg.inventoryBehaviour == this)
        {
            foreach(ItemData itemData in itemEventArg.items)
            {
                RemoveItem(itemData);
            }
        }
    }

    private void InventoryBehaviour_OnRemoveItem(object sender, ItemSlotEventArgs eventArgs)
    {
        RemoveItem(eventArgs.itemData);

        inventoryUpdateEvent.RaiseEvent(this.gameObject);
    }


    //ITakeFromFile
    public void LoadData(GameData data)
    {
        Debug.Log("Storage LoadData");
        //inventory.LoadItemList(data.globals.itemList);
    }
}
