using UnityEngine;
using System;

[RequireComponent(typeof(SpriteRenderer))]
public class ItemBehaviour : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [Header("Require Item_SO")]

    [SerializeField]
    private Item_SO itemToGenerate;

    [Tooltip("If amount is 0 item will be generated with default amount")]
    [SerializeField]
    private int amount = 0;

    ItemData itemData;
    public ItemData Data
    {
        get { return itemData; }
    }

    //public Action BeforeDestroyFunction;

    void Awake()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        GenerateItemByItem_SO(itemToGenerate);
        UpdateItemVisual();
    }

    [ContextMenu("GenerateItem")]
    public void GenerateItemByItem_SO()
    {
        GenerateItemByItem_SO(itemToGenerate);
    }
    public void GenerateItemByItem_SO(Item_SO item_SO)
    {
       if(item_SO)
       {
            SetItem(item_SO);
            if(amount != 0)
            {
                itemData.amount = amount;
            }
            UpdateItemVisual();
       }/*
       else
       {
            Debug.LogError("Can't generate item by empty ItemData_SO");
       }*/
    }

    public void SetItem(ItemData itemData)
    {
        this.itemData = itemData;
    }
    public void SetItem(Item_SO item_SO)
    {
        this.itemData = item_SO.Data;
    }
    public void UpdateItemVisual()
    {
        spriteRenderer.sprite = itemData.sprite;
    }

    public void DestroySelf()
    {
        //BeforeDestroyFunction?.Invoke();
        Destroy(gameObject);
    }
}
