using UnityEngine;
using System;

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/Effect Manager", order = 99)]
public class EffectManager : DescriptionBaseSO
{
    [SerializeField] private IntEventChannelSO HealEvent;
    [SerializeField] private IntEventChannelSO moneyChangeRequest = default;
    [SerializeField] private ItemDataEventChannelSO addItemRequest = default;
    [SerializeField] private IntEventChannelSO addPointsRequest = default;
    [SerializeField] private StatDataEventChannelSO statsChangeRequest = default;
    [SerializeField] private NoteEventChannelSO addNoteRequest = default;

    static EffectManager selfStatic;

    //public static EffectManager SelfStatic { get {return selfStatic;} }

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

    public static void Heal(HealEffect effectData)
    {
        selfStatic.HealEvent.RaiseEvent(effectData.Amount);
    }
    public static void AddMoney(MoneyAddEffect effectData)
    {
        selfStatic.moneyChangeRequest.RaiseEvent(effectData.Amount);
    }
    public static void AddItem(AddItemEffect effectData)
    {
           // TO DO: change to work with any InventoryBehaviour (not only player)
        InventoryBehaviour playerInventory = FindObjectOfType<PlayerMovement>().GetComponent<InventoryBehaviour>();
		ItemEventArg itemEventArg = new ItemEventArg(effectData.Items, playerInventory);
		selfStatic.addItemRequest.RaiseEvent(itemEventArg);
    }

    public static void AddStatPoint(AddStatPointEffect effectData)
    {
        selfStatic.addPointsRequest.RaiseEvent(effectData.Amount);
    }

    public static void StatIncrease(StatIncreaseEffect effectData)
    {
        selfStatic.statsChangeRequest.RaiseEvent(new StatData(effectData.Type, effectData.Amount));
    }

    public static void AddNote(NoteAddEffect effectData)
    {
        selfStatic.addNoteRequest.RaiseEvent(new Note(effectData.Type, effectData.Text));
    }
}
