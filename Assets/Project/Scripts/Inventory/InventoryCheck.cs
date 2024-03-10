using UnityEngine;
using UnityEngine.Events;

public class InventoryCheck : InteractOnTriggerWithInventory
{
    [SerializeField] private ItemType type;
    [SerializeField] private int amount = 1;

    [SerializeField] private bool autoRemove;

    [Header("Events Raise")]
    public ItemDataEventChannelSO removeItemRequest = default;

    public UnityEvent OnHasItem, OnNoItem, OnPassCheck;

    bool HasItem()
    {
        return inventoryBehaviour.ContainsItem(new ItemData(type), amount);
    }


    public void Check()
    {
        if(HasItem())
        {
            OnHasItem.Invoke();
            if(autoRemove)
            {
                RemoveTarget();
            }
        }
        else
        {
            OnNoItem.Invoke();
        }
    }
    public void RemoveTarget()
    {
        ItemEventArg itemEventArg = new ItemEventArg(new ItemData(type, amount), inventoryBehaviour);
		removeItemRequest.RaiseEvent(itemEventArg);
        OnPassCheck.Invoke();
    }
    public void TryRemoveTarget()
    {
        if(!InventoryInZone) return;

        if(HasItem())
        {
            Check();
            if(!autoRemove)  RemoveTarget();
        }
    }

    /*
    void GetInventory()
    {
        inventoryBehaviour = FindObjectOfType<PlayerMovement>().GetComponent<InventoryBehaviour>();
    }*/
}
