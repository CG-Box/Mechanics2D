using UnityEngine.Events;
using UnityEngine;

/// <summary>
/// This class is used for Events that have one TextAsset argument.
/// Example: Trigger dialogue event, where the TextAsset is the dialogue.
/// </summary>

[CreateAssetMenu(menuName = "Events/TextAsset Event Channel")]
public class TextAssetEventChannelSO : DescriptionBaseSO
{
	public UnityAction<TextAsset> OnEventRaised;
	
	public void RaiseEvent(TextAsset value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(value);
	}
}
