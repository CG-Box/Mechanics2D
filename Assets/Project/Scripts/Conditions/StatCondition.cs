using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Condition/Stat")]
public class StatCondition : Condition
{
    [SerializeField]private StatDataEventChannelSO dataChangeEvent = default;
    [SerializeField]private StatType statType;
    [SerializeField]private int statNeeded;

    StatData currentStat = default;

    // TO DO: change to work with statsChannelRequest or other faster method
    // potential errors - multi StatsBehaviour in scene or it's not exist or not loaded
    void SlowTakeData()
    {
        StatsBehaviour statsBehaviour = FindObjectOfType<StatsBehaviour>();
        int statValue = statsBehaviour.GetStat(statType);
        currentStat = new StatData(statType, statValue);
    }

    public override void Reset() 
    {
        base.Reset();
        currentStat = default;
    }

    public override bool Check()
    {
        if(IsDone) return true;

        //If stat wasn't updated yet, it will be getting by slow method
        if(currentStat.type == StatType.Empty)
        {
            SlowTakeData();
        }

        //Debug.Log("currentStat " +currentStat.value);

        if(currentStat.value >= statNeeded)
        {
            IsDone = true;
            //Debug.Log("StatCondition is done");
            InvokeDone();
            return true;
        }
        else
        {
            //Debug.Log("StatCondition not done yet");
            return false;
        }
    }

    void CheckNewData(StatData statData)
    {
        if(statData.type == statType)
        {
            currentStat = statData;
            Check();
        }
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