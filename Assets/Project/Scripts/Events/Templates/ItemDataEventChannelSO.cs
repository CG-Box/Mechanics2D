using UnityEngine.Events;
using UnityEngine;

/// <summary>
/// This class is used for Events that add\remove item from inventory.
/// Example: Add item to the inventory.
/// </summary>

[CreateAssetMenu(menuName = "Events/ItemEventArg Event Channel")]
public class ItemDataEventChannelSO : DescriptionBaseSO
{
	public UnityAction<ItemEventArg> OnEventRaised;
	
	public void RaiseEvent(ItemEventArg value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(value);
	}
}

public class ItemEventArg: System.EventArgs
{
	public ItemData[] items;
	public InventoryBehaviour inventoryBehaviour;

	public ItemEventArg(ItemData itemData, InventoryBehaviour inventoryBehaviour)
	{
		this.inventoryBehaviour = inventoryBehaviour;
		items = new ItemData[1];
		items[0] = itemData;
	}
	public ItemEventArg(ItemData[] itemsData, InventoryBehaviour inventoryBehaviour)
	{
		this.inventoryBehaviour = inventoryBehaviour;
		items = new ItemData[itemsData.Length];
		for(int i = 0; i < itemsData.Length; i++)
		{
			items[i] = itemsData[i];
		}
	}
}

