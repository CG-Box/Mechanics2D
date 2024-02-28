using UnityEngine.Events;
using UnityEngine;

/// <summary>
/// This class is used for PlayerInput (PlayerMovement) when need to lock\unlock input.
/// </summary>

[CreateAssetMenu(menuName = "Events/ControlData Event Channel")]
public class ControlDataEventChannelSO : DescriptionBaseSO
{
	public UnityAction<ControlData> OnEventRaised;

	public void RaiseEvent(ControlData value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(value);
	}
}

