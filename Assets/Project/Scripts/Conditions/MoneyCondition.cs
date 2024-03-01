using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Condition/Money")]
public class MoneyCondition : Condition
{
    [SerializeField]private IntEventChannelSO dataChangeEvent = default;
    [SerializeField]private int moneyNeeded;
    int currentData = -1;

    // TO DO: change to work with moneyChannelRequest or other faster method
    // potential errors - multi MoneyBehaviour in scene or it's not exist or not loaded
    void SlowTakeData()
    {
        MoneyBehaviour moneyBehaviour = FindObjectOfType<MoneyBehaviour>();
        currentData = moneyBehaviour.Money;
    }

    public override void Reset() 
    {
        base.Reset();
        currentData = -1;
    }

    public override bool Check()
    {
        if(IsDone) return true;

        //If stat wasn't updated yet, it will be getting by slow method
        if(currentData == -1)
        {
            SlowTakeData();
        }
        
        if(currentData >= moneyNeeded)
        {
            IsDone = true;
            Debug.Log("MoneyCondition is done");
            InvokeDone();
            return true;
        }
        else
        {
            Debug.Log("MoneyCondition not done yet");
            return false;
        }
    }

    void CheckNewData(int data)
    {
        currentData = data;
        Check();
    }

    public override void SubscribeEvents()
    {
        base.SubscribeEvents();
        if (dataChangeEvent != null)
			dataChangeEvent.OnEventRaised += CheckNewData;
    }
    public override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();
        if (dataChangeEvent != null)
			dataChangeEvent.OnEventRaised -= CheckNewData;
    }
}