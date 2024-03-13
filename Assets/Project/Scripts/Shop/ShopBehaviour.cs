using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class ShopBehaviour : InteractOnTriggerWithInventory
{
    [SerializeField] private SlotManager slotManager;

    [SerializeField] private ShopPrices shopPrices;

    //[SerializeField] private float sellMultiplier = 1;

    [SerializeField] private Wrap<int> money;

    [SerializeField] private ShopPanel shopPanel;

	[Header("Events Raise")]
    [SerializeField] private IntEventChannelSO moneyChangeRequest;
    [SerializeField] private ItemDataEventChannelSO addItemRequest;

    [Header("Shop Items")]
    [SerializeField] private Item_SO[] shopItems;

    //[SerializeField] private Item_SO[] notSellingItems;

    [Header("Unity Events")]
    public UnityEvent OnBuy;
    public UnityEvent OnNotMoney;

    void Awake()
    {
        LoadData(slotManager.GetActiveSlot().data);
        shopPanel.SetItems(shopItems);
        shopPanel.SetPrices(shopPrices);
        //shopPanel.Refresh(); //it's refresh on start
    }

    void OnEnable()
    {
        AddPanelListeners();
    }
    void OnDisable()
    {
        RemovePanelListeners();
    }

    void AddPanelListeners()
    {
        shopPanel.OnBuyItem += ShopBehaviour_OnBuyItem;
    }
    void RemovePanelListeners()
    {
        shopPanel.OnBuyItem -= ShopBehaviour_OnBuyItem;
    }

    /*
    public void ToSell()
    {
        Debug.Log("ToSell");
        List<ItemData> inventoryItems = inventoryBehaviour.GetItems();
        //shopPanel.SetItems(inventoryItems);   
    }
    public void ToBuy()
    {
        Debug.Log("ToBuy");
        shopPanel.SetItems(shopItems);
        shopPanel.Refresh(); //it's refresh on start
    }*/

    void ShopBehaviour_OnBuyItem(object sender, ItemSlotEventArgs eventArgs)
    {
        int itemPrice = shopPrices[eventArgs.itemData.type];
        int totalPrice = eventArgs.itemData.amount * itemPrice;
        if(money.value < totalPrice)
        {
            OnNotMoney?.Invoke();
        }
        else
        {
            moneyChangeRequest.RaiseEvent(-totalPrice);

            if(inventoryBehaviour == null){ Debug.LogError("inventoryBehaviour is null"); return; }

            ItemEventArg itemEventArg = new ItemEventArg(eventArgs.itemData, inventoryBehaviour);
            addItemRequest.RaiseEvent(itemEventArg);
            OnBuy?.Invoke();
        }
    }

    public void TryOpenShop()
    {
        if(InventoryInZone)
        {
            shopPanel.Toggle();
        }
        else
        {
            //Debug.Log("player not zone");
        }
    }

    //ITakeFromFile
    public void LoadData(GameData data)
    {
        this.money = data.globals.money;
    }
}
