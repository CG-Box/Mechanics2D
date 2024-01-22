using UnityEngine;
using System.Collections.Generic;
using System;

public class Inventory
{    
    List<ItemData> itemList;

    public event EventHandler OnItemListChanged;

    public Inventory()
    {
        itemList = new List<ItemData>();
    }

    public void AddItem(ItemData newItem)
    {   
        if(newItem.canStack)
        {   
            bool alreadyExist = false;
            for (int i = 0; i < itemList.Count; i++)
            {
                ItemData currentItem = itemList[i];
                if(currentItem.type == newItem.type)
                {
                    alreadyExist = true;
                    currentItem.amount += newItem.amount;
                    itemList[i] = currentItem;
                }
            }

            if(!alreadyExist)
            {
                itemList.Add(newItem);
            }
        }
        else
        {
            itemList.Add(newItem);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);

        //PrintInventoryToDebug();
    }
    public void RemoveItem(ItemData delItem)
    {   
        if(delItem.canStack)
        {   
            for (int i = 0; i < itemList.Count; i++)
            {
                ItemData currentItem = itemList[i];
                if(currentItem.type == delItem.type)
                {
                    currentItem.amount -= delItem.amount;
                    itemList[i] = currentItem;
                    if(currentItem.amount <= 0)
                    {
                        itemList.Remove(currentItem);
                    }
                    break;
                }
            }
        }
        else
        {
            itemList.Remove(delItem);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);

        //PrintInventoryToDebug();
    }

    public bool ContainsItem(ItemData checkItem)
    {
        foreach(ItemData item in itemList)
        {
            if(item.type == checkItem.type)
                return true;
        }
        return false;
    }
    public bool DoesNotContain(ItemData checkItem)
    {
        return !ContainsItem(checkItem);
    }

    public void PrintInventoryToDebug()
    {
        Debug.Log("*** INVENTORY ***");
        foreach(ItemData item in itemList)
        {
            Debug.Log($"item type: {item.type} amount: {item.amount}");
        }
    }
    
    public List<ItemData> GetItemList()
    {
        return itemList;
    }
    public void LoadItemList(List<ItemData> loadItemList)
    {
        itemList = loadItemList;
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public void LoadItemListCopy(List<ItemData> loadItemList)
    {
        Debug.LogWarning("loadItemList clone doesn't works now =( )");
        //itemList = loadItemList.Clone();
        //OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
}
