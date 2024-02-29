using System.Collections.Generic;
using UnityEngine;

public enum MenuType
{
	Pause,
	Stats,
	Dialogue,
}


[CreateAssetMenu(fileName = "CanvasManager", menuName = "SriptableObjects/CanvasManager", order = 90)]
public class CanvasManager : DescriptionBaseSO
{
	[Header("Events Listen")]
    [SerializeField] private VoidEventChannelSO dialogueStartEvent = default;
	[SerializeField] private VoidEventChannelSO dialogueEndEvent = default;

	[Header("Input Events")]
	[SerializeField] private VoidEventChannelSO OnTabClick;
    [SerializeField] private VoidEventChannelSO OnEscClick;

	[Header("Events Raise")]
	[SerializeField]private ControlDataEventChannelSO inputControlChannel;

	Stack<MenuType> menuStack = new Stack<MenuType>();


	#region EventsBinding
	public void AddBindings()
	{
		menuStack.Clear(); // when new scene loaded we need to clear menu stack

		if (dialogueStartEvent != null)
			dialogueStartEvent.OnEventRaised += DialogueStartEvent_Handler;
		if (dialogueEndEvent != null)
			dialogueEndEvent.OnEventRaised += DialogueEndEvent_Handler;

		if (OnTabClick != null)
			OnTabClick.OnEventRaised += TabClick_Handler;
		if (OnEscClick != null)
			OnEscClick.OnEventRaised += EscClick_Handler;
	}

	[ContextMenu("RemoveBindings")]
	public void RemoveBindings()
	{
		if (dialogueStartEvent != null)
			dialogueStartEvent.OnEventRaised -= DialogueStartEvent_Handler;
		if (dialogueEndEvent != null)
			dialogueEndEvent.OnEventRaised -= DialogueEndEvent_Handler;

		if (OnTabClick != null)
			OnTabClick.OnEventRaised -= TabClick_Handler;
		if (OnEscClick != null)
			OnEscClick.OnEventRaised -= EscClick_Handler;
	}
	#endregion


	void DialogueStartEvent_Handler()
	{
		ControlMenu(MenuType.Dialogue);
	}
	void DialogueEndEvent_Handler()
	{
		ControlMenu(MenuType.Dialogue);
	}


	void TabClick_Handler()
	{
		ControlMenu(MenuType.Stats);
	}
	void EscClick_Handler()
	{
		ControlMenu(MenuType.Pause);
	}

	void ControlMenu(MenuType targetType)
	{
		if(menuStack.Count == 0) // open menu
		{
			OpenMenu(targetType);
			FreezePlayer(targetType);
		}
		else
		{
			if(menuStack.Contains(targetType)) // close menu
			{
				CloseMenu(targetType);
				UnfreezePlayer(targetType);

				if(menuStack.Count != 0) // if another menu still open
				{
					targetType = menuStack.Peek();
					FreezePlayer(targetType);
				}
			}
			else // open second menu
			{
				OpenMenu(targetType);
				FreezePlayer(targetType);
			}
		}
	}
	void OpenMenu(MenuType targetType)
	{
		menuStack.Push(targetType);
		//OpenMenuRequest.RaiseEvent((int)targetType);
	}
	void CloseMenu(MenuType targetType)
	{
		menuStack.Pop();
		//CloseMenuRequest.RaiseEvent((int)targetType);
	}

	void FreezePlayer(MenuType targetType)
    {
		switch (targetType)
		{
			case MenuType.Pause:
				inputControlChannel.RaiseEvent(new ControlData(false));
				break;
			case MenuType.Stats:
				inputControlChannel.RaiseEvent(new ControlData(InputType.MovementAndUse,false));
				break;
			case MenuType.Dialogue:
				inputControlChannel.RaiseEvent(new ControlData(InputType.ExceptEsc,false));
				break;
		}
    }
    void UnfreezePlayer(MenuType targetType)
    {
		switch (targetType)
		{
			case MenuType.Pause:
				inputControlChannel.RaiseEvent(new ControlData(true));
				break;
			case MenuType.Stats:
				inputControlChannel.RaiseEvent(new ControlData(InputType.MovementAndUse,true));
				break;
			case MenuType.Dialogue:
				inputControlChannel.RaiseEvent(new ControlData(InputType.ExceptEsc,true));
				break;
		}
    }
}
