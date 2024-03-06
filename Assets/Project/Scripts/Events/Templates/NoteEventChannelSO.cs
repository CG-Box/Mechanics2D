using UnityEngine.Events;
using UnityEngine;

/// <summary>
/// This class is used for Chronicles to norify other classes.
/// </summary>

[CreateAssetMenu(menuName = "Events/Note Event Channel")]
public class NoteEventChannelSO : DescriptionBaseSO
{
	public UnityAction<Note> OnEventRaised;

	public void RaiseEvent(Note value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(value);
	}
}
