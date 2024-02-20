using UnityEngine;

[CreateAssetMenu(menuName = "Condition/Stat")]
public class StatCondition : Condition
{
    [SerializeField]private StatDataEventChannelSO statsChangeEvent = default;
    [SerializeField]private StatType statType;
    [SerializeField]private int statNeeded;

    StatData currentStat = default;
    public override bool Check()
    {
        if(IsDone) return true;

        // TO DO: change to work with statsChannelRequest
        //StatsBehaviour statsBehaviour = FindObjectOfType<StatsBehaviour>();
        //int currentStat = statsBehaviour.GetStat(statType);

        if(currentStat.type == StatType.Empty)
        {
            Debug.LogWarning($"currentStat type: {currentStat.type}, update is before use");
            return false;
        }

        Debug.Log("currentStat " +currentStat.value);


        if(currentStat.value >= statNeeded)
        {
            IsDone = true;
            Debug.Log("StatCondition is done");
            InvokeDone();
            return true;
        }
        else
        {
            Debug.Log("StatCondition not done yet");
            return false;
        }
    }

    void CheckNewStat(StatData statData)
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
        if (statsChangeEvent != null)
			statsChangeEvent.OnEventRaised += CheckNewStat;
    }
    public override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();
        if (statsChangeEvent != null)
			statsChangeEvent.OnEventRaised -= CheckNewStat;
    }
}