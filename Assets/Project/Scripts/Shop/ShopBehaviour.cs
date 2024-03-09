using UnityEngine;
using UnityEngine.Events;

public class ShopBehaviour : MonoBehaviour
{
    [SerializeField] private SlotManager slotManager;

    [SerializeField] private Wrap<int> money;

    [SerializeField] private ShopPanel shopPanel;

	[Header("Events Raise")]
    [SerializeField] private IntEventChannelSO moneyChangeRequest;
    [SerializeField] private ItemDataEventChannelSO addItemRequest;

    [Header("Shop Items")]
    [SerializeField] private Item_SO[] shopItems;

    public UnityEvent OnBuy;
    public UnityEvent OnNotMoney;

    void Awake()
    {
        LoadData(slotManager.GetActiveSlot().data);
        shopPanel.SetItems(shopItems);
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

    void ShopBehaviour_OnBuyItem(object sender, ItemSlotEventArgs eventArgs)
    {
        if(eventArgs.itemData.amount <= 0) return;

        int itemPrice = 100;
        int totalPrice = eventArgs.itemData.amount * itemPrice;
        if(money.value < totalPrice)
        {
            OnNotMoney?.Invoke();
        }
        else
        {
            moneyChangeRequest.RaiseEvent(-totalPrice);

            // TO DO: change to work with any InventoryBehaviour (not only player)
            InventoryBehaviour playerInventory = FindObjectOfType<PlayerMovement>().GetComponent<InventoryBehaviour>();
            ItemEventArg itemEventArg = new ItemEventArg(eventArgs.itemData, playerInventory);
            addItemRequest.RaiseEvent(itemEventArg);
            OnBuy?.Invoke();
        }
    }



    //ITakeFromFile
    public void LoadData(GameData data)
    {
        this.money = data.globals.money;
    }
}
