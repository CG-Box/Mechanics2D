using UnityEngine;
using System;

// not using

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/Effect Manager", order = 99)]
public class EffectManager : DescriptionBaseSO
{
    [SerializeField] private IntEventChannelSO healthChangeRequest = default;
    [SerializeField] private IntEventChannelSO moneyChangeRequest = default;
    [SerializeField] private ItemDataEventChannelSO addItemRequest = default;
    [SerializeField] private IntEventChannelSO addPointsRequest = default;
    [SerializeField] private StatDataEventChannelSO statsChangeRequest = default;
    [SerializeField] private NoteEventChannelSO addNoteRequest = default;

    static EffectManager selfStatic;

    //public static EffectManager SelfStatic { get {return selfStatic;} }


    //call init in BindEvents: EffectManager.Init(effectManager);
    public static void Init(EffectManager effectManager)
    {
        selfStatic = effectManager;
    }

    /*
    public static void UseEffect(Effect effect)
    {
        switch(effect)
        {
            case HealEffect healEffect:
                healEffect.Apply();
                break;
            case AddItemEffect addItemEffect:
                addItemEffect.Apply();
                break;
            default:
                Debug.Log("effect unknown");
                break;
        }
    }*/

    public static void Heal_Static(HealEffect effectData)
    {
        selfStatic.Heal(effectData.Amount);
    }
    public void Heal(int amount)
    {
        healthChangeRequest.RaiseEvent(amount);
    }
    public static void AddMoney_Static(MoneyAddEffect effectData)
    {
        selfStatic.AddMoney(effectData.Amount);
    }
    public void AddMoney(int amount)
    {
        moneyChangeRequest.RaiseEvent(amount);
    }
    public static void AddItem_Static(AddItemEffect effectData)
    {
        selfStatic.AddItem(effectData.Items);
    }
    public void AddItem(ItemData[] items)
    {
        // TO DO: change to work with any InventoryBehaviour (not only player)
        InventoryBehaviour playerInventory = FindObjectOfType<PlayerMovement>().GetComponent<InventoryBehaviour>();
		ItemEventArg itemEventArg = new ItemEventArg(items, playerInventory);
		selfStatic.addItemRequest.RaiseEvent(itemEventArg);
    }

    public static void AddStatPoint_Static(AddStatPointEffect effectData)
    {
        selfStatic.AddStatPoint(effectData.Amount);
    }
    public void AddStatPoint(int amount)
    {
        addPointsRequest.RaiseEvent(amount);
    }

    public static void StatIncrease_Static(StatIncreaseEffect effectData)
    {
        selfStatic.StatIncrease(effectData.Type, effectData.Amount);
    }
    public void StatIncrease(StatType type, int amount)
    {
        statsChangeRequest.RaiseEvent(new StatData(type, amount));
    }

    public static void AddNote_Static(NoteAddEffect effectData)
    {
        selfStatic.AddNote(effectData.Type, effectData.Text);
    }
    public void AddNote(NoteType type, string text)
    {
        addNoteRequest.RaiseEvent(new Note(type, text));
    }
}
