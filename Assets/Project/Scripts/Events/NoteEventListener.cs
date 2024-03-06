using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// To use a generic UnityEvent type you must override the generic type.
/// </summary>
[System.Serializable]
public class NoteEvent : UnityEvent<Note> {}

/// <summary>
/// A flexible handler for note events in the form of a MonoBehaviour. Responses can be connected directly from the Unity Inspector.
/// </summary>
public class NoteEventListener : MonoBehaviour
{
	[SerializeField] private NoteEventChannelSO _channel = default;

	public NoteEvent OnEventRaised;

	private void OnEnable()
	{
		if (_channel != null)
			_channel.OnEventRaised += Respond;
	}

	private void OnDisable()
	{
		if (_channel != null)
			_channel.OnEventRaised -= Respond;
	}

	private void Respond(Note value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(value);
	}
}

