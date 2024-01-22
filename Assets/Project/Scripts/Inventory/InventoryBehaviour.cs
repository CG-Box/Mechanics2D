using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Events;

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

	[Header("Unity Events")]
    public UnityEvent OnKitKatSaveRaised;

    void  Awake() 
    {
        inventory = new Inventory();   

        inventoryPanel.SetInventoryBehaviour(this);
        inventoryPanel.SetInventory(inventory);
    }

    void OnEnable()
    {
        AddPanelListeners();
        //StaticEvents.Spawning.OnSpawnItem += DropItem;
    }  
    void OnDisable()
    {
        RemovePanelListeners();
        //StaticEvents.Spawning.OnSpawnItem -= DropItem;
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

    public void UseItem(ItemData item)
    {
        switch(item.type)
        {
            default:
            case ItemType.Taco:
                inventory.RemoveItem(item);
                if(inventory.DoesNotContain(item))
                {
                    //StaticEvents.Collecting.OnOutOfAmmo?.Invoke();
                }
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
        }
        Debug.Log($"Using {item.type}");
    }
    public void UseItem(ItemData item, GameObject targetObject)
    {   
        if(gameObject == targetObject)
            UseItem(item);
    }
    public void RemoveItem(ItemData item)
    {
        inventory.RemoveItem(item);
        if(inventory.DoesNotContain(item))
        {
            //StaticEvents.Collecting.OnOutOfAmmo?.Invoke();
            Debug.Log("OnOutOfAmmo ???");
        }
        DropItem(item, transform.position);
        //inventory.PrintInventoryToDebug();

        //Debug.Log($"Remove {item.type}");
    }
    public void RemoveItem(ItemData item, GameObject targetObject)
    {
        if(gameObject == targetObject)
            RemoveItem(item);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        ItemBehaviour collectable = collider.GetComponent<ItemBehaviour>();
        if(collectable != null)
        {
            ItemData collectedItem = collectable.Data;
            inventory.AddItem(collectedItem);
            collectable.DestroySelf();

            inventoryUpdateEvent.RaiseEvent(this.gameObject);
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
