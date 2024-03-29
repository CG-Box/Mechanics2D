using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class InventoryBehaviour : MonoBehaviour , ITakeFromFile
{
    private Inventory inventory;

    [SerializeField]
    private Transform itemPrefab;

    [SerializeField]
    private ItemsLibrary itemsDatabase;

    [SerializeField]
    private InventoryPanel inventoryPanel;

    
	[Header("Events Raise")]
	public GameObjectEventChannelSO inventoryUpdateEvent = default;
    [SerializeField]private NoteEventChannelSO addNoteRequest = default;

    [Header("Events Listen")]
    public ItemDataEventChannelSO addItemRequest = default;
    public ItemDataEventChannelSO removeItemRequest = default;

	[Header("Unity Events")]
    public UnityEvent OnKitKatSaveRaised;
    public UnityEvent OnUseTaco;
    public UnityEvent OnUseKey;

    void  Awake() 
    {
        inventory = new Inventory();   

        inventoryPanel.SetInventoryBehaviour(this);
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
        inventoryPanel.OnUseItem += InventoryBehaviour_OnUseItem;
        inventoryPanel.OnRemoveItem += InventoryBehaviour_OnRemoveItem;
    }
    void RemovePanelListeners()
    {
        inventoryPanel.OnUseItem -= InventoryBehaviour_OnUseItem;
        inventoryPanel.OnRemoveItem -= InventoryBehaviour_OnRemoveItem;
    }

    public List<ItemData> GetItems()
    {
        return inventory.GetItemList();
    }

    public bool ContainsItem(ItemData checkItem)
    {
        return inventory.ContainsItem(checkItem);
    }
    public bool ContainsItem(ItemData checkItem, int amount)
    {
        return inventory.ContainsItem(checkItem, amount);
    }

    public void UseItem(ItemData item)
    {
        switch(item.type)
        {
            case ItemType.Taco:
                inventory.RemoveItem(new ItemData(ItemType.Taco,1));
                if(inventory.DoesNotContain(item))
                {
                    //StaticEvents.Collecting.OnOutOfAmmo?.Invoke();
                }
                OnUseTaco?.Invoke();
                break;
            case ItemType.Jam:
                //float medkitHealth = 50f;
                //StaticEvents.PlayerHealth.OnRestoreHealth?.Invoke(medkitHealth, this.gameObject);
                inventory.RemoveItem(new ItemData(ItemType.Jam,1));
                break;
            case ItemType.Kat:
                //float medkitHealth = 50f;
                //StaticEvents.PlayerHealth.OnRestoreHealth?.Invoke(medkitHealth, this.gameObject);
                inventory.RemoveItem(new ItemData(ItemType.Kat,1));
                KitKatSave();
                break;
            case ItemType.Key_red:
            case ItemType.Key_blue:
            case ItemType.Key_green:
            case ItemType.Key_yellow:
                inventory.RemoveItem(item);
                OnUseKey?.Invoke();
                break;
            default:
               inventory.RemoveItem(item);
               break;
        }

        //add note to chronicles
        addNoteRequest.RaiseEvent(new Note(NoteType.Inventory, $"You used {item.type} in {item.amount} amount"));
    }
    public void UseItem(ItemData item, GameObject targetObject)
    {   
        if(gameObject == targetObject)
            UseItem(item);
    }
    public void RemoveItem(ItemData item, bool dropItem)
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

        if(dropItem)
        {        
            DropItem(item, transform.position);
        }

        //inventory.PrintInventoryToDebug();

        //add note to chronicles
        addNoteRequest.RaiseEvent(new Note(NoteType.Inventory, $"You lost {item.type} in {item.amount} amount"));
    }
    public void RemoveItem(ItemData item)
    {   
        RemoveItem(item, true);
    }
    public void RemoveItem(ItemData item, GameObject targetObject)
    {
        if(gameObject == targetObject)
            RemoveItem(item);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        /**
        ItemBehaviour collectable = collider.GetComponent<ItemBehaviour>();
        if(collectable != null)
        {
            ItemData collectedItem = collectable.Data;
            AddItem(collectedItem);
            collectable.DestroySelf();
        }
        */
    }

    public void AddItem(ItemData itemData)
    {
        inventory.AddItem(itemData);
        inventoryUpdateEvent.RaiseEvent(this.gameObject);

        //add note to chronicles
        addNoteRequest.RaiseEvent(new Note(NoteType.Inventory, $"You get {itemData.type} in {itemData.amount} amount"));
    }

    void InventoryBehaviour_AddItemRequest(ItemEventArg itemEventArg)
    {
        if(itemEventArg.inventoryBehaviour == this)
        {
            foreach(ItemData itemData in itemEventArg.items)
            {
                ItemData realItemData = ItemsLibrary.GetItem(itemData.type); //new ItemData(itemType)
                realItemData.amount = itemData.amount;
                AddItem(realItemData);
            }
        }
    }
    void InventoryBehaviour_RemoveItemRequest(ItemEventArg itemEventArg)
    {
        if(itemEventArg.inventoryBehaviour == this)
        {
            foreach(ItemData itemData in itemEventArg.items)
            {
                RemoveItem(itemData, false);
            }
        }
    }


    public ItemBehaviour SpawnItem(ItemData item, Vector3 position)
    {
        Transform itemTransform = Instantiate(itemPrefab, position, Quaternion.identity);

        ItemBehaviour collectable = itemTransform.GetComponent<ItemBehaviour>();
        collectable.SetItem(item);
        collectable.UpdateItemVisual();
        return collectable;   
    }

    public ItemBehaviour DropItem(ItemData item, Vector3 dropPosition)
    {
        float x = UnityEngine.Random.Range(-1f, 1f);
        float y = UnityEngine.Random.Range(-1f, 1f);
        Vector3 randomDir = new Vector3(x, y, 0f);
        randomDir = randomDir.normalized * 2f;

        return SpawnItem(item, dropPosition + randomDir );
    }

    private void InventoryBehaviour_OnUseItem(object sender, ItemSlotEventArgs eventArgs)
    {
        UseItem(eventArgs.itemData);

        inventoryUpdateEvent.RaiseEvent(this.gameObject);
    }
    private void InventoryBehaviour_OnRemoveItem(object sender, ItemSlotEventArgs eventArgs)
    {
        RemoveItem(eventArgs.itemData);

        inventoryUpdateEvent.RaiseEvent(this.gameObject);
    }

    //ITakeFromFile
    public void LoadData(GameData data)
    {
        inventory.LoadItemList(data.globals.itemList);
    }

    
	void KitKatSave()
	{
		if (OnKitKatSaveRaised != null)
			OnKitKatSaveRaised.Invoke();
	}
}
