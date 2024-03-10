using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private TextMeshProUGUI priceText;
    [SerializeField]
    private TextMeshProUGUI amountText;

    [SerializeField]
    private Button buyButton;
    [SerializeField]
    private Button plusButton;
    [SerializeField]
    private Button minusButton;

    ItemData itemData;
    int price;

    public event EventHandler<ItemSlotEventArgs> OnBuySlot;

    void Awake()
    {
        itemData.amount = 1;
    }

    #region BindEvents
    void OnEnable()
    {
        buyButton.onClick.AddListener(OnBuyClick);

        plusButton.onClick.AddListener(OnPlusClick);
        minusButton.onClick.AddListener(OnMinusClick);
    }
    void OnDisable()
    {
        buyButton.onClick.RemoveListener(OnBuyClick);
        
        plusButton.onClick.RemoveListener(OnPlusClick);
        minusButton.onClick.RemoveListener(OnMinusClick);
    }
    #endregion


    public void SetItem(ItemData itemData)
    {
        this.itemData = itemData;
    }
    public void SetPrice(int price)
    {
        this.price = price;
    }

    #region UpdateVisaul
    public void UpdateSlotVisual()
    {
        image.sprite = ItemsLibrary.GetItem(itemData.type).sprite;
        UpdateAmount();
        UpdatePrice();
    }

    void UpdateAmount()
    {
        amountText.text = itemData.amount.ToString();
    }
    void UpdatePrice()
    {
        priceText.text = (price * itemData.amount).ToString()+"$";
    }
    #endregion

    #region Clicks
    void OnBuyClick()
    {
        OnBuySlot?.Invoke(this, new ItemSlotEventArgs(this.itemData));
	}
    void OnPlusClick()
    {
        this.itemData.amount++;

        UpdateAmount();
        UpdatePrice();
    }
    void OnMinusClick()
    {
        this.itemData.amount--;
        if(itemData.amount < 1) itemData.amount = 1;

        UpdateAmount();
        UpdatePrice();
    }
    #endregion
}
