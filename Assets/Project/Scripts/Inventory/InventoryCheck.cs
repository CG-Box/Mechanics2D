using UnityEngine;
using UnityEngine.Events;

public class InventoryCheck : InteractOnTrigger
{
    [SerializeField] private ItemType type;
    [SerializeField] private int amount = 1;

    [SerializeField] private bool autoRemove;

    [Header("Events Raise")]
    public ItemDataEventChannelSO removeItemRequest = default;

    public UnityEvent OnHasItem, OnNoItem, OnPassCheck;

    InventoryBehaviour inventoryBehaviour;
    bool inventoryInZone = false;

    bool HasItem()
    {
        return inventoryBehaviour.ContainsItem(new ItemData(type), amount);
    }

    protected override void ExecuteOnEnter(Collider2D other)
    {
        inventoryBehaviour = other.GetComponent<InventoryBehaviour>();
        inventoryInZone = true;
        base.ExecuteOnEnter(other);
    }
    protected override void ExecuteOnExit(Collider2D other)
    {
        base.ExecuteOnExit(other);
        inventoryBehaviour = null;
        inventoryInZone = false;
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
        if(!inventoryInZone) return;

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
