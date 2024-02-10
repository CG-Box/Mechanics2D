using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//using Mechanics2D;

[RequireComponent(typeof(Collider2D))]
public class InteractOnTrigger : MonoBehaviour
{
    public LayerMask layers;
    public UnityEvent OnEnter, OnExit;
    new Collider2D collider;
    //public InventoryController.InventoryChecker[] inventoryChecks;

    void Reset()
    {
        layers = LayerMask.NameToLayer(Constants.DefaultLayer);
        collider = GetComponent<Collider2D>();
        collider.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (0 != (layers.value & 1 << other.gameObject.layer))
        {
            ExecuteOnEnter(other);
        }
    }

    protected virtual void ExecuteOnEnter(Collider2D other)
    {
        OnEnter.Invoke();
        /*for (var i = 0; i < inventoryChecks.Length; i++)
        {
            inventoryChecks[i].CheckInventory(other.GetComponentInChildren<InventoryController>());
        }*/
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (0 != (layers.value & 1 << other.gameObject.layer))
        {
            ExecuteOnExit(other);
        }
    }

    protected virtual void ExecuteOnExit(Collider2D other)
    {
        OnExit.Invoke();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "InteractionTrigger", false);
    }

    void OnDrawGizmosSelected()
    {
        //need to inspect events and draw arrows to relevant gameObjects.
    }

} 