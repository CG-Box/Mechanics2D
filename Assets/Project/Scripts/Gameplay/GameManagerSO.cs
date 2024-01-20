using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "GameManagerSO", menuName = "SriptableObjects/GameManager", order = 99)]
public class GameManagerSO : DescriptionBaseSO
{
	[Header("Slot manager")]
    [SerializeField] private SlotManager slotManager;


	[Header("Events Listen")]
    public VoidEventChannelSO exitEventChannel = default;
	public VoidEventChannelSO continueGameEventChannel = default;
	public VoidEventChannelSO saveGameEventChannel = default;
	public StringEventChannelSO sceneChangedEventChannel = default;

	[Header("Events Raise")]
	public TransitionPointEventChannelSO sceneRequestEventChannel = default;

	#region EventsBinding
    void OnEnable()
	{}

	void OnDisable()
	{}
	public void AddBindings()
	{
		CheckForReferences();

		if (exitEventChannel != null)
			exitEventChannel.OnEventRaised += ExitGame;

		if (continueGameEventChannel != null)
			continueGameEventChannel.OnEventRaised += ContinueGame;

		if (saveGameEventChannel != null)
			saveGameEventChannel.OnEventRaised += SaveGame;

		if (sceneChangedEventChannel != null)
			sceneChangedEventChannel.OnEventRaised += SceneChanged;
	}
	public void RemoveBindings()
	{
		if (exitEventChannel != null)
			exitEventChannel.OnEventRaised -= ExitGame;

		if (continueGameEventChannel != null)
			continueGameEventChannel.OnEventRaised -= ContinueGame;

		if (saveGameEventChannel != null)
			saveGameEventChannel.OnEventRaised -= SaveGame;

		if (sceneChangedEventChannel != null)
			sceneChangedEventChannel.OnEventRaised -= SceneChanged;
	}
	#endregion

	void CheckForReferences()
	{
		if(slotManager == null)
			Debug.LogError("SlotManager must be defined");
	}

	public void ContinueGame()
	{
		slotManager.ContinueGame();
	}

	public void SaveGame()
	{
		slotManager.SaveActiveSlot();
	}

	void SceneChanged(string sceneName)
	{
		slotManager.ChangeActiveSlotSceneName(sceneName);
	}

    public void ExitGame()
	{
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else       
        Application.Quit();
        #endif
	}
}
