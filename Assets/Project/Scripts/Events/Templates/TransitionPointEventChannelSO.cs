using UnityEngine.Events;
using UnityEngine;

/// <summary>
/// This class is used for SceneController scene changes.
/// </summary>

[CreateAssetMenu(menuName = "Events/TransitionPoint Event Channel")]
public class TransitionPointEventChannelSO : DescriptionBaseSO
{
	public UnityAction<Mechanics2D.TransitionPointData> OnEventRaised;

	public void RaiseEvent(Mechanics2D.TransitionPointData value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(value);
	}
}