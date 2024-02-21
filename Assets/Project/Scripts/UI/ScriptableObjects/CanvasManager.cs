using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CanvasManager", menuName = "SriptableObjects/CanvasManager", order = 90)]
public class CanvasManager : DescriptionBaseSO
{
	[Header("Events Listen")]
    public VoidEventChannelSO dialogueStartEvent = default;
	public VoidEventChannelSO dialogueEndEvent = default;

	[Header("Events Raise")]
	public VoidEventChannelSO LockStatsPanelEvent = default;
	public VoidEventChannelSO UnlockStatsPanelEvent = default;

	#region EventsBinding
	public void AddBindings()
	{
		if (dialogueStartEvent != null)
			dialogueStartEvent.OnEventRaised += DialogueStartEvent_Handler;
		if (dialogueEndEvent != null)
			dialogueEndEvent.OnEventRaised += DialogueEndEvent_Handler;
	}
	public void RemoveBindings()
	{
		if (dialogueStartEvent != null)
			dialogueStartEvent.OnEventRaised -= DialogueStartEvent_Handler;
		if (dialogueEndEvent != null)
			dialogueEndEvent.OnEventRaised -= DialogueEndEvent_Handler;
	}
	#endregion

	void DialogueStartEvent_Handler()
	{
		LockStatsPanelEvent.RaiseEvent();
	}
	void DialogueEndEvent_Handler()
	{
		UnlockStatsPanelEvent.RaiseEvent();
	}
}
