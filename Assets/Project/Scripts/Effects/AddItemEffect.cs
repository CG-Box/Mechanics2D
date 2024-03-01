using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/AddItem")]
public class AddItemEffect : Effect
{
    [SerializeField] private ItemDataEventChannelSO addItemRequest = default;
    
    [SerializeField] private ItemData[] items;

    public override void Apply()
    {
        base.Apply();


        // TO DO: change to work with any InventoryBehaviour (not only player)
        InventoryBehaviour playerInventory = FindObjectOfType<PlayerMovement>().GetComponent<InventoryBehaviour>();
		ItemEventArg itemEventArg = new ItemEventArg(items, playerInventory);
		addItemRequest.RaiseEvent(itemEventArg);
    }
}
