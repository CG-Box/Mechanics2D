using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ItemBehaviour : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField] 
    private GameObject tooltipGO;

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

    InventoryBehaviour nearInventoryBehaviour = null;

    bool generateFromItem_SO = true;

    //public Action BeforeDestroyFunction;

    void OnEnable()
    {
        HideTip();
        ClearInventoryBehaviour();
    }

    void Start()
    {
        if(generateFromItem_SO)
        {
            GenerateItemByItem_SO(itemToGenerate);
        }
    }

    public void TryTakeItem()
	{
        if (nearInventoryBehaviour != null) 
        {
            nearInventoryBehaviour.AddItem(itemData);
            DestroySelf();
        }
        else 
        {
            //Debug.Log("inventory is too far");
        }
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
        generateFromItem_SO = false;
    }
    public void SetItem(Item_SO item_SO)
    {
        SetItem(item_SO.Data);
    }
    public void UpdateItemVisual()
    {
        spriteRenderer.sprite = ItemsLibrary.GetItem(itemData.type).sprite;
    }

    public void DestroySelf()
    {
        //BeforeDestroyFunction?.Invoke();
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void SetInventoryBehaviour(InventoryBehaviour inventoryBehaviour)
    {
        this.nearInventoryBehaviour = inventoryBehaviour;
    }
    void ClearInventoryBehaviour()
    {
        this.nearInventoryBehaviour = null;
    }

    void ShowTip()
    {
        tooltipGO.SetActive(true);
    }
    void HideTip()
    {
        tooltipGO.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collider) 
    {

        InventoryBehaviour inventoryBehaviour = collider.GetComponent<InventoryBehaviour>();
        if(inventoryBehaviour != null)
        {
            SetInventoryBehaviour(inventoryBehaviour);
            ShowTip();
        }

        /*
        if (collider.gameObject.tag == Constants.PlayerTag)
        {
            playerInRange = true;
        }*/
    }

    void OnTriggerExit2D(Collider2D collider) 
    {
        InventoryBehaviour inventoryBehaviour = collider.GetComponent<InventoryBehaviour>();
        if(inventoryBehaviour == nearInventoryBehaviour)
        {
            ClearInventoryBehaviour();
            HideTip();
        }

        /*
        if (collider.gameObject.tag == Constants.PlayerTag)
        {
            playerInRange = false;
        }*/
    }
}
