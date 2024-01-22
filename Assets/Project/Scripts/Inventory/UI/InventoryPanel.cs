using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using TMPro;

public class InventoryPanel : MonoBehaviour
{

    private Inventory inventory;

    [SerializeField]private Transform itemsContainer;
    [SerializeField]private Transform itemTemplate;

    private InventoryBehaviour inventoryBehaviour;

    const int slotsMaxAmount = 21;

    List<InventorySlot> slotsList = new List<InventorySlot>( new InventorySlot[slotsMaxAmount]);

    
    public event EventHandler<ItemSlotEventArgs> OnUseItem;
    public event EventHandler<ItemSlotEventArgs> OnRemoveItem;

    public void SetInventoryBehaviour(InventoryBehaviour inventoryBehaviour)
    {
        this.inventoryBehaviour = inventoryBehaviour;
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += InventoryPanel_OnListChanged;

        Refresh();
    }

    private void InventoryPanel_OnListChanged(object sender, System.EventArgs e)
    {
        Refresh();
    }

    private void Refresh()
    {
        foreach(Transform child in itemsContainer)
        {
            InventorySlot inventorySlot = child.gameObject.GetComponent<InventorySlot>();
            RemoveSlotListeners(inventorySlot);
            Destroy(child.gameObject);
        }
        
        foreach(ItemData item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemTemplate, itemsContainer).GetComponent<RectTransform>();
            //itemSlotRectTransform.gameObject.SetActive(true);
            InventorySlot inventorySlot = itemSlotRectTransform.gameObject.GetComponent<InventorySlot>();
            inventorySlot.SetSlotItem(item);
            inventorySlot.UpdateSlotVisual();
            AddSlotListeners(inventorySlot);
        }
    }

    private void AddSlotListeners(InventorySlot inventorySlot)
    {

        inventorySlot.OnUseSlotItem += InventoryPanel_OnUseSlotItem;
        inventorySlot.OnRemoveSlotItem += InventoryPanel_OnRemoveSlotItem;
    }
    private void RemoveSlotListeners(InventorySlot inventorySlot)
    {

        inventorySlot.OnUseSlotItem -= InventoryPanel_OnUseSlotItem;
        inventorySlot.OnRemoveSlotItem -= InventoryPanel_OnRemoveSlotItem;
    }

    private void InventoryPanel_OnUseSlotItem(object sender, ItemSlotEventArgs eventArgs)
    {
        //StaticEvents.Collecting.OnItemUse?.Invoke(eventArgs.itemData, inventoryBehaviour.gameObject);
        
        OnUseItem?.Invoke(sender, new ItemSlotEventArgs(eventArgs.itemData));
    }
    private void InventoryPanel_OnRemoveSlotItem(object sender, ItemSlotEventArgs eventArgs)
    {
        //StaticEvents.Collecting.OnItemRemove?.Invoke(eventArgs.itemData, inventoryBehaviour.gameObject);

        OnRemoveItem?.Invoke(sender, new ItemSlotEventArgs(eventArgs.itemData));
    }

}
