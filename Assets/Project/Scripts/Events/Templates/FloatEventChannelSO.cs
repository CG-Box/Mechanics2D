using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have one int argument.
/// Example: Change speed event, where the float is the object speed parametr.
/// </summary>
/// 
[CreateAssetMenu(menuName = "Events/Float Event Channel")]
public class FloatEventChannelSO : DescriptionBaseSO
{
	public UnityAction<float> OnEventRaised;
	
	public void RaiseEvent(float value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(value);
	}
}
