using UnityEngine.Events;
using UnityEngine;

/// <summary>
/// This class is used for StatsBehaviour when stats change to norify other classes.
/// </summary>

[CreateAssetMenu(menuName = "Events/StatData Event Channel")]
public class StatDataEventChannelSO : DescriptionBaseSO
{
	public UnityAction<StatData> OnEventRaised;

	public void RaiseEvent(StatData value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(value);
	}
}
