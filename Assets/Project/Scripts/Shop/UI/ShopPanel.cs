using System;
using UnityEngine;

public class ShopPanel : TogglePanel
{
    [Header("Items")]
    [SerializeField]private Transform itemsContainer;
    [SerializeField]private Transform itemTemplate;

    Item_SO[] shopItems;
    ShopPrices shopPrices;

    public event EventHandler<ItemSlotEventArgs> OnBuyItem;

    void OnEnable()
    {}
    void OnDisable()
    {}

    void Start()
    {
        Refresh();
    }


    public void SetItems(Item_SO[] items)
    {
        this.shopItems = items;
    }
    public void SetPrices(ShopPrices shopPrices)
    {
        this.shopPrices = shopPrices;
    }
    public void Refresh()
    {
        foreach(Transform child in itemsContainer)
        {
            var slot = child.gameObject.GetComponent<ShopSlot>();
            RemoveSlotListeners(slot);
            Destroy(child.gameObject);
        }

        foreach(Item_SO item in shopItems)
        {
            RectTransform itemSlotRectTransform = Instantiate(itemTemplate, itemsContainer).GetComponent<RectTransform>();
            //itemSlotRectTransform.gameObject.SetActive(true);
            var slot = itemSlotRectTransform.gameObject.GetComponent<ShopSlot>();
            slot.SetItem(item.Data);
            slot.SetPrice(shopPrices[item.Data.type]);
            slot.UpdateSlotVisual();
            AddSlotListeners(slot);
        }
    }

    void AddSlotListeners(ShopSlot slot)
    {
        slot.OnBuySlot += ShopPanel_OnBuySlot;
    }
    void RemoveSlotListeners(ShopSlot slot)
    {
        slot.OnBuySlot -= ShopPanel_OnBuySlot;
    }

    void ShopPanel_OnBuySlot(object sender, ItemSlotEventArgs eventArgs)
    {
        OnBuyItem?.Invoke(this, eventArgs);
    }
}
