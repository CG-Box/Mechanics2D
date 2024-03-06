using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/Stat Add Point")]
public class AddStatPointEffect : Effect
{
    [SerializeField] private IntEventChannelSO addPointsRequest = default;
    [SerializeField] private int addAmount;

    public override void Apply()
    {
        base.Apply();

        addPointsRequest.RaiseEvent(addAmount);
    }
}

