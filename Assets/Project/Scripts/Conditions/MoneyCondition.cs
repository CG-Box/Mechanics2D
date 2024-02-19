using UnityEngine;

[CreateAssetMenu(menuName = "Condition/Money")]
public class MoneyCondition : Condition
{
    [SerializeField]private int moneyNeeded;
    public override bool Check()
    {
        if(IsDone) return true;
        if(10 >= moneyNeeded)
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
}