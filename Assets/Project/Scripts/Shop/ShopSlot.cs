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

    public event EventHandler<ItemSlotEventArgs> OnBuySlot;

    void Awake()
    {
        itemData.amount = 1;
    }

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


    public void SetSlot(ItemData itemData)
    {
        this.itemData = itemData;
    }
    public void UpdateSlotVisual()
    {
        image.sprite = ItemsLibrary.GetItem(itemData.type).sprite;
        UpdateAmount();
    }

    void UpdateAmount()
    {
        amountText.text = itemData.amount.ToString();
    }

    void OnBuyClick()
    {
        OnBuySlot?.Invoke(this, new ItemSlotEventArgs(this.itemData));
	}
    void OnPlusClick()
    {
        this.itemData.amount++;

        UpdateAmount();
    }
    void OnMinusClick()
    {
        this.itemData.amount--;
        if(itemData.amount < 0) itemData.amount = 0;

        UpdateAmount();
    }
}
