using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// To use a generic UnityEvent type you must override the generic type.
/// </summary>
[System.Serializable]
public class StatDataEvent : UnityEvent<StatData> {}

/// <summary>
/// A flexible handler for statData events in the form of a MonoBehaviour. Responses can be connected directly from the Unity Inspector.
/// </summary>
public class StatDataEventListener : MonoBehaviour
{
	[SerializeField] private StatDataEventChannelSO _channel = default;

	public StatDataEvent OnEventRaised;

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

	private void Respond(StatData value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(value);
	}
}
