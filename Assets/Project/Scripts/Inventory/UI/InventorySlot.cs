using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlot : MonoBehaviour
{

    [SerializeField]
    private Image slotImage;

    [SerializeField]
    private TextMeshProUGUI slotStackText;

    [SerializeField]
    private Button removeButton;

    [SerializeField]
    private Button useButton;

    ItemData itemData;
    
    public event EventHandler<ItemSlotEventArgs> OnUseSlotItem;
    public event EventHandler<ItemSlotEventArgs> OnRemoveSlotItem;


    void OnEnable()
    {
        removeButton.onClick.AddListener(OnRemoveClick);
        useButton.onClick.AddListener(OnUseClick);
    }
    void OnDisable()
    {
        removeButton.onClick.RemoveListener(OnRemoveClick);
        useButton.onClick.RemoveListener(OnUseClick);
    }

    /*
    void Awake()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("Mouse was clicked outside");
    }*/

    void OnRemoveClick()
    {
        OnRemoveSlotItem?.Invoke(this, new ItemSlotEventArgs(this.itemData));
	}
    void OnUseClick()
    {
        OnUseSlotItem?.Invoke(this, new ItemSlotEventArgs(this.itemData));
	}

    public void HideControlButtons()
    {
        transform.parent.gameObject.SetActive(false);
    }


    public void SetSlotItem(ItemData itemData)
    {
        this.itemData = itemData;
    }
    public void UpdateSlotVisual()
    {
        slotImage.sprite = ItemsLibrary.GetItem(itemData.type).sprite;
        if(itemData.amount > 1)
        {
            slotStackText.SetText(itemData.amount.ToString());
        }
        else
        {
            slotStackText.SetText("");
        }
    }
}

public class ItemSlotEventArgs : EventArgs
{
    public ItemData itemData;
    public ItemSlotEventArgs(ItemData itemData)
    {
        this.itemData = itemData;
    }
}
