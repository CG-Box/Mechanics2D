using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/Money Add")]
public class MoneyAddEffect : Effect
{
    //[SerializeField]private IntEventChannelSO moneyChangeRequest = default;
    [SerializeField]private int amount;
    public int Amount { get {return amount;} }

    public override void Apply()
    {
        base.Apply();

        //moneyChangeRequest.RaiseEvent(amount);
        EffectManager.AddMoney(this);
    }
}

