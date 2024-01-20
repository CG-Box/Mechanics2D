using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have one string argument.
/// Example: Change scene event, where the string is the new scene name.
/// </summary>
/// 
[CreateAssetMenu(menuName = "Events/String Event Channel")]
public class StringEventChannelSO : DescriptionBaseSO
{
	public UnityAction<string> OnEventRaised;
	
	public void RaiseEvent(string value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(value);
	}
}
