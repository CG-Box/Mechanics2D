using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Linq;

[CreateAssetMenu(fileName = "GameManagerSO", menuName = "ScriptableObjects/GameManager", order = 99)]
public class GameManagerSO : DescriptionBaseSO
{
	[Header("Slot manager")]
    [SerializeField] private SlotManager slotManager;


	[Header("Events Listen")]
    public VoidEventChannelSO exitEventChannel = default;
	public VoidEventChannelSO continueGameEventChannel = default;
	public VoidEventChannelSO saveGameEventChannel = default;
	public TransitionPointEventChannelSO sceneChangedEventChannel = default;

	//Valuables
	public IntEventChannelSO healthUpdateEvent = default;
	public GameObjectEventChannelSO inventoryUpdateEvent = default;

	[Header("Events Raise")]
	public TransitionPointEventChannelSO sceneRequestEventChannel = default;
    public ItemDataEventChannelSO addItemRequest = default;
    public ItemDataEventChannelSO removeItemRequest = default;

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

		//Valuables
		if (healthUpdateEvent != null)
			healthUpdateEvent.OnEventRaised += HealthChanged;

		if (inventoryUpdateEvent != null)
			inventoryUpdateEvent.OnEventRaised += InventoryChanged;
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
		
		//Valuables
		if (healthUpdateEvent != null)
			healthUpdateEvent.OnEventRaised -= HealthChanged;

		if (inventoryUpdateEvent != null)
			inventoryUpdateEvent.OnEventRaised -= InventoryChanged;
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
		Debug.Log("SaveGame");
		slotManager.SaveActiveSlot();
	}

	void SceneChanged(Mechanics2D.TransitionPointData transitionPointData)
	{
		slotManager.ChangeActiveSlotSceneName(transitionPointData.newSceneName);
		slotManager.ChangeActiveSlotSceneDestination(transitionPointData.transitionDestinationTag);
		LoadDataToScripts();
	}

	void HealthChanged(int newHealt)
	{
		Debug.Log($"health changes {newHealt}");
	}
	void InventoryChanged(GameObject gameObject)
	{
		if(gameObject.tag == Constants.PlayerTag)
		{
			//slotManager.ChangeInventory(gameObject);
			//may be some variables need to change or event raise
		}
		else
		{
			Debug.Log("its not a player");
		}
	}


	void LoadDataToScripts()
	{
		List<ITakeFromFile> dataPersistenceObjects = FindAllDataPersistenceObjects();
		foreach (ITakeFromFile dataPersistenceObj in dataPersistenceObjects) 
        {
            dataPersistenceObj.LoadData(slotManager.GetActiveSlot().data);
        }
	}

    List<ITakeFromFile> FindAllDataPersistenceObjects() 
    {
        // FindObjectsofType takes in an optional boolean to include inactive gameobjects
        IEnumerable<ITakeFromFile> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true).OfType<ITakeFromFile>();

        return new List<ITakeFromFile>(dataPersistenceObjects);
    }

	//Remove this useless
	public void AddItemToPlayer(ItemType itemType = ItemType.Taco)
	{
		//need to change get player from prefab or tag 
		Debug.LogWarning("change player finding");
		InventoryBehaviour playerInventory = FindObjectOfType<PlayerMovement>().GetComponent<InventoryBehaviour>();
		ItemEventArg itemEventArg = new ItemEventArg(new ItemData(itemType, 1), playerInventory);
		addItemRequest.RaiseEvent(itemEventArg);    
	}
	public void RemoveItemFromPlayer(ItemType itemType = ItemType.Taco)
	{
		//need to change get player from prefab or tag 
		Debug.LogWarning("change player finding");
		InventoryBehaviour playerInventory = FindObjectOfType<PlayerMovement>().GetComponent<InventoryBehaviour>();
		ItemEventArg itemEventArg = new ItemEventArg(new ItemData(itemType, 1), playerInventory);
		removeItemRequest.RaiseEvent(itemEventArg);    
	}
	//Remove this useless

    public void ExitGame()
	{
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else       
        Application.Quit();
        #endif
	}
}
