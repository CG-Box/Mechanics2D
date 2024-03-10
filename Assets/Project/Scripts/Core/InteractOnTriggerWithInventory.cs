using UnityEngine;

public abstract class InteractOnTriggerWithInventory : InteractOnTrigger
{
    protected InventoryBehaviour inventoryBehaviour;
    
    public bool InventoryInZone { get { return inventoryBehaviour != null; } }

    protected override void ExecuteOnEnter(Collider2D other)
    {
        inventoryBehaviour = other.GetComponent<InventoryBehaviour>();
        base.ExecuteOnEnter(other);
    }
    protected override void ExecuteOnExit(Collider2D other)
    {
        base.ExecuteOnExit(other);
        inventoryBehaviour = null;
    }
}
