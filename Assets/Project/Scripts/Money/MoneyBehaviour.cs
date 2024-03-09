using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBehaviour : MonoBehaviour
{
    public int Money { get { return money.value; } }

    [SerializeField] private SlotManager slotManager;

    [SerializeField] private Wrap<int> money;

    [Header("Events Raise")]
    [SerializeField] private IntEventChannelSO moneyChangeEvent = default;
    [SerializeField] private StringEventChannelSO infoShowTranslateRequest = default;
    [SerializeField] private NoteEventChannelSO addNoteRequest = default;

    void OnEnable()
    {
        LoadData(slotManager.GetActiveSlot().data);

        AddPanelListeners();
    }
    void OnDisable()
    {
        RemovePanelListeners();
    }

    void Start()
    {
        moneyChangeEvent.RaiseEvent(money.value);
        //TODO: it's can be race condition if Dialogue not exist or not init
        SendDataToDialogue();
    }

    void AddPanelListeners()
    {
        //statsPanel.OnAddPoint += StatsBehaviour_OnAddPoint;
    }
    void RemovePanelListeners()
    {
        //statsPanel.OnAddPoint -= StatsBehaviour_OnAddPoint;
    }


    public void Add(int amount)
    {
        money.value += amount;
        moneyChangeEvent.RaiseEvent(money.value);

        SendDataToDialogue();

        //add note to chronicles
        addNoteRequest.RaiseEvent(new Note(NoteType.Money, $"You get {amount} money"));
    }
    public void Remove(int amount)
    {
        money.value -= amount;
        if(money.value < 0)
        {
            money.value = 0;
        }
        moneyChangeEvent.RaiseEvent(money.value);

        SendDataToDialogue();

        //add note to chronicles
        addNoteRequest.RaiseEvent(new Note(NoteType.Money, $"You spent {amount} money"));
    }
    public bool TryRemove(int amount)
    {
        if(money.value < amount)
        {   
            Debug.Log($"You don't have {amount} money, your current money {money.value}");
            //infoShowTranslateRequest.RaiseEvent("NOT_ENOUGH_POINTS");
            return false;
        }
        else
        {
            Remove(amount);
            return true;
        }
    }

    //TODO: need rework for something more robust
    void SendDataToDialogue()
    {
        Dictionary<string, object> moneyDict = new Dictionary<string, object>(1);
        moneyDict[nameof(money)] = money.value;
        DialogueManager.GetInstance().SetVariableState(moneyDict);
    }


    //ITakeFromFile
    public void LoadData(GameData data)
    {
        this.money = data.globals.money;
    }
}
